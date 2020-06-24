using BusinessLogic.Services;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

#pragma warning disable 1591
namespace CmcSfDemoServices.Controllers
{
    public class HttpActionResult : IHttpActionResult
    {
        public string Message;
        public  HttpStatusCode StatusCode;
        public GenericServiceResponse CommandResult;

        public HttpActionResult(HttpStatusCode statusCode, GenericServiceResponse commandResult)
        {
            StatusCode = statusCode;
            CommandResult = commandResult;
        }

        public HttpActionResult(HttpStatusCode statusCode, string errorMsg)
        {
            StatusCode = statusCode;
            if (CommandResult == null) { CommandResult = new GenericServiceResponse();}
            CommandResult.OperationException = new Exception(errorMsg);
            CommandResult.Success = false;
        }


        public HttpActionResult(HttpStatusCode statusCode, Exception error)
        {
            StatusCode = statusCode;
            CommandResult.OperationException = error;
            CommandResult.Success = false;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(StatusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(CommandResult))
            };
             return Task.FromResult(response);
        }

    }
}