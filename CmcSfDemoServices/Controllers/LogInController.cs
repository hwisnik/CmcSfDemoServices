using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Web.Http;

namespace CmcSfDemoServices.Controllers
{
    /// <summary>
    /// Controls Login related actions
    /// </summary>
    public class LoginController : ApiController
    {
        /// <summary>
        /// Sample method to show that the caller is authorized for a role = user 
        /// </summary>
        /// <returns></returns>
        [Route("Login")]
        [Authorize(Roles = "SalesforceRoleScheduler")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "Login Test Method", typeof(string))]

        public IHttpActionResult Get()
        {
            return Ok("You are loggedIn as a user");
        }

        /// <summary>
        /// Sample method to show that the caller is authorized for a role = ErmsAdmins
        /// </summary>
        /// <returns></returns>
        [Route("LoginAdmin")]
        [Authorize(Roles = "SalesforceRoleAdmin")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "LoginAdmin Test Method", typeof(string))]
        public IHttpActionResult LoginAdmin()
        {
            return Ok("You are loggedIn as an Admin");
        }
    }
}
