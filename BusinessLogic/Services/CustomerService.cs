using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Services.Audit;
using BusinessLogic.Services.Interfaces;
using DataAccess.SFInterfaces;
//using DataAccess.SFInterfaces;
using Shared.Logger;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Commands;
using Shared.Entities.CDCClient;
using Shared.Entities.DTO.Customer;
using Shared.EventPayloads;
using Shared.Handlers;

namespace BusinessLogic.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IProgramRepository _programRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICdcRepository _cdcRepository;


        private readonly ILog _loggingInstance;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;
        private const string Service = "CustomerService";


        public CustomerService(
                IAddressRepository addressRepository
                , IProgramRepository programRepository
                , ICustomerRepository customerRepository
                , ICdcRepository cdcRepository
                , ILog loggingInstance, LoggingCommandHandlerDecorator<LogCommand> loghandler
            )
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException($"AddressRepository repository instance is null");
            _customerRepository = customerRepository ?? throw new ArgumentNullException($"customerRepository repository instance is null");
            _programRepository = programRepository ?? throw new ArgumentNullException($"programRepository repository instance is null");
            _cdcRepository = cdcRepository ?? throw new ArgumentNullException($"cdcRepository repository instance is null");

            _loggingInstance = loggingInstance ?? throw new ArgumentNullException($"logging instance is null");
            _logHandler = loghandler ?? throw new ArgumentNullException($"logging commandhandlerDecorator instance is null");
        }

        public async Task<GenericServiceResponse> CdcEventAddressCreate(PayloadParent cdcPayload, LogCommand logCommand)
        {
            const string method = "CdcEventAddressCreate";
            var UpdateEventState = string.Empty;
            try
            {
                logCommand.LogMessage = $"{Service}.{method} Starting input parameter cdcPayload = {cdcPayload}";
                _logHandler.HandleLog(logCommand);

                var addressPayload = new AddressPayload(cdcPayload);

                logCommand.LogMessage = $"{Service}.{method} Starting input parameter addressPayload = {addressPayload}";
                _logHandler.HandleLog(logCommand);
                var recordsAffected = 0;

                var response = new GenericServiceResponse
                {
                    Success = true,
                    RestResponseStatus = GenericServiceResponse.RestStatus.Empty
                };

                Guid programGuid;
                Guid subprogramGuid;
                Guid addressTypeGuid;
                Guid countyGuid;
                Guid ownershipGuid;

                string ownershipTypeName = "Rented";

                //Customer Guid Check
                UpdateEventState = "Attempting to check CustomerGuid" + Environment.NewLine;
                if (addressPayload._PayloadHeader.CmcCustomerGuidC == null ||
                    addressPayload._PayloadHeader.CmcCustomerGuidC == Guid.Empty)
                {
                    throw new Exception($"Error: {Service}.{method} -- Missing Customer Guid ");
                }

                //Address Record ID
                UpdateEventState = "Attempting To check Address Record ID Guid" + Environment.NewLine;
                if (string.IsNullOrEmpty(addressPayload._PayloadHeader.RecordIdC))
                {
                    throw new Exception($"Error: {Service}.{method} -- Missing Record ID, Required for Update ");
                }

                //ProgramGuid
                UpdateEventState = "Attempting To Get Program Guid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(addressPayload._PayloadHeader.SalesforceProgramIDC))
                {
                    var program = await _programRepository.GetProgramFromSFID(addressPayload._PayloadHeader.SalesforceProgramIDC, logCommand);
                    programGuid = program.ProgramGuid;
                }
                else
                {
                    throw new Exception($"Error: {Service}.{method} -- Missing SalesforceProgramID ");
                }

                //SubprogramGuid
                UpdateEventState = "Attempting To Get SubProgram Guid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(addressPayload._PayloadHeader.SalesforceSubProgramIDC))
                {
                    var subprogram = await _programRepository.GetSubProgramSFID(addressPayload._PayloadHeader.SalesforceSubProgramIDC, logCommand);
                    subprogramGuid = subprogram.SubProgramGuid;
                }
                else
                {
                    subprogramGuid = Guid.Parse("FB02657F-3985-4F06-84C3-1669BE2F2991");
                }

                //addressTypeGuid setting it to default for now. 
                UpdateEventState = "Attempting To Get addressTypeGuid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(addressPayload._PayloadBody.AddressTypeC))
                {
                    var lkpAccountType = await _addressRepository.GetLKPAddressTypeFromAddressTypeName(addressPayload._PayloadBody.AddressTypeC, logCommand);
                    addressTypeGuid = lkpAccountType.AddressTypeGuid;
                }
                else
                {

                    logCommand.LogMessage = string.Format($"Error: {Service}.{method} -- Missing addressTypeGuid ");
                    _logHandler.HandleLog(logCommand);

                    response.Entity = "Empty Address Type";
                    return response;
                }

                //CountyGuid setting it to default for now. 
                UpdateEventState = "Attempting To Get CountyGuid" + Environment.NewLine;
                if (addressPayload._PayloadBody.AddressTypeC.ToLower() == "service")
                {
                    if (!string.IsNullOrEmpty(addressPayload._PayloadBody.CountyC))
                    {
                        var lkpCounties = await _addressRepository.GetLkpCountyFromCountyDescription(addressPayload._PayloadBody.CountyC, logCommand);
                        countyGuid = lkpCounties.CountyGuid;
                    }

                    else
                    {
                        logCommand.LogMessage = string.Format($"Error: {Service}.{method} -- Missing CountyGuid ");
                        _logHandler.HandleLog(logCommand);

                        response.Entity = "Empty County";
                        return response;
                    }
                }
                else
                {
                    countyGuid = Guid.Parse("24C3CDAC-2BB8-46A9-AF92-70E36D0B37F7");
                }

                //OwnershipGuid setting it to default for now. 
                UpdateEventState = "Attempting To Get OwnershipGuid" + Environment.NewLine;
                if (ownershipTypeName == "Rented")
                {
                    var lkpOwnership = await _addressRepository.GetLkpOwnershipFromOwnershipType("Rented", logCommand);
                    ownershipGuid = lkpOwnership.OwnershipGuid;
                }
                else
                {

                    logCommand.LogMessage = string.Format($"Error: {Service}.{method} -- Missing OwnershipGuid ");
                    _logHandler.HandleLog(logCommand);
                }

                UpdateEventState = "Attempting To Build Object" + Environment.NewLine;
                var address = new Address(addressPayload, addressTypeGuid, countyGuid, ownershipGuid);

                //Update Phone
                UpdateEventState = "Attempting To Insert/Update" + Environment.NewLine;
                if (addressPayload._PayloadBody.Address_SourceC.ToLower() == "program")
                {
                    recordsAffected += await _addressRepository.UpdateProgramServiceAddress(address, logCommand);
                    if (recordsAffected > 0)
                    {
                        response = new GenericServiceResponse
                        {
                            Entity = $"Update Address Complete. Rows updated = {recordsAffected}",
                            Success = true,
                            RestResponseStatus = GenericServiceResponse.RestStatus.Success
                        };
                    }
                    else
                    {
                        response = new GenericServiceResponse
                        {
                            Entity = $"Failure - Could not find Record for update, {recordsAffected}. " +
                                     $"Records Affected with required fields: " +
                                     $"-- Customer Guid: {addressPayload._PayloadBody.CmcCustomerGuidC} " +
                                     $"-- Address Source: {addressPayload._PayloadBody.Address_SourceC} " +
                                     $"-- Address Type: {addressPayload._PayloadBody.AddressTypeC} " +
                                     $"-- AddressTypeGuid: {addressTypeGuid} ",
                            Success = false,
                            RestResponseStatus = GenericServiceResponse.RestStatus.Success
                        };
                    }
                }
                else
                {
                    recordsAffected += await _addressRepository.InsertAddress(address, logCommand);
                    if (recordsAffected > 0)
                    {
                        response = new GenericServiceResponse
                        {
                            Entity = $"Insert Address Related Tables Successful, rows added/updated = {recordsAffected}",
                            Success = true,
                            RestResponseStatus = GenericServiceResponse.RestStatus.Error
                        };
                    }
                    else
                    {
                        response = new GenericServiceResponse
                        {
                            Entity = $"Failure - Could not insert into Address Table",
                            Success = false,
                            RestResponseStatus = GenericServiceResponse.RestStatus.Error
                        };
                    }
                }

                logCommand.LogMessage = string.Format($"{Service}.{method} completed");
                _logHandler.HandleLog(logCommand);

                return response;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Point of Failure", UpdateEventState);
                AppLogger.LogException(_loggingInstance, ex.Message, ex);
                return ServiceHelper.SetErrorGenericServiceResponse(ex);
            }
        }

        public async Task<GenericServiceResponse> CdcEventAddressUpdate(PayloadParent PayloadResponse, LogCommand logCommand)
        {
            const string method = "UpdateLeadFromUpdateEvent";
            var UpdateEventState = string.Empty;
            try
            {
                logCommand.LogMessage = $"{Service}.{method} Starting input parameter PayloadResponse = {PayloadResponse}";
                _logHandler.HandleLog(logCommand);

                var addressPayload = new AddressPayload(PayloadResponse);

                logCommand.LogMessage = $"{Service}.{method} Starting input parameter addressPayload = {addressPayload}";
                _logHandler.HandleLog(logCommand);
                var recordsAffected = 0;

                var response = new GenericServiceResponse
                {
                    Success = true,
                    RestResponseStatus = GenericServiceResponse.RestStatus.Empty
                };

                Guid programGuid;
                Guid subprogramGuid;
                Guid addressTypeGuid;
                Guid countyGuid;
                Guid ownershipGuid;

                string ownershipTypeName = "Rented";
                //Customer Guid Check
                UpdateEventState = "Attempting to check CustomerGuid" + Environment.NewLine;
                if (addressPayload._PayloadHeader.CmcCustomerGuidC == null ||
                    addressPayload._PayloadHeader.CmcCustomerGuidC == Guid.Empty)
                {
                    throw new Exception($"Error: {Service}.{method} -- Missing Customer Guid ");
                }

                //Address Record ID
                UpdateEventState = "Attempting To check Address Record ID Guid" + Environment.NewLine;
                if (string.IsNullOrEmpty(addressPayload._PayloadHeader.RecordIdC))
                {
                    throw new Exception($"Error: {Service}.{method} -- Missing Record ID, Required for Update ");
                }

                //ProgramGuid
                UpdateEventState = "Attempting To Get Program Guid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(addressPayload._PayloadHeader.SalesforceProgramIDC))
                {
                    var program = await _programRepository.GetProgramFromSFID(addressPayload._PayloadHeader.SalesforceProgramIDC, logCommand);
                    programGuid = program.ProgramGuid;
                }
                else
                {
                    throw new Exception($"Error: {Service}.{method} -- Missing SalesforceProgramID ");
                }

                //SubprogramGuid
                UpdateEventState = "Attempting To Get SubProgram Guid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(addressPayload._PayloadHeader.SalesforceSubProgramIDC))
                {
                    var subprogram = await _programRepository.GetSubProgramSFID(addressPayload._PayloadHeader.SalesforceSubProgramIDC, logCommand);
                    subprogramGuid = subprogram.SubProgramGuid;
                }
                else
                {
                    subprogramGuid = Guid.Parse("FB02657F-3985-4F06-84C3-1669BE2F2991");
                }

                //addressTypeGuid setting it to default for now. 
                UpdateEventState = "Attempting To Get addressTypeGuid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(addressPayload._PayloadBody.AddressTypeC))
                {
                    var lkpAccountType = await _addressRepository.GetLKPAddressTypeFromAddressTypeName(addressPayload._PayloadBody.AddressTypeC, logCommand);
                    addressTypeGuid = lkpAccountType.AddressTypeGuid;
                }
                else
                {

                    logCommand.LogMessage = string.Format($"Error: {Service}.{method} -- Missing addressTypeGuid ");
                    _logHandler.HandleLog(logCommand);

                    return response;
                }

                //CountyGuid setting it to default for now. 
                UpdateEventState = "Attempting To Get CountyGuid" + Environment.NewLine;
                if (addressPayload._PayloadBody.AddressTypeC.ToLower() == "service")
                {
                    if (!string.IsNullOrEmpty(addressPayload._PayloadBody.CountyC))
                    {
                        var lkpCounties = await _addressRepository.GetLkpCountyFromCountyDescription(addressPayload._PayloadBody.CountyC, logCommand);
                        countyGuid = lkpCounties.CountyGuid;
                    }

                    else
                    {
                        logCommand.LogMessage = string.Format($"Error: {Service}.{method} -- Missing CountyGuid ");
                        _logHandler.HandleLog(logCommand);
                        return response;
                    }
                }
                else
                {
                    countyGuid = Guid.Parse("24C3CDAC-2BB8-46A9-AF92-70E36D0B37F7");
                }

                //CountyGuid setting it to default for now. 
                UpdateEventState = "Attempting To Get OwnershipGuid" + Environment.NewLine;
                if (ownershipTypeName == "Rented")
                {
                    var lkpOwnership = await _addressRepository.GetLkpOwnershipFromOwnershipType("Rented", logCommand);
                    ownershipGuid = lkpOwnership.OwnershipGuid;
                }
                else
                {

                    logCommand.LogMessage = string.Format($"Error: {Service}.{method} -- Missing OwnershipGuid ");
                    _logHandler.HandleLog(logCommand);
                }

                var address = new Address(addressPayload, addressTypeGuid, countyGuid, ownershipGuid);

                //Even though this is an address update PE message, it may be the initial insert of a landlord mailing address
                if (addressTypeGuid == Guid.Parse("431407C6-C889-48BF-9DFC-7CD879F85847")) //Landlord Mailing
                {
                    var existingLlAddress = await _addressRepository.GetAddressBySfIdAndAddressType(address, logCommand);
                    if (!existingLlAddress.Any())
                    {
                        recordsAffected += await _addressRepository.InsertAddress(address, logCommand);
                    }
                    else
                    {
                        //Update Address
                        recordsAffected += await _addressRepository.UpdateAddress(address, logCommand);
                    }

                    if (recordsAffected > 0)
                        {
                            response = new GenericServiceResponse
                            {
                                Entity = $"Update Address Related Tables Successful, rows added/updated = {recordsAffected}",
                                Success = true,
                                RestResponseStatus = GenericServiceResponse.RestStatus.Success
                            };
                        }
                        else
                        {
                            response = new GenericServiceResponse
                            {
                                Entity = $"Address Does not Exist. Records Changed: {recordsAffected}. Could not update. ",
                                Success = false,
                                RestResponseStatus = GenericServiceResponse.RestStatus.Success
                            };
                        }
                   
                }

                logCommand.LogMessage = string.Format($"{Service}.{method} completed");
                _logHandler.HandleLog(logCommand);

                return response;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Point of Failure", UpdateEventState);
                AppLogger.LogException(_loggingInstance, ex.Message, ex);
                return ServiceHelper.SetErrorGenericServiceResponse(ex);
            }

        }

        public Task<GenericServiceResponse> CdcEventContactCreate(JObject cdcPayload, LogCommand logCommand)
        {
            const string method = "CDCEventContactCreate";

            try
            {
                logCommand.LogMessage = $"{Service}.{method} Starting input parameter CDCPayload = {cdcPayload}";
                _logHandler.HandleLog(logCommand);


                var response = new GenericServiceResponse
                {
                    Entity = $"Ignoring Contact ",
                    Success = true,
                    RestResponseStatus = GenericServiceResponse.RestStatus.Success
                };


                logCommand.LogMessage = string.Format($"{Service}.{method} completed");
                _logHandler.HandleLog(logCommand);

                return Task.Run(() => response);
            }
            catch (Exception ex)
            {
                AppLogger.LogException(_loggingInstance, ex.Message, ex);
                return Task.Run(() => ServiceHelper.SetErrorGenericServiceResponse(ex));
            }
        }

        public Task<GenericServiceResponse> CdcEventContactUpdate(JObject cdcPayload, LogCommand logCommand)
        {
            const string method = "CDCEventContactUpdate";

            try
            {
                logCommand.LogMessage = $"{Service}.{method} Starting input parameter CDCPayload = {cdcPayload}";
                _logHandler.HandleLog(logCommand);


                var response = new GenericServiceResponse
                {
                    Entity = $"Ignoring Contact ",
                    Success = true,
                    RestResponseStatus = GenericServiceResponse.RestStatus.Success
                };


                logCommand.LogMessage = string.Format($"{Service}.{method} completed");
                _logHandler.HandleLog(logCommand);

                return Task.Run(() => response);
            }
            catch (Exception ex)
            {
                AppLogger.LogException(_loggingInstance, ex.Message, ex);
                return Task.Run(() => ServiceHelper.SetErrorGenericServiceResponse(ex));
            }
        }

        //Change Minor
        //TODO: HW This is temporary code Needs to be removed ASAP Hack to update Lead so that it will be excluded from GetPrioritizedLeads query if touched by user.
        public Task<GenericServiceResponse> TaskEventInsertUpdate(PayloadParent payloadResponse, LogCommand logCommand)
        {
            const string method = "TaskEventInsertUpdate";

            try
            {
                logCommand.LogMessage = $"{Service}.{method} Starting input parameter cdcPayload = {JsonConvert.SerializeObject(payloadResponse)}";
                _logHandler.HandleLog(logCommand);


                var response = AuditInsert.InsertAuditRecordAsync(logCommand, payloadResponse, "Shared.Entities.DTO.Customer.Lead", payloadResponse.CreatedById, _cdcRepository);
                logCommand.LogMessage = string.Format($"{Service}.{method} completed");
                _logHandler.HandleLog(logCommand);

                return response;
            }
            catch (Exception ex)
            {
                AppLogger.LogException(_loggingInstance, ex.Message, ex);
                return Task.Run(() => ServiceHelper.SetErrorGenericServiceResponse(ex));
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
