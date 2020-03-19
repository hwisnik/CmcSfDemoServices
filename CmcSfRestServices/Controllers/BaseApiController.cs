using BusinessLogic.Services;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace CmcSfRestServices.Controllers
{
#pragma warning disable 1591

    public class BaseApiController : ApiController
    {

        internal IHttpActionResult ReturnFormattedResults(GenericServiceResponse results)
        {
            if (results == null)
            {
                results = new GenericServiceResponse {Success = false, RestResponseStatus = GenericServiceResponse.RestStatus.Error, OperationException = new Exception("Not Found") };
            }

            if (results.Success) return Ok(results);
            return ResponseMessage(results.OperationException.Message == "Not Found" ? Request.CreateResponse(HttpStatusCode.NotFound, results) : Request.CreateResponse(HttpStatusCode.InternalServerError, results));
        }

        public Guid GetUserGuidFromHttpContextBase(HttpContextBase httpContextBase)
        {
            var claimsPrincipal = (ClaimsPrincipal)httpContextBase.User;

            if (!claimsPrincipal.HasClaim(c => c.Type.ToUpper() == "ERMSUSERGUID"))
            {
                return Guid.Empty;
            }

            var userGuidString = claimsPrincipal.Claims.FirstOrDefault(c => c.Type.ToUpper() == "ERMSUSERGUID")?.Value;
            return string.IsNullOrEmpty(userGuidString) ? Guid.Empty : Guid.Parse(userGuidString);
        }


        public int? GetUserOrgIdFromHttpContextBase(HttpContextBase httpContextBase)
        {
            try
            {
                var userOrgIdString = ((ClaimsPrincipal)(httpContextBase).User).Claims.FirstOrDefault(c => c.Type.ToUpper() == "ERMSDEFAULTORGID")?.Value;
                if (string.IsNullOrEmpty(userOrgIdString)) { return null; }
                int.TryParse(userOrgIdString, out var intOrgId);
                return intOrgId;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetAdUserName(System.Security.Principal.IPrincipal userPrincipal)
        {
            return ((ClaimsIdentity)userPrincipal.Identity).Claims.FirstOrDefault(x => x.Type.ToUpper() == "ERMSADUSERNAME")?.Value;
        }

    }
}
