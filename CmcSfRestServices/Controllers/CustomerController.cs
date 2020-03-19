using log4net;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.Services.Interfaces;
using Newtonsoft.Json;
using System.Net;
using Swashbuckle.Swagger.Annotations;
using BusinessLogic.Services;
using Newtonsoft.Json.Linq;
using Shared.Commands;
using Shared.EventPayloads;
using Shared.Handlers;

namespace CmcSfRestServices.Controllers
{
    /// <summary>
    /// CustomerController constructor with dependencies (params) shown below
    /// </summary>
    public class CustomerController : BaseApiController
    {
        private readonly ICustomerService _CustomerService;
        private readonly ILog _loggingInstance;

        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;
        //private readonly HttpContextBase _httpContextBase;

        //private Guid UserGuid { get; set; }

        /// <summary>
        /// CustomerController constructor with dependencies (params) shown below
        /// </summary>
        /// <param name="CustomerService"></param>
        /// <param name="loggingInstance"></param>
        /// <param name="loghandler"></param>
        public CustomerController(ICustomerService CustomerService, ILog loggingInstance,
                LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _CustomerService = CustomerService;
             _loggingInstance = loggingInstance;
            _logHandler = loghandler;
        }
        /// <param name="CDCPayload"></param>
        /// <returns>CustomerEntity in GenericServiceResponse Entity property</returns>
        [Route("Intake_SurveyEventListenerInsert", Name = "Intake_SurveyEventListenerInsert")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Updates a Customer", typeof(LeadPayload))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "UpdateCustomer exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public  IHttpActionResult Intake_SurveyEventListenerInsert(JObject CDCPayload)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"CustomerController.ContactEventListenerInsert Starting input parameter CDCPayload = {CDCPayload}"
            };
            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = new GenericServiceResponse
            {
                Entity = "Temp Place Holder",
                Success = true,
                RestResponseStatus = GenericServiceResponse.RestStatus.Success
            };

            //Log the response
            logCommand.LogMessage =
                $"CustomerController.ContactEventListenerInsert completed. Output value End of controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }

        /// <param name="CDCPayload"></param>
        /// <returns>CustomerEntity in GenericServiceResponse Entity property</returns>
        [Route("Intake_SurveyEventListenerUpdate", Name = "Intake_SurveyEventListenerUpdate")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Updates a Customer", typeof(LeadPayload))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "UpdateCustomer exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public IHttpActionResult Intake_SurveyEventListenerUpdate(JObject CDCPayload)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"CustomerController.ContactEventListenerInsert Starting input parameter CDCPayload = {CDCPayload}"
            };
            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = new GenericServiceResponse
            {
                Entity = "Temp Place Holder",
                Success = true,
                RestResponseStatus = GenericServiceResponse.RestStatus.Success
            };
            //Log the response
            logCommand.LogMessage =
                $"CustomerController.ContactEventListenerInsert completed. Output value End of controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }
        /// <param name="CDCPayload"></param>
        /// <returns>CustomerEntity in GenericServiceResponse Entity property</returns>
        [Route("ServiceAppointmentEventListenerInsert", Name = "ServiceAppointmentEventListenerInsert")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Updates a Customer", typeof(LeadPayload))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "UpdateCustomer exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public  IHttpActionResult ServiceAppointmentEventListenerInsert(JObject CDCPayload)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"CustomerController.ContactEventListenerInsert Starting input parameter CDCPayload = {CDCPayload}"
            };
            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = new GenericServiceResponse
            {
                Entity = "Temp Place Holder",
                Success = true,
                RestResponseStatus = GenericServiceResponse.RestStatus.Success
            };
            //Log the response
            logCommand.LogMessage =
                $"CustomerController.ContactEventListenerInsert completed. Output value End of controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
         }
        
        
       
        /// <param name="payloadResponse"></param>
        /// <returns>CustomerEntity in GenericServiceResponse Entity property</returns>
        [Route("TaskEventListenerInsert", Name = "TaskEventListenerInsert")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Updates a Customer", typeof(LeadPayload))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "TaskEventListenerInsert exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult> TaskEventListenerInsert(PayloadParent payloadResponse)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"CustomerController.TaskEventListenerInsert Starting input parameter PayloadParent = {JsonConvert.SerializeObject(payloadResponse)}"
            };
            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = await _CustomerService.TaskEventInsertUpdate(payloadResponse, logCommand);
            //Log the response
            logCommand.LogMessage =
                $"CustomerController.TaskEventListenerInsert completed. Output value End of controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }


        /// <param name="payloadResponse"></param>
        /// <returns>CustomerEntity in GenericServiceResponse Entity property</returns>
        [Route("TaskEventListenerDelete", Name = "TaskEventListenerDelete")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "TaskEventListenerDelete", typeof(LeadPayload))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "TaskEventListenerInsert exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult> TaskEventListenerDelete(PayloadParent payloadResponse)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"CustomerController.TaskEventListenerDelete Starting input parameter PayloadParent = {JsonConvert.SerializeObject(payloadResponse)}"
            };
            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = await _CustomerService.TaskEventInsertUpdate(payloadResponse, logCommand);
            //Log the response
            logCommand.LogMessage =
                $"CustomerController.TaskEventListenerDelete completed. Output value End of controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }

        /// <param name="payloadResponse"></param>
        /// <returns>CustomerEntity in GenericServiceResponse Entity property</returns>
        [Route("TaskEventListenerUpdate", Name = "TaskEventListenerUpdate")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Adds SF Account ID to Account Table", typeof(LeadPayload))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "TaskEventListenerUpdate exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult> TaskEventListenerUpdate(PayloadParent payloadResponse)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"CustomerController.TaskEventListenerUpdate Starting input parameter TaskEventListenerUpdate = {JsonConvert.SerializeObject(payloadResponse)}"
            };
            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = await _CustomerService.TaskEventInsertUpdate(payloadResponse, logCommand);

            //Log the response
            logCommand.LogMessage =
                $"CustomerController.TaskEventListenerUpdate completed. Output value End Of controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }
        
        /// <param name="CDCPayload"></param>
        /// <returns>CustomerEntity in GenericServiceResponse Entity property</returns>
        [Route("ContactEventListenerInsert", Name = "ContactEventListenerInsert")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Updates a Customer", typeof(LeadPayload))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "UpdateCustomer exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult> ContactEventListenerInsert(JObject CDCPayload)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"CustomerController.ContactEventListenerInsert Starting input parameter CDCPayload = {CDCPayload}"
            };
            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = await _CustomerService.CdcEventContactCreate(CDCPayload, logCommand);
            //Log the response
            logCommand.LogMessage =
                $"CustomerController.ContactEventListenerInsert completed. Output value End of controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }
        
        /// <param name="CDCPayload"></param>
        /// <returns>CustomerEntity in GenericServiceResponse Entity property</returns>
        [Route("ContactEventListenerUpdate", Name = "ContactEventListenerUpdate")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Adds SF Account ID to Account Table", typeof(LeadPayload))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "UpdateCustomer exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult> ContactEventListenerUpdate(JObject CDCPayload)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"CustomerController.ContactEventListenerUpdate Starting input parameter CDCCustomerDataForUpdate = {CDCPayload}"
            };
            _logHandler.HandleLog(logCommand);

            //Await the response 
            //ToDo: This needs to be handled correctly.
            //Await the response
            var results = await _CustomerService.CdcEventContactUpdate(CDCPayload, logCommand); 

            //Log the response
            logCommand.LogMessage =
                $"CustomerController.ContactEventListenerUpdate completed. Output value End Of COntroller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }

        /// <param name="payloadParent"></param>
        /// <returns>CustomerEntity in GenericServiceResponse Entity property</returns>
        [Route("AddressEventListenerInsert", Name = "AddressEventListenerInsert")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Updates a Address", typeof(PayloadParent))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "UpdateCustomer exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult> AddressEventListenerInsert(PayloadParent payloadParent)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"CustomerController.AddressEventListenerInsert Starting input parameter payloadParent = {payloadParent}"
            };
            _logHandler.HandleLog(logCommand);

            //Await the response
            var results = await _CustomerService.CdcEventAddressCreate(payloadParent, logCommand); 
            //Log the response
            logCommand.LogMessage =
                $"CustomerController.AddressEventListenerInsert completed. Output value End of controller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }
        
        /// <param name="payloadParent"></param>
        /// <returns>CustomerEntity in GenericServiceResponse Entity property</returns>
        [Route("AddressEventListenerUpdate", Name = "AddressEventListenerUpdate")]
        [Authorize(Roles = "SalesforceEnvDev, SalesforceRoleAdmin ,SalesforceRoleScheduler")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "PayloadParent used to update an Address", typeof(PayloadParent))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,
            "UpdateCustomer exception see ServiceResponse.OperationException", typeof(GenericServiceResponse))]

        public async Task<IHttpActionResult> AddressEventListenerUpdate(PayloadParent payloadParent)
        {
            //UserGuid = GetUserGuidFromHttpContextBase(_httpContextBase);
            //if (UserGuid == Guid.Empty) return new HttpActionResult(HttpStatusCode.Unauthorized, "User Validation Error");

            //Log the request
            var logCommand = new LogCommand
            {
                User = User,
                LoggingInstance = _loggingInstance,
                LogMessage = $"CustomerController.AddressEventListenerUpdate Starting input parameter payloadParent = {payloadParent}"
            };
            _logHandler.HandleLog(logCommand);

            //Await the response 
            //ToDo: This needs to be handled correctly.
            //Await the response
            var results = await _CustomerService.CdcEventAddressUpdate(payloadParent, logCommand);

            //Log the response
            logCommand.LogMessage =
                $"CustomerController.AddressEventListenerUpdate completed. Output value End Of COntroller";
            _logHandler.HandleLog(logCommand);

            //Return the results
            return ReturnFormattedResults(results);
        }
    }
}