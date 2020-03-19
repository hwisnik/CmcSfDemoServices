using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Services.Interfaces;
using DataAccess.SFInterfaces;
using log4net;
using Shared.Commands;
using Shared.Entities.CDCClient;
using Shared.Entities.DTO.Scheduling;
using Shared.EventPayloads;
using Shared.Handlers;
using Shared.Logger;

namespace BusinessLogic.Services
{
    public class SchedulingService : ISchedulingService
    {
        private readonly ISchedulingRepository _schedulingRepository;
        private readonly IProgramRepository _programRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICdcRepository _cdcRepository;

        private readonly ILog _loggingInstance;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;
        private const string Service = "SchedulingService";
        private GenericServiceResponse ServiceResponse { get; set; }

        public SchedulingService(
                  ISchedulingRepository schedulingRepository
                , IProgramRepository programRepository
                , ICustomerRepository customerRepository
                , ICdcRepository cdcRepository
                , ILog loggingInstance, LoggingCommandHandlerDecorator<LogCommand> loghandler
            )
        {

            _schedulingRepository = schedulingRepository ?? throw new ArgumentNullException($"schedulingRepository repository instance is null");
            _programRepository = programRepository ?? throw new ArgumentNullException($"programRepository repository instance is null");
            _customerRepository = customerRepository ?? throw new ArgumentNullException($"customerRepository repository instance is null");
            _cdcRepository = cdcRepository ?? throw new ArgumentNullException($"cdcRepository repository instance is null");
            _loggingInstance = loggingInstance ?? throw new ArgumentNullException($"logging instance is null");
            _logHandler = loghandler ?? throw new ArgumentNullException($"logging commandhandlerDecorator instance is null");
        }

        public Task<GenericServiceResponse> GetSchedulingList(LogCommand logCommand)
        {
            throw new NotImplementedException();
        }
        public async Task<GenericServiceResponse> SimpleServiceAppointmentHandler(PayloadParent responseObject, LogCommand logCommand)
        {
            const string method = "ServiceAppointmentUpdate";
            var updateEventState = string.Empty;
            try
            {
                logCommand.LogMessage = $"{Service}.{method} Starting input parameter ResponseObject = {responseObject}";
                _logHandler.HandleLog(logCommand);

                var serviceAppointmentPayload = new ServiceAppointmentPayload(responseObject);

                logCommand.LogMessage = $"{Service}.{method} Starting input parameter serviceAppointmentPayload = {serviceAppointmentPayload}";
                _logHandler.HandleLog(logCommand);

                Guid programGuid;

                //ProgramGuid
                updateEventState = "Attempting To Get Program Guid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(serviceAppointmentPayload._PayloadHeader.SalesforceProgramIDC))
                {
                    var program = await _programRepository.GetProgramFromSFID(serviceAppointmentPayload._PayloadHeader.SalesforceProgramIDC, logCommand);
                    programGuid = program.ProgramGuid;
                }
                else
                {
                    //SubProgramGuid	ProgramGuid
                    // programGuid = Guid.Parse("8CC20C6D-13C2-424B-9EB3-194F653CC778");
                    throw new Exception($"Error: {Service}.{method} -- Missing SalesforceProgramID ");
                }

                ////SubprogramGuid
                //updateEventState = "Attempting To Get SubProgram Guid" + Environment.NewLine;
                //if (!string.IsNullOrEmpty(serviceAppointmentPayload._PayloadHeader.SalesforceSubProgramIDC))
                //{
                //    var subprogram = await _programRepository.GetSubProgramSFID(serviceAppointmentPayload._PayloadHeader.SalesforceSubProgramIDC, logCommand);
                //    subprogramGuid = subprogram.SubProgramGuid;
                //}
                //else
                //{
                //    subprogramGuid = Guid.Parse("FB02657F-3985-4F06-84C3-1669BE2F2991");
                //}
                updateEventState = "Attempting to Check Dates" + Environment.NewLine;
                if (serviceAppointmentPayload._PayloadBody.ServiceAppointment.SchedStartTime == null)
                {
                    throw new Exception($"Error: {Service}.{method} -- Missing SchedStartTime ");
                }
                var scheduleStartTime = (DateTime)serviceAppointmentPayload._PayloadBody.ServiceAppointment.SchedStartTime; 
                
                if (!DateTime.TryParse(serviceAppointmentPayload._PayloadBody.ServiceAppointment.LastModifiedDate,
                    out var lastModifiedDate))
                {
                    lastModifiedDate = DateTime.Now;
                }

                updateEventState = "Attempting to Map dataObject" + Environment.NewLine;
                var schedulingDataObject = new SimpleScheduling
                {
                    SfRecordID = serviceAppointmentPayload._PayloadHeader.RecordIdC,
                    ProgramGuid = programGuid,
                    SalesforceUser = serviceAppointmentPayload._PayloadBody.ServiceAppointment.LastModifiedById,
                    Technician = serviceAppointmentPayload._PayloadBody.ServiceAppointment.TechnicianGuidC,
                    WorkOrderType = serviceAppointmentPayload._PayloadBody.WorkOrder.WorkTypeId,
                    ScheduleBit = true,
                    ScheduleDateTime = scheduleStartTime,
                    LastModifiedDateTime = lastModifiedDate
                };

                if (string.IsNullOrEmpty(schedulingDataObject.SfRecordID)
                    || string.IsNullOrEmpty(schedulingDataObject.WorkOrderType)
                    || string.IsNullOrEmpty(schedulingDataObject.SalesforceUser))
                {
                    throw new Exception($"Error: {Service}.{method} -- Missing required fields");
                }
                    
                updateEventState = "Attempting to Execute First" + Environment.NewLine;
                var recordsChanged = await _schedulingRepository.CreateSimpleSchedulingRecordFirst(schedulingDataObject, logCommand);

                if (recordsChanged > 0)
                {

                    ServiceResponse = new GenericServiceResponse
                    {
                        Entity = $"Record Created in Simple Scheduling, rows affected = {recordsChanged}",
                        Success = true,
                        RestResponseStatus = GenericServiceResponse.RestStatus.Success
                    };

                    logCommand.LogMessage = string.Format($"{Service}.{method} completed");
                    _logHandler.HandleLog(logCommand);

                    return ServiceResponse;
                }

                updateEventState = "Attempting to get List" + Environment.NewLine;
                var simpleSchedulingList =
                    (await _schedulingRepository.GetSimpleScheduling(schedulingDataObject.SfRecordID, logCommand)).ToList();
                updateEventState = "Attempting to start reschedule logic" + Environment.NewLine;
                var simpleSchedulingList0 = simpleSchedulingList.Where(a => a.ScheduleBit == false).ToList();
                if (simpleSchedulingList0.Any())
                {
                    updateEventState = "Attempting to Rescheduled [1] " + Environment.NewLine;
                    var firstOrDefault = simpleSchedulingList0.OrderByDescending(a => a.SSID).FirstOrDefault();
                    if (
                            firstOrDefault != null && (schedulingDataObject.SalesforceUser != firstOrDefault.SalesforceUser
                                                       && schedulingDataObject.LastModifiedDateTime > firstOrDefault.LastModifiedDateTime.AddMinutes(+5)
                                                       &&
                                                       (schedulingDataObject.WorkOrderType != firstOrDefault.WorkOrderType
                                                        || schedulingDataObject.ScheduleDateTime != firstOrDefault.ScheduleDateTime
                                                        || schedulingDataObject.Technician != firstOrDefault.Technician))
                            )
                    {
                        recordsChanged = await _schedulingRepository.CreateSimpleSchedulingRecordReschedule(schedulingDataObject,
                            logCommand);
                    }
                }
                else
                {
                    updateEventState = "Attempting to Rescheduled [2] " + Environment.NewLine;
                    var firstOrDefault = simpleSchedulingList.OrderByDescending(a => a.SSID)
                        .FirstOrDefault(a => a.ScheduleBit);
                    if (firstOrDefault == null)
                    {
                        throw new Exception($"Error: {Service}.{method} -- Missing Created Date Record on Reschedule ");
                    }
                    if (schedulingDataObject.SalesforceUser != firstOrDefault.SalesforceUser &&
                        schedulingDataObject.LastModifiedDateTime > firstOrDefault.LastModifiedDateTime.AddMinutes(+30))
                    {
                        recordsChanged = await _schedulingRepository.CreateSimpleSchedulingRecordReschedule(schedulingDataObject,
                            logCommand);
                    }
                }
                updateEventState = "Executions Complete" + Environment.NewLine;

                ServiceResponse = await InsertAuditRecordAsync(logCommand, responseObject,
                    schedulingDataObject.GetType().FullName,
                    serviceAppointmentPayload._PayloadBody.ServiceAppointment.LastModifiedById);
                if (!ServiceResponse.Success) return ServiceResponse;

                if (recordsChanged >= 1)
                {
                    ServiceResponse = new GenericServiceResponse
                    {
                        Entity = $"Successful: Update Scheduling Related Table, rows added/updated = {recordsChanged}",
                        Success = true,
                        RestResponseStatus = GenericServiceResponse.RestStatus.Success
                    };
                }
                else
                {
                    ServiceResponse = new GenericServiceResponse
                    {
                        Entity = $"Failure: Update Scheduling Related Table, rows added/updated = {recordsChanged}",
                        Success = true,
                        RestResponseStatus = GenericServiceResponse.RestStatus.Empty
                    };
                }

                logCommand.LogMessage = string.Format($"{Service}.{method} completed");
                _logHandler.HandleLog(logCommand);

                return ServiceResponse;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Point of Failure", updateEventState);
                AppLogger.LogException(_loggingInstance, ex.Message, ex);
                return ServiceHelper.SetErrorGenericServiceResponse(ex);
            }
        }
        public async Task<GenericServiceResponse> ServiceAppointmentUpdate(PayloadParent ResponseObject, LogCommand logCommand)
        {
            const string method = "ServiceAppointmentUpdate";
            var updateEventState = string.Empty;
            try
            {
                logCommand.LogMessage = $"{Service}.{method} Starting input parameter ResponseObject = {ResponseObject}";
                _logHandler.HandleLog(logCommand);

                var serviceAppointmentPayload = new ServiceAppointmentPayload(ResponseObject);

                logCommand.LogMessage = $"{Service}.{method} Starting input parameter serviceAppointmentPayload = {serviceAppointmentPayload}";
                _logHandler.HandleLog(logCommand);
                var recordsAffected = 0;

                Guid programGuid;
                Guid subprogramGuid;
                Guid WorkOrderScoreWeightingGuid;
                Guid StatusTypeGuid;
                Guid UsernameMappingGuid;

                //ProgramGuid
                updateEventState = "Attempting To Get Program Guid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(serviceAppointmentPayload._PayloadHeader.SalesforceProgramIDC))
                {
                    var program = await _programRepository.GetProgramFromSFID(serviceAppointmentPayload._PayloadHeader.SalesforceProgramIDC, logCommand);
                    programGuid = program.ProgramGuid;
                }
                else
                {
                    //SubProgramGuid	ProgramGuid
                    // programGuid = Guid.Parse("8CC20C6D-13C2-424B-9EB3-194F653CC778");
                    throw new Exception($"Error: {Service}.{method} -- Missing SalesforceProgramID ");
                }

                //SubprogramGuid
                updateEventState = "Attempting To Get SubProgram Guid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(serviceAppointmentPayload._PayloadHeader.SalesforceSubProgramIDC))
                {
                    var subprogram = await _programRepository.GetSubProgramSFID(serviceAppointmentPayload._PayloadHeader.SalesforceSubProgramIDC, logCommand);
                    subprogramGuid = subprogram.SubProgramGuid;
                }
                else
                {
                    subprogramGuid = Guid.Parse("FB02657F-3985-4F06-84C3-1669BE2F2991");
                }

                // Mapping SFUserid To AD Username
                updateEventState = "Attempting To Get Map Status GUid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(serviceAppointmentPayload._PayloadBody.ServiceAppointment.Status))
                {
                    var lkpSchedulingType = await _schedulingRepository.GetLkpSchedulingTypeEnum(logCommand);
                    var statusTypeMapping = lkpSchedulingType.FirstOrDefault(a => string.Equals(a.SchedulingTypeName, serviceAppointmentPayload._PayloadBody.ServiceAppointment.Status, StringComparison.CurrentCultureIgnoreCase));
                    if (statusTypeMapping != null)
                        StatusTypeGuid = statusTypeMapping.LkpSchedulingTypeGuid;
                    else
                    {
                        throw new Exception($"Error: {Service}.{method} -- Cannot Status GUid ");
                    }
                }
                else
                {
                    throw new Exception($"Error: {Service}.{method} -- Cannot Status GUid ");
                }

                // LKPWorkOrderScoreMapping Workorder WorkTypeId
                updateEventState = "Attempting To Get Map Work Order Type Guid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(serviceAppointmentPayload._PayloadBody.WorkOrder.WorkTypeId))
                {
                    var lkpAuditType = await _schedulingRepository.GetWorkOrderScoreWeightingEnum(logCommand);
                    var workOrderScoreMapping = lkpAuditType.FirstOrDefault(a => a.SalesforceRecordID == serviceAppointmentPayload._PayloadBody.WorkOrder.WorkTypeId);
                    if (workOrderScoreMapping != null)
                        WorkOrderScoreWeightingGuid = workOrderScoreMapping.WorkOrderScoreWeightingGuid;
                    else
                    {
                        throw new Exception($"Error: {Service}.{method} -- Cannot Map Work order Type ");
                    }
                }
                else
                {
                    throw new Exception($"Error: {Service}.{method} -- Cannot Map Work order Type ");
                }

                // LKPWorkOrderScoreMapping Workorder WorkTypeId
                updateEventState = "Attempting To Get Username Guid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(serviceAppointmentPayload._PayloadBody.ServiceAppointment.LastModifiedById))
                {
                    var usernameMappingList = await _schedulingRepository.GetUsernameMappingSFtoADEnum(logCommand);
                    var usernameMapping = usernameMappingList.FirstOrDefault(a => a.SalesForceUserRecordID == serviceAppointmentPayload._PayloadBody.ServiceAppointment.LastModifiedById);
                    if (usernameMapping != null)
                        UsernameMappingGuid = usernameMapping.UsernameMappingSFtoADGuid;
                    else
                    {
                        throw new Exception($"Error: {Service}.{method} -- Cannot Map Username ");
                    }
                }
                else
                {
                    throw new Exception($"Error: {Service}.{method} -- Cannot Map Username");
                }
                updateEventState = "Attempting Execute" + Environment.NewLine;


                var schedulingDataObject = new Scheduling()
                {
                    SubprogramGuid = subprogramGuid,
                    ProgramGuid = programGuid,
                    WorkOrderScoreWeightingGuid = WorkOrderScoreWeightingGuid,
                    UsernameMappingSFtoADGuid = UsernameMappingGuid,
                    CreatedDateTime = DateTime.Now,
                    LkpSchedulingTypeGuid = StatusTypeGuid,
                    SalesforceRecordID = serviceAppointmentPayload._PayloadHeader.RecordIdC
                };

                recordsAffected = await _schedulingRepository.CreateNewSchedulingRecord(schedulingDataObject, logCommand);

                ServiceResponse = await InsertAuditRecordAsync(logCommand, ResponseObject,
                    schedulingDataObject.GetType().FullName,
                    serviceAppointmentPayload._PayloadBody.ServiceAppointment.LastModifiedById);
                if (!ServiceResponse.Success) return ServiceResponse;

                updateEventState = "Executions Complete" + Environment.NewLine;


                ServiceResponse = new GenericServiceResponse
                {
                    Entity = $"Update Scheduling Related Tables Successful, rows added/updated = {recordsAffected}",
                    Success = true,
                    RestResponseStatus = GenericServiceResponse.RestStatus.Success
                };

                logCommand.LogMessage = string.Format($"{Service}.{method} completed");
                _logHandler.HandleLog(logCommand);

                return ServiceResponse;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Point of Failure", updateEventState);
                AppLogger.LogException(_loggingInstance, ex.Message, ex);
                return ServiceHelper.SetErrorGenericServiceResponse(ex);
            }
        }

        private async Task<GenericServiceResponse> InsertAuditRecordAsync(LogCommand logCommand, PayloadParent payload, string objectFullName, string lastModifiedBy)
        {
            var response = new GenericServiceResponse();
            var cdcAuditEnity = new CdcAuditEntity
            {
                AuditLogGuid = Guid.NewGuid(),
                CreatedById = payload.CreatedById,
                CreatedDate = payload.CreatedDate,
                EventType = payload.EventTypeC,
                ObjectType = objectFullName,
                LastModifiedById = lastModifiedBy,
                LastModifiedDate = DateTime.Now,
                RecordId = payload.RecordIdC,
            };
            var auditResponse = await _cdcRepository.InsertAuditRecord(cdcAuditEnity, logCommand);
            if (auditResponse == 1)
            {
                response.Success = true;
            }
            return response;
        }

    }
}
