using Shared.Entities;
using DataAccess.Repositories;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DataAccess.Repositories.Interfaces;
using Shared.Logger;


#pragma warning disable 1591

namespace CmcSfDemoServices.Owin
{
    // using AppFunc = Func<IDictionary<string, object>, Task>;

    public class OwinMiddlewareLogger : OwinMiddleware
    {
        //private readonly AppFunc _next;
        private readonly OwinMiddleware _next;
        //private readonly IAppBuilder _app;

        private readonly IHttpLoggerRepository _httpLoggerRepository;
        private readonly bool _isLoggingEnabled;
        private bool _logThisError;

        public OwinMiddlewareLogger(OwinMiddleware next, IAppBuilder app, IReadOnlyList<object> args) : base(next)
        {
            _next = next;
            //_app = app;

            _httpLoggerRepository = (HttpLoggerRepository)args[0];
            _isLoggingEnabled = (bool)args[1];
            // _httpLoggerRepository = new HttpLoggerRepository(connFactory);
            //_isLoggingEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["HttpLoggerEnabled"]);
        }

        public override async Task Invoke(IOwinContext context)
        {
            //Don't log if the isLoggingEnabled = false or Swagger requests.
            if (!_isLoggingEnabled || context.Request.Uri.AbsoluteUri.ToLower().Contains("localhost"))
            {
                //skip the middleware logger
                await _next.Invoke(context);
                return;
            }
            //public async Task Invoke(IDictionary<string, object> environment)
            //{
            //    IOwinContext context = new OwinContext(environment);


            // Get the identity 
            var identity = (context.Request.User != null && context.Request.User.Identity.IsAuthenticated)
                ? context.Request.User.Identity.Name
                : "(anonymous)";


            // Buffer the request (body is a string, we can use this to log the request later
            var requestBody = new StreamReader(context.Request.Body).ReadToEnd();
            var requestData = Encoding.UTF8.GetBytes(requestBody);
            context.Request.Body = new MemoryStream(requestData);

            var apiPacket = new ApiPacket
            {
                CallerIdentity = identity,
                RequestBody = requestBody,
                RequestLength = context.Request.Body.Length,
                RequestTimestamp = DateTime.Now // HttpContext.Current.Timestamp
            };

            var replayId = LoggingHelper.GetReplayId(requestBody);
            if (replayId != 0)
            {
                apiPacket.ReplayId = replayId;
            }
            apiPacket.CallerIdentity = SplitRequestBody(requestBody, "username");


            // Buffer the response
            var responseBuffer = new MemoryStream();
            var responseStream = context.Response.Body;
            context.Response.Body = responseBuffer;

            // add the "http-correlation-id" response header so the user can correlate back to this entry
            var responseHeaders = (IDictionary<string, string[]>)context.Environment["owin.ResponseHeaders"];
            //responseHeaders["http-correlation-id"] = new[] { apiPacket.CorrelationId.ToString("d") };

            await _next.Invoke(context);

            responseBuffer.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(responseBuffer);
            apiPacket.Response = await reader.ReadToEndAsync();

            //If you need code to check for a specific request and modify the associated response
            //if (context.Request.Path.Value == @"/token")
            //{
            //    responseBuffer.Seek(0, SeekOrigin.Begin);
            //    var reader = new StreamReader(responseBuffer);
            //    apiPacket.Response = await reader.ReadToEndAsync();

            //    JObject jResp = JObject.Parse(apiPacket.Response);
            //    JObject jUserGuid = JObject.Parse(@"{'UserGuid':'PutUseGuidValueHere'}");
            //    jResp.Add("UserGuid", JToken.FromObject("UserGuidValue"));
            //    apiPacket.Response = jResp.ToString();


            //    var writeStream = new MemoryStream();
            //    StreamWriter writer = new StreamWriter(writeStream);
            //    writer.Write(apiPacket.Response);
            //    writer.Flush();
            //    writeStream.Position = 0;

            //    responseBuffer.Seek(0, SeekOrigin.Begin);
            //    writeStream.CopyTo(responseBuffer);
            //}

            //context.Response.Body = responseBuffer;
            apiPacket.ResponseLength = context.Response.ContentLength ?? 0;
            apiPacket.Duration = (DateTime.Now - apiPacket.RequestTimestamp).TotalSeconds;

            //Make sure that user and the user identity is not null and that claims is not null before you look for the CorrelationId and AdUserName
            //or you will get a null ref error
            string trackingId;
            if (((ClaimsIdentity)context.Authentication.User?.Identity)?.Claims.FirstOrDefault() != null)
            {
                if (context.Authentication.User != null)
                {
                    trackingId = ((ClaimsIdentity)context.Authentication.User.Identity).Claims.FirstOrDefault(x => x.Type.ToUpper() == "SFCORRELATIONID")?.Value;
                    Guid.TryParse(trackingId, out Guid trackGuid);
                    apiPacket.CorrelationId = trackGuid;
                    apiPacket.CallerIdentity = ((ClaimsIdentity)context.Authentication.User.Identity).Claims.FirstOrDefault(x => x.Type.ToUpper() == "SFADUSERNAME")?.Value;
                }

                context.Environment.TryGetValue("owin.CallCancelled", out var cancellation);
                if (cancellation != null)
                {
                    var token = (CancellationToken)cancellation;
                    if (token.IsCancellationRequested == false)
                        responseHeaders["http-correlation-id"] = new[] { apiPacket.CorrelationId.ToString("d") };
                }
            }

            else if (responseHeaders != null && responseHeaders.ContainsKey("http-correlation-id") && responseHeaders["http-correlation-id"].FirstOrDefault() != Guid.Empty.ToString())
            {
                trackingId = responseHeaders["http-correlation-id"].FirstOrDefault();
                Guid.TryParse(trackingId, out Guid trackGuid);
                apiPacket.CorrelationId = trackGuid;
            }

            WriteRequestHeaders(context.Request, apiPacket);
            WriteResponseHeaders(context.Response, apiPacket);

            _logThisError = apiPacket.StatusCode != (int)HttpStatusCode.OK;
            try
            {
                //Persist the ApiPacket in the database ignore any logging errors ex: (System.Web.HttpException (0x80004005): Server cannot append header after HTTP headers have been sent. 
                await _httpLoggerRepository.InsertApiPacketAsync(_isLoggingEnabled, apiPacket, _logThisError);
            }

            finally
            {
                // You need to do the following so that the buffered response is flushed out to the client application.
                responseBuffer.Seek(0, SeekOrigin.Begin);
                await responseBuffer.CopyToAsync(responseStream);
            }
        }

        public static void WriteRequestHeaders(IOwinRequest request, ApiPacket packet)
        {
            packet.Verb = request.Method;
            ObfuscatePassword(packet);
            packet.RequestUri = request.Uri.AbsoluteUri;
            packet.RequestHeaders = "{\r\n" + string.Join(Environment.NewLine, request.Headers.Select(kv => "\t" + kv.Key + "=" + string.Join(",", kv.Value))) + "\r\n}";
        }

        private static void WriteResponseHeaders(IOwinResponse response, ApiPacket packet)
        {
            packet.StatusCode = response.StatusCode;
            packet.ReasonPhrase = response.ReasonPhrase;
            packet.ResponseHeaders = "{\r\n" + string.Join(Environment.NewLine, response.Headers.Select(kv => "\t" + kv.Key + "=" + string.Join(",", kv.Value))) + "\r\n}";
        }

        private static void ObfuscatePassword(ApiPacket packet)
        {
            if (!packet.RequestBody.Contains("grant_type")) return;
            var startPos = packet.RequestBody.IndexOf("&password=", StringComparison.Ordinal);

            var pwordLen = 0;
            var charArray = new[] { '&' };
            var splitArray = packet.RequestBody.Split(charArray, packet.RequestBody.Length);
            foreach (var item in splitArray)
            {
                var splitItem = item.Split('=');
                //Left hand side of expression should be password  (ensuring that we find password= or password =)
                if (splitItem[0].Contains("password") && splitItem.Count() == 2)
                {
                    pwordLen = splitItem[1].Length;
                }
            }
            var password = packet.RequestBody.Substring(startPos + "&password=".Length, pwordLen);
            packet.RequestBody = packet.RequestBody.Replace(password, "XXXXX");
        }
        private static string SplitRequestBody(string stringToSplit,  string searchTerm)
        {
            var retVal = string.Empty;
            if (string.IsNullOrEmpty(stringToSplit)) return retVal;
            var splitArray = stringToSplit.Split('&');
            foreach (var item in splitArray)
            {
                if (!item.Contains(searchTerm)) continue;
                var splitItem = item.Split('=');
                if (splitItem.Count() != 2) continue;
                retVal = splitItem[1];
                return retVal;
            }
            return retVal;
        }
    }

}
