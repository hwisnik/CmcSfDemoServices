using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using System.Web.Http.Filters;
#pragma warning disable 1591

namespace CmcSfDemoServices.Filters
{
    public class LogResponseBodyInterceptorAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (actionExecutedContext?.Response?.Content is ObjectContent)
            {
                actionExecutedContext.Request.GetOwinContext().Environment["log-requestBody"] =
                    await actionExecutedContext.Request.Content.ReadAsStringAsync();
                //await actionExecutedContext.Response.Content.ReadAsHttpRequestMessageAsync().ConfigureAwait(false);

            }
        }
    }
}
