using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Services.Description;

namespace CmcSfRestServices.Filters

{
    /// <inheritdoc />
    /// <summary>
    /// Https URI validator class
    /// </summary>
    public class HttpsValidator : AuthorizationFilterAttribute
    {
        ///  <summary>
        ///  Validate request URI
        ///  </summary>
        ///  <param name="actionContext">HttpActionContext value</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext?.Request == null || actionContext.Request.RequestUri.Scheme.Equals(Uri.UriSchemeHttps)) return;
            var controllerFilters = actionContext.ControllerContext.ControllerDescriptor.GetFilters();
            var actionFilters = actionContext.ActionDescriptor.GetFilters();

            if ((controllerFilters != null && controllerFilters.Select
                     (t => t.GetType() == typeof(HttpsValidator)).Any()) ||
                (actionFilters != null && actionFilters.Select(t =>
                     t.GetType() == typeof(HttpsValidator)).Any()))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden,
                    new Message() { Documentation = "Requested URI requires HTTPS" },
                    new MediaTypeHeaderValue("text/json"));
            }
        }
    }
}
