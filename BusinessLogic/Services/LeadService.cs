using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BusinessLogic.Services.Interfaces;
using Shared.Logger;

using log4net;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Handlers;
using System.Text.RegularExpressions;
using DataAccess.Repositories.Interfaces;
using Shared.Entities.CDCClient;
using Shared.Entities.DTO.Contact;
using Shared.Entities.DTO.Customer;
using Shared.Entities.DTO.ERMS;
using Shared.Entities.DTO.SchedEntities;
using Shared.Entities.DTO.Program;
using Shared.Entities.InputObjects.Lead;
using Shared.Entities.SFDb.Lead;
using Shared.Entities._SearchCriteria.Client;
using Shared.EventPayloads;
using Shared.Notifier;

namespace BusinessLogic.Services
{
    public class LeadService : ILeadService
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IERMSRepository _ermsRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IUsageRepository _usageRepository;
        private readonly IProgramRepository _programRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICdcRepository _cdcRepository;

        private readonly ILog _loggingInstance;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;
        private const string Service = "LeadService";
        private GenericServiceResponse ServiceResponse { get; set; }

        private static string DdErrorMessage { get; set; }

        private static double HomeBaseLat { get; set; }
        private static double HomeBaseLong { get; set; }

        public LeadService(
                ILeadRepository leadRepository
                , IERMSRepository ermsRepository
                , IContactRepository contactRepository
                , IAddressRepository addressRepository
                , IUsageRepository usageRepository
                , IProgramRepository programRepository
                , ICustomerRepository customerRepository
                , ICdcRepository cdcRepository
                , ILog loggingInstance, LoggingCommandHandlerDecorator<LogCommand> loghandler
            )
        {

            _leadRepository = leadRepository ?? throw new ArgumentNullException($"lead repository instance is null");
            _contactRepository = contactRepository ?? throw new ArgumentNullException($"ContactRepository repository instance is null");
            _ermsRepository = ermsRepository ?? throw new ArgumentNullException($"ermsRepository repository instance is null");
            _addressRepository = addressRepository ?? throw new ArgumentNullException($"AddressRepository repository instance is null");
            _usageRepository = usageRepository ?? throw new ArgumentNullException($"UsageRepository repository instance is null");
            _customerRepository = customerRepository ?? throw new ArgumentNullException($"customerRepository repository instance is null");
            _cdcRepository = cdcRepository ?? throw new ArgumentNullException($"cdcRepository instance is null");
            _programRepository = programRepository ?? throw new ArgumentNullException($"programRepository repository instance is null");
            _loggingInstance = loggingInstance ?? throw new ArgumentNullException($"logging instance is null");
            _logHandler = loghandler ?? throw new ArgumentNullException($"logging commandhandlerDecorator instance is null");
        }


        public async Task<GenericServiceResponse> GetLead(Guid leadGuid, LogCommand logCommand)
        {
            const string method = "GetLead";

            try
            {
                logCommand.LogMessage = string.Format($"{Service}.{method} Starting input parameter leadGuid = {0}", leadGuid);
                _logHandler.HandleLog(logCommand);

                var lead = await _leadRepository.GetLead(leadGuid, logCommand);
                var response = ServiceHelper.SetGenericServiceResponseForEntity(lead);
                response.Success = true;

                logCommand.LogMessage = string.Format($"{Service}.{method} completed");
                _logHandler.HandleLog(logCommand);

                return response;
            }
            catch (Exception ex)
            {
                AppLogger.LogException(_loggingInstance, ex.Message, ex);
                return ServiceHelper.SetErrorGenericServiceResponse(ex);
            }

        }

        public async Task<GenericServiceResponse> GetLead(SearchLeadSimple leadSearcher, LogCommand logCommand)
        {
            const string method = "GetLead";
            try
            {
                logCommand.LogMessage = string.Format($"{Service}.{method} Starting input parameter LeadSearcher = {JsonConvert.SerializeObject(0)}", leadSearcher);
                _logHandler.HandleLog(logCommand);

                var sfLeadList = await _leadRepository.GetSfLeads(leadSearcher, logCommand);
                var response = ServiceHelper.SetGenericServiceResponseForEntityList(sfLeadList);
                response.Success = true;

                logCommand.LogMessage = string.Format($"{Service}.{method} completed");
                _logHandler.HandleLog(logCommand);
                return response;

            }
            catch (Exception ex)
            {
                AppLogger.LogException(_loggingInstance, ex.Message, ex);
                return ServiceHelper.SetErrorGenericServiceResponse(ex);
            }

        }

    

        public async Task<GenericServiceResponse> GetLeadDetailed(Guid customerGuid, LogCommand logCommand)
        {
            const string method = "GetLeadDetailed";
            try
            {
                logCommand.LogMessage = string.Format($"{Service}.{method} Starting input parameter leadGuid = {0}", customerGuid);
                _logHandler.HandleLog(logCommand);

                //Initialize Detailed Object, Get Lead Data. 
                var sfLead = new SFDetailedLead
                {
                    LeadData = await _leadRepository.GetDetailedLead(customerGuid, logCommand),
                    CustomerData = await _customerRepository.GetDetailedCustomerFromGuid(customerGuid, logCommand),
                    CapStatus = await _usageRepository.GetIsCapQualifiedFromCustomerGuid(customerGuid, logCommand),

                    PremiseData = new List<SFDetailedPremise>()
                    {
                        new SFDetailedPremise()
                        {
                            AddressData =
                                await _addressRepository.GetDetailedAddressFromCustomerGuid(customerGuid, logCommand)
                        }
                    },

                    UsageData = await _usageRepository.GetUsageFromCustomerGuid(customerGuid, logCommand),
                    PrimaryContactData = new SFDetailedContact()
                    {
                        ContactData = await _contactRepository.GetPrimaryContactFromCustomerGuid(customerGuid,
                            logCommand)
                    }
                };

                // Primary Contact Data: Phones, Emails, MailingAddresses
                sfLead.PrimaryContactData.ContactPhones = await _contactRepository.GetPhoneFromContactGuid(sfLead.PrimaryContactData.ContactData.ContactGuid, logCommand);
                sfLead.PrimaryContactData.ContactEmails = await _contactRepository.GetEmailFromContactGuid(sfLead.PrimaryContactData.ContactData.ContactGuid, logCommand);
                //--------------------------------------------------------

                //Only Send Data On Initial Load. 
                if (string.IsNullOrEmpty(sfLead.LeadData.SFLeadID))
                {
                    sfLead.ERMSData = await _ermsRepository.GetERMSCustomer(customerGuid, logCommand);
                }
                else
                {
                    sfLead.ERMSData = new ERMSCustomer();
                }

                var response = ServiceHelper.SetGenericServiceResponseForEntity(sfLead);
                response.Success = true;

                logCommand.LogMessage = string.Format($"{Service}.{method} completed");
                _logHandler.HandleLog(logCommand);

                return response;

            }
            catch (Exception ex)
            {
                AppLogger.LogException(_loggingInstance, ex.Message, ex);
                return ServiceHelper.SetErrorGenericServiceResponse(ex);
            }

        }

        public async Task<GenericServiceResponse> ConvertLeadToAccount(PayloadParent responseObject, LogCommand logCommand)
        {
            const string method = "ConvertLeadToAccount";

            try
            {
                logCommand.LogMessage = string.Format($"{Service}.{method} Starting - ResponseObject: {responseObject}");
                _logHandler.HandleLog(logCommand);
                var rowsAffected = await _leadRepository.SetLeadStatusToConverted(responseObject.CmcCustomerGuidC, logCommand);

                logCommand.LogMessage = string.Format($"{Service}.{method} completed");
                _logHandler.HandleLog(logCommand);

                if (rowsAffected >= 1)
                {
                    return new GenericServiceResponse
                    {
                        Entity = $"Lead Status Set To Converted, rows added/updated = {rowsAffected}",
                        Success = true,
                        RestResponseStatus = GenericServiceResponse.RestStatus.Success
                    };
                }
                else
                {
                    return new GenericServiceResponse
                    {
                        Entity = $"Lead Status Not Converted, rows added/updated = {rowsAffected}",
                        Success = false,
                        RestResponseStatus = GenericServiceResponse.RestStatus.Error
                    };
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogException(_loggingInstance, ex.Message, ex);
                return ServiceHelper.SetErrorGenericServiceResponse(ex);
            }
        }

        public async Task<GenericServiceResponse> UpdateLeadFromUpdateEvent(PayloadParent payloadResponse, LogCommand logCommand)
        {
            const string method = "UpdateLeadFromUpdateEvent";
            var updateEventState = string.Empty;
            try
            {
                logCommand.LogMessage = $"{Service}.{method} Starting input parameter PayloadResponse = {payloadResponse}";
                _logHandler.HandleLog(logCommand);


                var leadPayload = new LeadPayload(payloadResponse);

                logCommand.LogMessage = $"{Service}.{method} Starting input parameter LeadPayload = {leadPayload}";
                _logHandler.HandleLog(logCommand);
                var recordsAffected = 0;

                var existingLeadEnumerable = await _leadRepository.GetLeadByCustomerGuidAsync(leadPayload._PayloadHeader.CmcCustomerGuidC, logCommand);
                var existingLeadList = existingLeadEnumerable.ToList();
                if (!existingLeadList.Any())
                {
                    throw new Exception($"Attempting to update Lead where CustomerGuid =  {leadPayload._PayloadHeader.CmcCustomerGuidC} but lead was not found ");
                }
                var existingLead = existingLeadList[0];

                Guid programGuid;
                Guid subprogramGuid;
                Guid lkpLeadStatusGuid;
                Guid lkpAuditTypeGuid;
                Guid accountTypeGuid;

                //Customer Guid Check
                updateEventState = "Attempting to check CustomerGuid" + Environment.NewLine;
                if (leadPayload._PayloadHeader.CmcCustomerGuidC == Guid.Empty)
                {
                    throw new Exception($"Error: {Service}.{method} -- Missing Customer Guid ");
                }

                //ProgramGuid
                updateEventState = "Attempting To Get Program Guid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(leadPayload._PayloadHeader.SalesforceProgramIDC))
                {
                    var program = await _programRepository.GetProgramFromSFID(leadPayload._PayloadHeader.SalesforceProgramIDC, logCommand);
                    programGuid = program.ProgramGuid;
                }
                else
                {
                    throw new Exception($"Error: {Service}.{method} -- Missing SalesforceProgramID ");
                }

                //SubprogramGuid
                updateEventState = "Attempting To Get SubProgram Guid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(leadPayload._PayloadHeader.SalesforceSubProgramIDC))
                {
                    var subprogram = await _programRepository.GetSubProgramSFID(leadPayload._PayloadHeader.SalesforceSubProgramIDC, logCommand);
                    subprogramGuid = subprogram.SubProgramGuid;
                }
                else
                {
                    subprogramGuid = Guid.Parse("FB02657F-3985-4F06-84C3-1669BE2F2991");
                }

                //LKPLeadStatusGuid
                updateEventState = "Attempting To Get LKPLeadStatus Guid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(leadPayload._PayloadBody.Status))
                {
                    var lkPleadStatus = await _leadRepository.GetLkpLeadStatusFromName(leadPayload._PayloadBody.Status, logCommand);
                    lkpLeadStatusGuid = lkPleadStatus.LeadStatusGuid;
                }
                else
                {
                    throw new Exception($"Error: {Service}.{method} -- Missing LKPLeadStatus ");
                }

                //LKPAuditTypeGuid
                updateEventState = "Attempting To Get LKPAuditType Guid" + Environment.NewLine;
                if (!string.IsNullOrEmpty(leadPayload._PayloadBody.AuditTypeC))
                {
                    var lkpAuditType = await _leadRepository.GetLkpLeadAuditTypeFromName(leadPayload._PayloadBody.AuditTypeC, logCommand);
                    lkpAuditTypeGuid = lkpAuditType.AuditTypeGuid;
                }
                else
                {
                    throw new Exception($"Error: {Service}.{method} -- Missing LKPAuditType ");
                }

                updateEventState = "Attempting To Instantiate customer" + Environment.NewLine;
                var customer = new Customer(leadPayload, programGuid, subprogramGuid, accountTypeGuid);

                //Get Primary Contact Guid
                updateEventState = "Attempting To Instantiate primaryContact" + Environment.NewLine;
                var primaryContact = new Contact(leadPayload, Guid.Parse("BEF01936-5D80-471D-AF77-4CC2AE5161B4"));
                //primaryContact.CustomerGuid = Guid.Parse("A30FD8B1-9BE2-47FB-8D9E-AC8EFCEFE369");

                updateEventState = "Attempting To Get Primary Contact" + Environment.NewLine;
                var primaryContactData = await _contactRepository.GetPrimaryContactFromCustomerGuid(leadPayload._PayloadHeader.CmcCustomerGuidC, logCommand);


                //Customer
                updateEventState = "Attempting To Execute on Customer" + Environment.NewLine;
                recordsAffected += await _customerRepository.UpdateCustomer(customer, logCommand);
                ServiceResponse = await InsertAuditRecordAsync(logCommand, payloadResponse, customer.GetType().FullName, leadPayload._PayloadBody.LastModifiedById);
                if (!ServiceResponse.Success) return ServiceResponse;

                //Lead
                updateEventState = "Attempting To Instantiate lead" + Environment.NewLine;
                var lead = new Lead(leadPayload, lkpLeadStatusGuid, lkpAuditTypeGuid);

                //Update Lead Status Change Date timestamp if lead status changed
                if (existingLead.LeadStatusGuid == lead.LeadStatusGuid)
                {
                    if (lead.LeadStatusChangeDate == null)
                    {
                        lead.LeadStatusChangeDate = Convert.ToDateTime("1900-01-01");
                    }
                }
                else
                {
                    lead.LeadStatusChangeDate = DateTime.Now.Date;
                }

                //Set ReserveDate to Today.23:59.59 if reservation is done by SF.  See GetPrioLeads which sets
                //Reservation for leads to now + 15 minutes as a way to send different sets of leads to CSR's
                //that are looking for leads with similar criteria
                if (lead.ReservedDate == DateTime.Today)
                {
                    lead.ReservedDate = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);
                }

                updateEventState = "Attempting To Execute on Lead" + Environment.NewLine;
                recordsAffected += await _leadRepository.UpdateLeadAsync(lead, logCommand);
                ServiceResponse = await InsertAuditRecordAsync(logCommand, payloadResponse, lead.GetType().FullName, leadPayload._PayloadBody.LastModifiedById);
                if (!ServiceResponse.Success) return ServiceResponse;

                //Update Primary Contact
                updateEventState = "Attempting To Execute on Primary Contact" + Environment.NewLine;
                recordsAffected += await _contactRepository.UpdatePrimaryContact(primaryContact, logCommand);
                ServiceResponse = await InsertAuditRecordAsync(logCommand, payloadResponse, primaryContact.GetType().FullName, leadPayload._PayloadBody.LastModifiedById);
                if (!ServiceResponse.Success) return ServiceResponse;

                //Phone and Email
                updateEventState = "Attempting To Instantiate Phone and Email Objects" + Environment.NewLine;
                var phone = new PhoneNumbers(leadPayload, primaryContactData.ContactGuid);
                var email = new Email(leadPayload, primaryContactData.ContactGuid);

                updateEventState = "Attempting To Execute on Phone" + Environment.NewLine;
                recordsAffected += await _contactRepository.UpdatePhone(phone, logCommand);
                ServiceResponse = await InsertAuditRecordAsync(logCommand, payloadResponse, phone.GetType().FullName, leadPayload._PayloadBody.LastModifiedById);
                if (!ServiceResponse.Success) return ServiceResponse;

                updateEventState = "Attempting To Execute on Email" + Environment.NewLine;
                recordsAffected += await _contactRepository.UpdateEmail(email, logCommand);
                ServiceResponse = await InsertAuditRecordAsync(logCommand, payloadResponse, email.GetType().FullName, leadPayload._PayloadBody.LastModifiedById);
                if (!ServiceResponse.Success) return ServiceResponse;

                updateEventState = "Executions Complete" + Environment.NewLine;


                ServiceResponse = new GenericServiceResponse
                {
                    Entity = $"Update Lead Related Tables Successful, rows added/updated = {recordsAffected}",
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



        public async Task<GenericServiceResponse> UpsertLeadFromInsertEvent(PayloadParent cdcResponseObject, LogCommand logCommand)
        {
            const string method = "UpsertLeadFromInsertEvent";
            string upsertEventState = string.Empty;

            try
            {
                logCommand.LogMessage = $"{Service}.{method} Starting input parameter CDCResponseObject = {cdcResponseObject}";
                _logHandler.HandleLog(logCommand);

                var checkIfExists = await _leadRepository.GetLeads(cdcResponseObject.CmcCustomerGuidC, logCommand);
                var leadPayload = new LeadPayload(cdcResponseObject);

                var recordsAffected = 0;
                string entityResponse;

                ServiceResponse = new GenericServiceResponse
                {
                    Success = true,
                    RestResponseStatus = GenericServiceResponse.RestStatus.Empty
                };

                if (checkIfExists.Any())
                {
                    recordsAffected = await _leadRepository.LeadCreatedSetRecordId(
                        leadPayload._PayloadHeader.CmcCustomerGuidC, leadPayload._PayloadHeader.RecordIdC, logCommand);

                    entityResponse = $"Lead Exists, Update SF-Record-ID, rows added/updated = {recordsAffected}" + Environment.NewLine +
                                     $"Records Expected: 1 ";
                }
                else
                {

                    Guid programGuid;
                    Guid subprogramGuid;
                    Guid lkpLeadStatusGuid;
                    Guid lkpAuditTypeGuid;
                    Guid accountTypeGuid;

                    //Customer Guid Check
                    upsertEventState = "Attempting to check CustomerGuid" + Environment.NewLine;
                    if (leadPayload._PayloadHeader.CmcCustomerGuidC == Guid.Empty)
                    {
                        throw new Exception($"Error: {Service}.{method} -- Missing Customer Guid ");
                    }

                    //ProgramGuid
                    upsertEventState = "Attempting To Get Program Guid" + Environment.NewLine;
                    if (!string.IsNullOrEmpty(leadPayload._PayloadHeader.SalesforceProgramIDC))
                    {
                        var program = await _programRepository.GetProgramFromSFID(leadPayload._PayloadHeader.SalesforceProgramIDC, logCommand);
                        programGuid = program.ProgramGuid;
                    }
                    else
                    {
                        throw new Exception($"Error: {Service}.{method} -- Missing SalesforceProgramID ");
                    }

                    //SubprogramGuid
                    upsertEventState = "Attempting To Get SubProgram Guid" + Environment.NewLine;
                    if (!string.IsNullOrEmpty(leadPayload._PayloadHeader.SalesforceSubProgramIDC))
                    {
                        var subprogram = await _programRepository.GetSubProgramSFID(leadPayload._PayloadHeader.SalesforceSubProgramIDC, logCommand);
                        subprogramGuid = subprogram.SubProgramGuid;
                    }
                    else
                    {
                        subprogramGuid = Guid.Parse("FB02657F-3985-4F06-84C3-1669BE2F2991");
                    }

                    //LKPLeadStatusGuid
                    upsertEventState = "Attempting To Get LKPLeadStatus Guid" + Environment.NewLine;
                    if (!string.IsNullOrEmpty(leadPayload._PayloadBody.Status))
                    {
                        var lkPleadStatus = await _leadRepository.GetLkpLeadStatusFromName(leadPayload._PayloadBody.Status, logCommand);
                        lkpLeadStatusGuid = lkPleadStatus.LeadStatusGuid;
                    }
                    else
                    {
                        logCommand.LogMessage = string.Format($"Error: {Service}.{method} -- Missing LKPLeadStatus ");
                        _logHandler.HandleLog(logCommand);

                        return ServiceResponse;
                    }

                    //LKPAuditTypeGuid
                    upsertEventState = "Attempting To Get LKPAuditType Guid" + Environment.NewLine;
                    if (!string.IsNullOrEmpty(leadPayload._PayloadBody.AuditTypeC))
                    {
                        var lkpAuditType = await _leadRepository.GetLkpLeadAuditTypeFromName(leadPayload._PayloadBody.AuditTypeC, logCommand);
                        lkpAuditTypeGuid = lkpAuditType.AuditTypeGuid;
                    }
                    else
                    {
                        var lkpAuditType = await _leadRepository.GetLkpLeadAuditTypeFromName("Not Qualified", logCommand);
                        lkpAuditTypeGuid = lkpAuditType.AuditTypeGuid;
                    }

                    //LKPAccountTypeGuid
                    //Todo This is hard coded!!! NotQualify, needs to be fixed. 
                    upsertEventState = "Attempting To Get LkpAccountType Guid" + Environment.NewLine;
                    if (!string.IsNullOrEmpty("Commercial"))
                    {
                        var lkpAccountType = await _customerRepository.GetCustomerAccountTypeFromName("Commercial", logCommand);
                        accountTypeGuid = lkpAccountType.AccountTypeGuid;
                    }
                    else
                    {
                        logCommand.LogMessage = string.Format($"Error: {Service}.{method} -- Missing AccountTypeGuid ");
                        _logHandler.HandleLog(logCommand);


                        return ServiceResponse;
                    }

                    upsertEventState = "Attempting To Instantiate Customer" + Environment.NewLine;
                    var customer = new Customer(leadPayload, programGuid, subprogramGuid, accountTypeGuid);
                    upsertEventState = "Attempting To Instantiate Lead" + Environment.NewLine;
                    var lead = new Lead(leadPayload, lkpLeadStatusGuid, lkpAuditTypeGuid) { LeadStatusChangeDate = DateTime.Now };

                    upsertEventState = "Attempting To Execute on Customer" + Environment.NewLine;
                    recordsAffected += await _customerRepository.InsertCustomer(customer, logCommand);
                    ServiceResponse = await InsertAuditRecordAsync(logCommand, cdcResponseObject, customer.GetType().FullName, leadPayload._PayloadBody.LastModifiedById);
                    if (!ServiceResponse.Success) return ServiceResponse;

                    upsertEventState = "Attempting To Execute on Lead" + Environment.NewLine;
                    recordsAffected += await _leadRepository.InsertLead(lead, logCommand);
                    ServiceResponse = await InsertAuditRecordAsync(logCommand, cdcResponseObject, lead.GetType().FullName, leadPayload._PayloadBody.LastModifiedById);
                    if (!ServiceResponse.Success) return ServiceResponse;

                    upsertEventState = "Attempting To Instantiate Contact" + Environment.NewLine;
                    var primaryContact = new Contact(leadPayload, Guid.Parse("BEF01936-5D80-471D-AF77-4CC2AE5161B4"));
                    recordsAffected += await _contactRepository.InsertContact(primaryContact, logCommand);
                    ServiceResponse = await InsertAuditRecordAsync(logCommand, cdcResponseObject, primaryContact.GetType().FullName, leadPayload._PayloadBody.LastModifiedById);
                    if (!ServiceResponse.Success) return ServiceResponse;

                    upsertEventState = "Attempting To Get Primary Contact" + Environment.NewLine;
                    var primaryContactData = await _contactRepository.GetPrimaryContactFromCustomerGuid(primaryContact.CustomerGuid, logCommand);
                    upsertEventState = "Attempting To Instantiate Phone" + Environment.NewLine;
                    var phone = new PhoneNumbers(leadPayload, primaryContactData.ContactGuid);
                    upsertEventState = "Attempting To Instantiate Email" + Environment.NewLine;
                    var email = new Email(leadPayload, primaryContactData.ContactGuid);

                    upsertEventState = "Attempting To Execute on Phone" + Environment.NewLine;
                    recordsAffected += await _contactRepository.InsertPhone(phone, logCommand);
                    ServiceResponse = await InsertAuditRecordAsync(logCommand, cdcResponseObject, phone.GetType().FullName, leadPayload._PayloadBody.LastModifiedById);
                    if (!ServiceResponse.Success) return ServiceResponse;

                    upsertEventState = "Attempting To Execute on Email" + Environment.NewLine;
                    recordsAffected += await _contactRepository.InsertEmail(email, logCommand);
                    ServiceResponse = await InsertAuditRecordAsync(logCommand, cdcResponseObject, email.GetType().FullName, leadPayload._PayloadBody.LastModifiedById);
                    if (!ServiceResponse.Success) return ServiceResponse;
                    upsertEventState = "Executions Complete" + Environment.NewLine;

                    entityResponse = $"Lead Does Not Exist, Insert Skeleton " + Environment.NewLine +
                                     $"Records Inserted: {recordsAffected} " + Environment.NewLine +
                                     $"Records Expected: 5 ";
                }

                if (recordsAffected == 0) return ServiceHelper.SetGenericServiceResponseForEntity($"Insert for ObjectName: {cdcResponseObject.ObjectNameC} and RecordId: {cdcResponseObject.RecordIdC} failed.");
                ServiceResponse.Entity = entityResponse;
                ServiceResponse.Success = true;
                ServiceResponse.RestResponseStatus = GenericServiceResponse.RestStatus.Success;


                logCommand.LogMessage = string.Format($"{Service}.{method} completed");
                _logHandler.HandleLog(logCommand);

                return ServiceResponse;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Point of Failure", upsertEventState);
                AppLogger.LogException(_loggingInstance, ex.Message, ex);
                return ServiceHelper.SetErrorGenericServiceResponse(ex);
            }

        }

        #region private methods

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




        private static string GetDeploymentEnvFromDbConnection()
        {
            return ConfigurationManager.ConnectionStrings["SalesforceServicesCon"].ToString().Split(new char[] { ';' })[1].Replace("Initial Catalog=CMC-SFDC_", "").ToUpper();
        }

        #endregion

    }

}
