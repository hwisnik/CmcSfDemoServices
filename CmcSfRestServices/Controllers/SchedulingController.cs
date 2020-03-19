using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using log4net;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Entities.DTO.Scheduling;
using Shared.EventPayloads;
using Shared.Handlers;
using Shared.Logger;
using Swashbuckle.Swagger.Annotations;

namespace CmcSfRestServices.Controllers
{
    /// <summary>
    /// CustomerController constructor with dependencies (params) shown below
    /// </summary>
    public class SchedulingController : BaseApiController
    {
        private readonly ISchedulingService _SchedulingService;

        private readonly ILog _loggingInstance;
        private readonly string Controller = "SchedulingController";
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        /// <param name="SchedulingService"></param>
        /// <param name="loggingInstance"></param>
        /// <param name="loghandler"></param>
        public SchedulingController(ISchedulingService SchedulingService, ILog loggingInstance,
                LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _SchedulingService = SchedulingService;
            _loggingInstance = loggingInstance;
            _logHandler = loghandler;
        }

        /// <param name="payloadResponse"></param>
        [Route("ServiceAppointmentEventListenerUpdate", Name = "ServiceAppointmentEventListenerUpdate")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Scheduling Payload Handler for Update Event", typeof(PayloadParent))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "UpdateCustomer exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult>  ServiceAppointmentEventListenerUpdate(PayloadParent payloadResponse)
        {
            
            string ActionName = "ServiceAppointmentEventListenerUpdate";
            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"{Controller}.{ActionName} Starting input parameter PayloadParent = {JsonConvert.SerializeObject(payloadResponse)}"
            };
            _logHandler.ReplayId = LoggingHelper.GetReplayId(JsonConvert.SerializeObject(payloadResponse));

            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = await _SchedulingService.SimpleServiceAppointmentHandler(payloadResponse, logCommand);
            //Log the response
            logCommand.LogMessage =
                $"CustomerController.TaskEventListenerInsert completed. Output value End of controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }
        
        /// <returns>Scheduling List in GenericServiceResponse Entity property</returns>
        [Route("GetSchedulingList", Name = "GetSchedulingList")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Get A list Of Scheduling", typeof(Scheduling))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "UpdateCustomer exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult>  GetSchedulingList()
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");
            string ActionName = "GetSchedulingList";

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"{Controller}.{ActionName} Starting No input parameter"
            };
            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = await _SchedulingService.GetSchedulingList(logCommand);
            //Log the response
            logCommand.LogMessage =
                $"CustomerController.TaskEventListenerInsert completed. Output value End of controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }
    }
}