using log4net;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.Services.Interfaces;
using Newtonsoft.Json;
using System.Net;
using Swashbuckle.Swagger.Annotations;
using BusinessLogic.Services;
using Shared.Commands;
using Shared.Entities.DTO.Helper;
using Shared.Entities.SFDb.Lead;
using Shared.Entities._SearchCriteria.Client;
using Shared.EventPayloads;
using Shared.Handlers;
using Shared.Logger;

namespace CmcSfRestServices.Controllers
{
    /// <summary>
    /// LeadController constructor with dependencies (params) shown below
    /// </summary>
    public class LeadController : BaseApiController
    {
        private readonly ILeadService _leadService;
        private readonly ILog _loggingInstance;

        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        //private Guid UserGuid { get; set; }

        /// <summary>
        /// LeadController constructor with dependencies (params) shown below
        /// </summary>
        /// <param name="leadService"></param>
        /// <param name="loggingInstance"></param>
        /// <param name="loghandler"></param>
        public LeadController(ILeadService leadService, ILog loggingInstance,
                LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _leadService = leadService;
            _loggingInstance = loggingInstance;
            _logHandler = loghandler;
        }


        /// <summary>
        /// Gets a LeadEntity by LeadSearcher
        /// </summary>
        /// <param name="leadSearcher"></param>
        /// <returns>LeadEntity in GenericServiceResponse Entity property</returns>
        [Route("GetLeads", Name = "GetLeads")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Gets a LeadEntity by LeadSearcher", typeof(SFSimpleLeadResults))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "LeadEntity exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult> GetLeads(SearchLeadSimple leadSearcher)
        {
            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"LeadController.GetLead Starting input parameter LeadSearcher = {JsonConvert.SerializeObject(leadSearcher)}"
            };
            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = await _leadService.GetLead(leadSearcher, logCommand);

            //Log the response
            logCommand.LogMessage =
                $"LeadController.GetLead completed. Output value = {JsonConvert.SerializeObject(results.Entity)}";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }

  

        /// <summary>
        /// Gets a LeadEntity by LeadGuid
        /// </summary>
        /// <param name="inputObject"></param>
        /// <returns>LeadEntity in GenericServiceResponse Entity property</returns>
        [Route("GetLeadDetailed", Name = "GetLeadDetailed")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Gets a Detailed Lead by LeadGuid", typeof(SFDetailedLead))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "GetLeadDetailed exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult> GetLeadDetailed(DetailedLeadGet inputObject)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"LeadController.GetLeadDetailed Starting input parameter CustomerGuid = {inputObject.CustomerGuid}"
            };
            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = await _leadService.GetLeadDetailed(inputObject.CustomerGuid, logCommand);

            //Log the response
            logCommand.LogMessage =
                $"LeadController.GetLead completed. Output value = {JsonConvert.SerializeObject(results.Entity)}";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }

        /// <summary>
        /// Gets a LeadEntity by LeadGuid
        /// </summary>
        /// <param name="peLeadDataForUpdate"></param>
        /// <returns>LeadEntity in GenericServiceResponse Entity property</returns>
        [Route("LeadEventListenerUpdate", Name = "LeadEventListenerUpdate")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Updates a Lead", typeof(LeadPayload))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "UpdateLead exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult> LeadEventListenerUpdate(PayloadParent peLeadDataForUpdate)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"LeadController.LeadEventListenerUpdate Starting input parameter CDCLeadDataForUpdate = {peLeadDataForUpdate}",
             };
            _logHandler.ReplayId = LoggingHelper.GetReplayId(JsonConvert.SerializeObject(peLeadDataForUpdate));
            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = await _leadService.UpdateLeadFromUpdateEvent(peLeadDataForUpdate, logCommand);
            //Log the response
            logCommand.LogMessage =
                $"LeadController.UpdateLead completed. Output value End of controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }
        
        /// <param name="payloadParent"></param>
        /// <returns>CustomerEntity in GenericServiceResponse Entity property</returns>
        [Route("AccountEventListenerUpdate", Name = "AccountEventListenerUpdate")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Adds SF Account ID to Account Table", typeof(LeadPayload))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "UpdateCustomer exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public IHttpActionResult AccountEventListenerUpdate(PayloadParent payloadParent)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"CustomerController.AccountAppointmentEventListenerUpdate Starting input parameter CDCCustomerDataForUpdate = {payloadParent}"
            };
            _logHandler.ReplayId = LoggingHelper.GetReplayId(JsonConvert.SerializeObject(payloadParent));
            _logHandler.HandleLog(logCommand);

            //Await the response 
            //ToDo: This needs to be handled correctly.
            //Await the response
            //Await the response
            var results = new GenericServiceResponse
            {
                Entity = "Account Place Holder - Nothing is Happening!",
                Success = true,
                RestResponseStatus = GenericServiceResponse.RestStatus.Success
            };
            //Log the response
            logCommand.LogMessage =
                $"CustomerController.AccountAppointmentEventListenerUpdate completed. Output value End Of Controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }
        
        /// <param name="payloadParent"></param>
        /// <returns>CustomerEntity in GenericServiceResponse Entity property</returns>
        [Route("AccountEventListenerInsert", Name = "AccountEventListenerInsert")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Updates a Customer", typeof(LeadPayload))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "UpdateCustomer exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult> AccountEventListenerInsert(PayloadParent payloadParent)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"CustomerController.AccountAppointmentEventListenerInsert Starting input parameter payloadParent = {payloadParent}"
            };
            _logHandler.ReplayId = LoggingHelper.GetReplayId(JsonConvert.SerializeObject(payloadParent));

            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = await _leadService.ConvertLeadToAccount(payloadParent, logCommand);

            //Log the response
            logCommand.LogMessage =
                $"CustomerController.AccountAppointmentEventListenerInsert completed. Output value End of controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }

        /// <summary>
        /// Gets a LeadEntity by LeadGuid
        /// </summary>
         /// <returns>LeadEntity in GenericServiceResponse Entity property</returns>
        [Route("GetMeAGuid", Name = "GetMeAGuid")]
        //[Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "It Get's Me a Guid... ", typeof(AGuidForYou))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "UpdateLead exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult> GetMeAGuid()
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"LeadController.GetMeAGuid Starting - No input Parameter"
            };
            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = await _leadService.GetMeAGuidPlease(logCommand);
            //Log the response
            logCommand.LogMessage =
                $"LeadController.GetMeAGuid completed. Output value End of controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }

        /// <summary>
        /// Gets a LeadEntity by LeadGuid
        /// </summary>
        /// <param name="peLeadDataForUpdate"></param>
        /// <returns>LeadEntity in GenericServiceResponse Entity property</returns>
        [Route("LeadEventListenerInsert", Name = "LeadEventListenerInsert")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Adds SF Account ID to Account Table", typeof(LeadPayload))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "UpdateLead exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult> LeadEventListenerInsert(PayloadParent peLeadDataForUpdate)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"LeadController.LeadEventListenerInsert Starting input parameter CDCLeadDataForUpdate = {peLeadDataForUpdate}"
            };
            _logHandler.ReplayId = LoggingHelper.GetReplayId(JsonConvert.SerializeObject(peLeadDataForUpdate));

            _logHandler.HandleLog(logCommand);

            //Await the response 
            //ToDo: This needs to be handled correctly.
            //Await the response
            var results = await _leadService.UpsertLeadFromInsertEvent(peLeadDataForUpdate, logCommand);

            //Log the response
            logCommand.LogMessage =
                $"LeadController.LeadEventListenerCreate completed. Output value End Of Controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }
    }
}