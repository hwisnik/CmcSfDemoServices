using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

#pragma warning disable 1591

namespace CmcSfDemoServices.Controllers
{
    public class JsonActionResult : IHttpActionResult
    {
        private HttpRequestMessage Request { get; }

        private string JsonText { get; }

        private bool Success { get; }

        public JsonActionResult(HttpRequestMessage request, string jsonText, bool success)
        {
            Request = request;
            JsonText = jsonText;
            Success = success;

        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            var response = Success ? Request.CreateResponse(HttpStatusCode.OK,JsonText) : Request.CreateResponse(HttpStatusCode.InternalServerError,JsonText);

            response.Content = new StringContent(JsonText, Encoding.UTF8, "application/json");

            return response;
        }
    }
}