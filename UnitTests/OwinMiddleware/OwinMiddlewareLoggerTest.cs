using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CmcSfDemoServices.Owin;
using DataAccess.Infrastructure;
using DataAccess.Repositories;
using Shared.Logger;
using log4net;
using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Owin;
using Shared.Entities;
using Unit_Tests.Helpers;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using System.Reflection;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using BusinessLogic.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using Shared.Commands;
using Shared.Handlers;


namespace Unit_Tests.OwinMiddleware
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class OwinMiddlewareLoggerTest
    {
        #region setup
        private TestContext testContextInstance;
        private static Mock<ILeadService> MoqService { get; set; }
        private static Mock<ICommandHandler<LogCommand>> LogCommand { get; set; }
        private static Mock<ILog> MoqLogInstance { get; set; }
        //private static ClaimsIdentity _testIdentity;
        //private static string _bearerToken;
        private static string _tokenExpiration;
        public static string TokenExpiration { get => _tokenExpiration; set => _tokenExpiration = value; }


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            AppLogger.Initialize();
            MoqService = new Mock<ILeadService>();
            LogCommand = new Mock<ICommandHandler<LogCommand>>();
            MoqLogInstance = new Mock<ILog>();
            // OwinTestConf OwinTestConf = new OwinTestConf();
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void Cleanup()
        {
            //Runs after each test
            UnitTestHelpers.WriteTestResultsToTestContext(TestContext);
        }
        #endregion

        [TestMethod]
        public async Task OwinMiddlewareLoggerThrowsExceptionIfContextRequestBodyIsNull()
        {
            //using (var server = TestServer.Create<Startup>())
            //{
            //    Dictionary<string, object> dictionary = new Dictionary<string, object>();
            //    OwinContext context = new OwinContext();
            //    dictionary.Add("Test", context);

            //    await Assert.ThrowsExceptionAsync<NotSupportedException>(async () =>
            //       await Task.Run(() => server.Invoke(dictionary))
            //    );
            //}

            var server = TestServer.Create<Startup>();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                OwinContext context = new OwinContext();
                dictionary.Add("Test", context);

                await Assert.ThrowsExceptionAsync<NotSupportedException>(async () =>
                    await Task.Run(() => server.Invoke(dictionary))
                );

            //using (HttpClient client = new HttpClient())
            //{
            //    Dictionary<string, string> tokenDetails = null;
            //    var login = new Dictionary<string, string>
            //   {
            //       {"grant_type", "password"},
            //       {"username", "ERMSDevAdminUser"},
            //       {"password", "CircusTent32Free"},
            //   };

            //    //Act
            //    var response = client.PostAsync(new Uri("https://localhost/CmcSfRestServices/token"), new FormUrlEncodedContent(login)).Result;
            //    if (response.ReasonPhrase == HttpStatusCode.OK.ToString() && response.IsSuccessStatusCode)
            //    {
            //        tokenDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);
            //        if (tokenDetails != null && tokenDetails.Any())
            //        {
            //            //var tokenNo = tokenDetails.FirstOrDefault().Value;
            //            tokenDetails.TryGetValue("access_token", out _bearerToken);
            //            tokenDetails.TryGetValue("expires_in", out _tokenExpiration);
            //        }
            //    }

            //    TokenCacher.Delete("ERMSDevAdminUser");
            //    TokenCacher.Add("ERMSDevAdminUser", _bearerToken, DateTimeOffset.UtcNow.AddSeconds(Convert.ToDouble(_tokenExpiration)));



            //    //    using (var client = new HttpClient(server.Handler))
            //    //{
            //    //    var response = await client.GetAsync("http://testserver/CmcSfRestServices/GetUserByUserName");
            //    //    var result = await response.Content.ReadAsAsync<WhoAmiResult>();
            //    //    Assert.AreEqual("test user", result.UserName);
            //    //    Assert.AreEqual("test name identifier", result.NameIdentifier);
            //    //}
        }


        [TestMethod]
        public async Task OwinMiddlewareLoggerThrowsExceptionIfApiPacketIsNull()
        {
            using (var server = TestServer.Create<Startup>())
            {

                //HttpContext httpContext = new HttpContext(new HttpRequest("", "http://localhost", ""), resp);
                //dictionary.Add("HttpContext", httpContext);

                //HttpContext httpContext = new HttpContext(httpRequest, httpResponse);



                Uri url = new Uri("https://localhost/");
                RouteData routeData = new RouteData();

                HttpRequest httpRequest = new HttpRequest("", url.AbsoluteUri, "")
                {
                    //HttpResponse httpResponse = new HttpResponse(null);

                    RequestContext = new RequestContext()
                };
                byte[] requestData = Encoding.ASCII.GetBytes("RequestBody");

                var reqStream = new MemoryStream(requestData.Length);
                reqStream.Write(requestData, 0, requestData.Length);
                reqStream.Seek(0, SeekOrigin.Begin);

                OwinRequest owinRequest = new OwinRequest
                {
                    Method = "GET",
                    Body = reqStream
                };

                httpRequest.RequestType = "GET";
                string[] reqArray = new string[1];  //{ httpRequest.ContentType };
                Dictionary<string, string[]> reqHeaders = new Dictionary<string, string[]>()
                {
                    {"Connection", reqArray },
                    {"Accept", reqArray },
                    {"Accept-Language", reqArray },
                    {"Host", reqArray },
                    {"User-Agent", reqArray },
                    { "Content-Type", reqArray }
                };


                //httpRequest.Headers.Set("owin.RequestHeaders","RequestHeder");

                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                byte[] responseData = Encoding.ASCII.GetBytes("ResponseBody");

                var respStream = new MemoryStream(responseData.Length);
                respStream.Write(responseData, 0, responseData.Length);
                respStream.Seek(0, SeekOrigin.Begin);
                OwinRequest oReq = new OwinRequest
                {
                    Body = respStream
                };

                var headers = new NameValueCollection
                    {
                        { "Special-Header-Name", "false" }
                    };

                OwinContext context = new OwinContext(dictionary);
                context.Response.Body = oReq.Body;

                HttpResponse resp = new HttpResponse(new StringWriter());
                string[] respArray = new string[] { resp.ContentType };
                Dictionary<string, string[]> responseHeaders = new Dictionary<string, string[]>() { { "Content-Type", respArray } };


                responseHeaders.Add("owin.ResponseHeaders", respArray);




                //context.Request.Method = HttpMethod.Get.Method;

                //HttpContext owinContext = new HttpContext(httpRequest, httpResponse);

                //Dictionary<string, object> owinEnvironment = new Dictionary<string, object>()
                //    {
                //        {"owin.RequestBody", httpRequest},
                //        {"owin.ResponseHeaders",respArray},
                //        {"Method", HttpMethod.Get.Method}
                //    };

                Dictionary<string, object> owinEnvironment = new Dictionary<string, object>()
                    {
                        {"owin.RequestBody", oReq.Body},
                        {"owin.RequestHeader",reqHeaders },
                        {"owin.ResponseHeaders",responseHeaders},
                        {"System.Web.HttpContextBase", "System.Web.HttpContextWrapper"},
                        {"sendfile.SendAsync", "{Method=(System.Threading.Tasks.Task.SendFileAsync(System.String,Int64,System.Nullable'1[System.Int64],System.Threading.CancellationToken)}" }
                    };



                HttpContext httpContext = new HttpContext(httpRequest, resp);

                //dictionary.Add("HttpContext", httpContext);

                //HttpContext httpContext = new HttpContext(httpRequest, httpResponse);

                httpContext.Items.Add("owin.Environment", owinEnvironment);
                httpContext.Items.Add("Method", "Get");




                //var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                //            new HttpStaticObjectsCollection(), 10, true,
                //            HttpCookieMode.AutoDetect,
                //            SessionStateMode.InProc, false);

                //httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                //      BindingFlags.NonPublic | BindingFlags.Instance,
                //      null, CallingConventions.Standard,
                //      new[] { typeof(HttpSessionStateContainer) },
                //      null)
                //      .Invoke(new object[] { sessionContainer });

                ////var connectionFactory = new Mock<ConnectionFactory>();
                ////var ermsUserRepository = new Mock<ErmsUserRepository>();
                ////var loggingInstance = AppLogger.Initialize();
                ////var logCommand = new Mock<LoggingCommandHandlerDecorator<LogCommand>>(); //LoggingCommandHandlerDecorator<LogCommand>();
                ////var ermsUserService = new Mock<ErmsUserService>();  //(ermsUserRepository.Object,loggingInstance,logCommand);

                ////var controller = new ErmsUserController(ermsUserService.Object,logCommand.Object,loggingInstance);
                ////controller.Request.SetOwinContext(context);

                HttpContext.Current = httpContext;
                HttpContext.Current.Cache.Insert("Headers", reqHeaders);

                //var oContext = httpContext.Request.GetOwinContext();
                var current = HttpContext.Current.GetOwinContext();






                await Assert.ThrowsExceptionAsync<NotSupportedException>(async () =>
                await Task.Run(() =>
                {
                    return server.Invoke(owinEnvironment);
                })
                );
            }
        }

        //[TestMethod]
        public async Task TestSomething()
        {
            using (var server = TestServer.Create<Startup>())
            {

                ////HttpContext httpContext = new HttpContext(new HttpRequest("", "http://localhost", ""), resp);
                ////dictionary.Add("HttpContext", httpContext);

                ////HttpContext httpContext = new HttpContext(httpRequest, httpResponse);



                //Uri url = new Uri("https://localhost/");
                //RouteData routeData = new RouteData();

                //HttpRequest httpRequest = new HttpRequest("", url.AbsoluteUri, "");
                ////HttpResponse httpResponse = new HttpResponse(null);

                //httpRequest.RequestContext = new RequestContext();
                //byte[] requestData = Encoding.ASCII.GetBytes("RequestBody");

                //var reqStream = new MemoryStream(requestData.Length);
                //reqStream.Write(requestData, 0, requestData.Length);
                //reqStream.Seek(0, SeekOrigin.Begin);

                //OwinRequest owinRequest = new OwinRequest
                //{
                //    Method = "GET",
                //    Body = reqStream
                //};

                //httpRequest.RequestType = "GET";
                //string[] reqArray = new string[1];  //{ httpRequest.ContentType };
                //Dictionary<string, string[]> reqHeaders = new Dictionary<string, string[]>()
                //{
                //    {"Connection", reqArray },
                //    {"Accept", reqArray },
                //    {"Accept-Language", reqArray },
                //    {"Host", reqArray },
                //    {"User-Agent", reqArray },
                //    { "Content-Type", reqArray }
                //};


                ////httpRequest.Headers.Set("owin.RequestHeaders","RequestHeder");

                //Dictionary<string, object> dictionary = new Dictionary<string, object>();
                //byte[] responseData = Encoding.ASCII.GetBytes("ResponseBody");

                //var respStream = new MemoryStream(responseData.Length);
                //respStream.Write(responseData, 0, responseData.Length);
                //respStream.Seek(0, SeekOrigin.Begin);
                //OwinRequest oReq = new OwinRequest
                //{
                //    Body = respStream
                //};



                //OwinContext context = new OwinContext(dictionary);
                //context.Response.Body = oReq.Body;

                //HttpResponse resp = new HttpResponse(new StringWriter());
                //string[] respArray = new string[] { resp.ContentType };
                //Dictionary<string, string[]> responseHeaders = new Dictionary<string, string[]>() { { "Content-Type", respArray } };


                //responseHeaders.Add("owin.ResponseHeaders", respArray);




                ////context.Request.Method = HttpMethod.Get.Method;

                ////HttpContext owinContext = new HttpContext(httpRequest, httpResponse);

                ////Dictionary<string, object> owinEnvironment = new Dictionary<string, object>()
                ////    {
                ////        {"owin.RequestBody", httpRequest},
                ////        {"owin.ResponseHeaders",respArray},
                ////        {"Method", HttpMethod.Get.Method}
                ////    };

                //Dictionary<string, object> owinEnvironment = new Dictionary<string, object>()
                //    {
                //        {"owin.RequestBody", oReq.Body},
                //        {"owin.RequestHeader",reqHeaders },
                //        {"owin.ResponseHeaders",responseHeaders},
                //        {"System.Web.HttpContextBase", "System.Web.HttpContextWrapper"},
                //        {"sendfile.SendAsync", "{Method=(System.Threading.Tasks.Task.SendFileAsync(System.String,Int64,System.Nullable'1[System.Int64],System.Threading.CancellationToken)}" }
                //    };



                //HttpContext httpContext = new HttpContext(httpRequest, resp);

                ////dictionary.Add("HttpContext", httpContext);

                ////HttpContext httpContext = new HttpContext(httpRequest, httpResponse);

                //httpContext.Items.Add("owin.Environment", owinEnvironment);
                //httpContext.Items.Add("Method", "Get");




                ////var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                ////            new HttpStaticObjectsCollection(), 10, true,
                ////            HttpCookieMode.AutoDetect,
                ////            SessionStateMode.InProc, false);

                ////httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                ////      BindingFlags.NonPublic | BindingFlags.Instance,
                ////      null, CallingConventions.Standard,
                ////      new[] { typeof(HttpSessionStateContainer) },
                ////      null)
                ////      .Invoke(new object[] { sessionContainer });

                //////var connectionFactory = new Mock<ConnectionFactory>();
                //////var ermsUserRepository = new Mock<ErmsUserRepository>();
                //////var loggingInstance = AppLogger.Initialize();
                //////var logCommand = new Mock<LoggingCommandHandlerDecorator<LogCommand>>(); //LoggingCommandHandlerDecorator<LogCommand>();
                //////var ermsUserService = new Mock<ErmsUserService>();  //(ermsUserRepository.Object,loggingInstance,logCommand);

                //////var controller = new ErmsUserController(ermsUserService.Object,logCommand.Object,loggingInstance);
                //////controller.Request.SetOwinContext(context);

                //HttpContext.Current = httpContext;
                //HttpContext.Current.Cache.Insert("Headers", reqHeaders);

                ////var oContext = httpContext.Request.GetOwinContext();
                //var current = HttpContext.Current.GetOwinContext();


                var mockHttpRequest = new Mock<HttpRequestBase>();


                mockHttpRequest.SetupGet(s => s.Headers).Returns(new NameValueCollection
                    {
                        { "Special-Header-Name", "false" }
                    });
                mockHttpRequest.SetupGet(s => s.IsSecureConnection).Returns(false); //(/*specify true or false depending on your test */);
                mockHttpRequest.SetupGet(s => s.HttpMethod).Returns("GET");

                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                byte[] responseData = Encoding.ASCII.GetBytes("ResponseBody");

                var respStream = new MemoryStream(responseData.Length);
                respStream.Write(responseData, 0, responseData.Length);
                respStream.Seek(0, SeekOrigin.Begin);
                OwinRequest oReq = new OwinRequest
                {
                    Body = respStream
                };

                OwinContext context = new OwinContext(dictionary);
                context.Response.Body = oReq.Body;

                HttpResponse resp = new HttpResponse(new StringWriter());
                string[] respArray = new string[] { resp.ContentType };
                Dictionary<string, string[]> responseHeaders = new Dictionary<string, string[]>() { { "Content-Type", respArray } };
                responseHeaders.Add("owin.ResponseHeaders", respArray);


                var mockHttpContext = new Mock<HttpContextBase>();
                mockHttpContext.Setup(s => s.GetService(It.IsAny<Type>())).Returns(mockHttpRequest.Object);
                mockHttpContext.SetupAllProperties();
                mockHttpContext.Setup(s => s.Request).Returns(mockHttpRequest.Object);
                //  mockHttpContext.Setup(s => s.GetOwinContext()).Returns(mockHttpContext.Object.GetOwinContext());

                Dictionary<string, object> owinEnvironment = new Dictionary<string, object>()
                    {
                        {"owin.RequestBody", oReq.Body},
                        {"owin.RequestHeader",mockHttpRequest.Object.Headers },
                        {"owin.ResponseHeaders",responseHeaders},
                        {"System.Web.HttpContextBase", "System.Web.HttpContextWrapper"},
                        //{"sendfile.SendAsync", "{Method=(System.Threading.Tasks.Task.SendFileAsync(System.String,Int64,System.Nullable'1[System.Int64],System.Threading.CancellationToken)}" }
                    };

                //var app = (HttpApplication)mockHttpContext.Object.GetService(typeof(HttpApplication));
                //HttpContext.Current = app.Context;

                ////HttpContext.Current = httpContext.Object.Request
                //var current = HttpContext.Current.GetOwinContext();

                var client = new HttpClient(server.Handler);

                var resp1 = await server.CreateRequest("/GetErmsUserByUserName?userName=pcaadmin").GetAsync();




                var request = new HttpRequest("", "http://google.com", "rUrl=http://www.google.com")
                {
                    ContentEncoding = Encoding.UTF8  //UrlDecode needs this to be set
                };

                var ctx = new HttpContext(request, new HttpResponse(new StringWriter()));

                //Session need to be set
                var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                    new HttpStaticObjectsCollection(), 10, true,
                    HttpCookieMode.AutoDetect,
                    SessionStateMode.InProc, false);
                //this adds aspnet session
                ctx.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.Instance,
                    null, CallingConventions.Standard,
                    new[] { typeof(HttpSessionStateContainer) },
                    null)
                    .Invoke(new object[] { sessionContainer });

                var data = new Dictionary<string, object>()
                        {
                            {"a", "b"} // fake whatever  you need here.
                        };

                ctx.Items["owin.Environment"] = data;















                await Task.Run(() =>
                    {
                        return server.Invoke(mockHttpContext.Object.GetOwinContext().Environment);
                    }
                    );


                var response = await client.GetAsync("/GetErmsUserByUserName?userName=pcaadmin");
                var result = await response.Content.ReadAsAsync<List<string>>();
                Assert.IsTrue(result.Any());


                //await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
                //await Task.Run(() =>
                //{
                //    return server.Invoke(owinEnvironment);
                //})
                //);
            }


        }
        [TestMethod]
        public void TestMethod1()
        {


            var moqAppBuilder = new Mock<IAppBuilder>();
            var moqConnectionFactory = new Mock<IConnectionFactory>();
            var moqHttpLoggerRepo = new Mock<IHttpLoggerRepository>();
            //moqHttpLoggerRepo.Setup(s => s.InsertApiPacket(false, new ApiPacket()).Result).Returns(1);

            var moqOwinRequest = new Mock<IOwinRequest>();
            //object[] args = new object[2];
            //args[0] = new HttpLoggerRepository(new ConnectionFactory());
            //args[1] = Convert.ToBoolean(ConfigurationManager.AppSettings["HttpLoggerEnabled"]);

            //appBuilder.Use<OwinMiddlewareLogger>(appBuilder, args);


            object[] args = new object[2];
            args[0] = moqHttpLoggerRepo.Object;
            args[1] = false;
            var x = moqAppBuilder.Object.Use<OwinMiddlewareLogger>(moqAppBuilder.Object, args);

            //return await Task.Run(() =>
            //{
            //    using (var context = new PrincipalContext(ContextType.Domain))
            //    {
            //        PrincipalContext pc = GetPrincipalContext(context);
            //        return pc.ValidateCredentials(userName, password);
            //    }
            //});



            var writeResponse = Task.Run(() =>
                    {
                        OwinMiddlewareLogger.WriteRequestHeaders(moqOwinRequest.Object, new ApiPacket());
                    });


        }



    }
}