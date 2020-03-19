using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using DataAccess.SFInterfaces;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Entities.DTO.Helper;
using Shared.Entities.InputObjects.Lead;
using Shared.Entities.SFDb.Lead;
using Shared.Entities._SearchCriteria.Client;
using Shared.EventPayloads;
using Shared.Handlers;

namespace DataAccess.SFRepositories
{
    public class LeadRepository : ILeadRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        // Repository table(s) for dapper
        private const string LeadTable = " [Customer].[LEAD] ";
        private const string LeadStatusTable = " [Customer].[LkpLeadStatus] ";
        private const string LeadAuditTypeTable = " [Customer].[LkpAuditType] ";
        //Logging String Helpers
        private const string Repository = "LeadRepository";

        public LeadRepository(IConnectionFactory connectionFactory, LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler), @"logging commandhandlerDecorator instance is null");
        }


        public async Task<Lead> GetLead(Guid leadGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLeadAsync";

            //Query Used by Dapper
            var query =
                " SELECT  TOP (100) * from " + LeadTable +
                " where LeadId = '" + leadGuid + "'";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter leadGuid={leadGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var lead = await connection.QueryFirstAsync<Lead>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return lead;
            }
        }

        public async Task<IEnumerable<Lead>> GetLeadByCustomerGuidAsync(Guid customerGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLeadByCustomerGuidAsync";

            //Query Used by Dapper
            var query = QueryResources.LeadQueries.ResourceManager.GetString("GetLeadByCustomerGuid");

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter customerGuid={customerGuid}" + Environment.NewLine + $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var lead = await connection.QueryAsync<Lead>(query, new { customerGuid });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return lead;
            }
        }


        public async Task<IEnumerable<Lead>> GetLeads(Guid customerGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLeads";

            //Query Used by Dapper
            var query =
                " SELECT  TOP (100) * from " + LeadTable +
                " where CustomerGuid = @customerGuid ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter CustomerGuid={customerGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var lead = await connection.QueryAsync<Lead>(query, new { customerGuid });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return lead;
            }
        }
        public async Task<Lead> GetDetailedLead(Guid customerGuid, LogCommand logCommand)
        {
            //Logging Helper String
            const string methodName = "GetDetailedLead";

            //ToDo:: Fix this to match our strategy. 
            //Query used by Dapper

            //Query Used by Dapper
            var query =
                "select " +
                "_lead.[LeadGuid] " +
                ",_lead.[CustomerGuid] " +
                ",_lead.[LeadStatusGuid] " +
                ",_lead.[SFLeadID] " +
                ",_lead.[LeadSource]  " +
                ",_lead.[QualifiedAuditTypeGuid]  " +
                ",_LeadStatus.[LeadStatusName] as LeadStatusName" +
                ",_lead.[LeadStatusReason] as LeadStatusReason" +
                ",_lead.[QualifiedRate] " +
                " from [Customer].[Lead] as _lead     " +
                $" join [Customer].[Customer] as _Customer       " +
                " on _Customer.CustomerGuid = _lead.[CustomerGuid]     " +
                $" left join {LeadStatusTable} as _LeadStatus       " +
                " on _lead.LeadStatusGuid = _LeadStatus.[LeadStatusGuid]     " +

                " where _Customer.CustomerGuid = @CustomerGuid ";


            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter CustomerGuid={customerGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);

            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var leadFilledGuids = await connection.QueryFirstAsync<Lead>(query, new { CustomerGuid = customerGuid });
                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return leadFilledGuids;
            }
        }

        public async Task<IEnumerable<SFSimpleLeadResults>> GetSfLeads(SearchLeadSimple leadSearcher, LogCommand logCommand)
        {
            //Logging Helper String
            const string methodName = "GetSFLeads";

            if (leadSearcher.NumberOfRecords == null || leadSearcher.NumberOfRecords == 0 || leadSearcher.NumberOfRecords > 1000)
            {
                leadSearcher.NumberOfRecords = 1000;
            }


            //ToDo:: Fix this to match our strategy. 
            //Query used by Dapper

            //Query Used by Dapper
            var query = new QueryBuilder($" select distinct TOP ({leadSearcher.NumberOfRecords})     " +
                                            " _lead.[LeadGuid]      " +
                                            " ,_Customer.[CustomerGuid]     " +
                                            " ,_Customer.[BillingAccountNumber] as BillAccount     " +
                                            " ,_Customer.[CMCCustomerID]    " +
                                            " ,_email.Email_Address as Email    " +
                                            " ,_phone.Phone as PhoneNumber    " +
                                            " ,_contact.FullName as CustomerName         " +
                                            " ,_address.Zip as ZipCode    " +
                                            " ,_county.CountyDescription as County    " +
                                            " ,_address.StreetAddress1 as STAddress  " +
                                            " ,CUU.CAPTier   " +
                                            " from [Customer].[Lead] as _lead     " +

                                            " join Customer.Customer as _Customer       " +
                                            " on _Customer.CustomerGuid = _lead.[CustomerGuid]     " +

                                            " join [Contact].Contact as _contact       " +
                                            " on _contact.[CustomerGuid] = _Customer.CustomerGuid     " +

                                            " left join [Contact].Email as _email       " +
                                            " on _email.ContactGuid = _contact.ContactGuid    " +

                                            " left join [Contact].Phone as _phone     " +
                                            " on _phone.ContactGuid = _contact.ContactGuid        " +

                                            " join Program.Program _program       " +
                                            " on _program.ProgramGuid = _Customer.ProgramGuid      " +

                                            "	CROSS APPLY(SELECT TOP 1 lrc.CapTier FROM [Customer].[Usage] CUU " +
                                            " INNER JOIN [UsageRaw].[LkpRateCodes] LRC ON LRC.RateCode = CUU.Rate  " +

                                            " AND LRC.ProgramGuid = _Customer.ProgramGuid " +
                                            " WHERE CUU.CustomerGuid = _Customer.CustomerGuid) CUU " +

                                            " join Program.Subprogram _subprogram     " +
                                            " on _subprogram.SubProgramGuid = _Customer.SubProgramGuid     " +

                                            " join [Customer].[Address] as _address  " +
                                            " on _address.CustomerGuid = _Customer.CustomerGuid " +

                                            " join [Customer].[LkpCounties] as _county  " +
                                            " on _address.CountyGuid = _county.CountyGuid ");

            query.WhereAndCondition("_lead.SFLeadID is null");
            query.WhereAndCondition($"_email.Email_Address like '%'+@EmailAddress+'%'", leadSearcher.EmailAddress);
            query.WhereAndCondition($"_contact.FullName like '%'+@CustomerName+'%'", leadSearcher.CustomerName);
            query.WhereAndCondition($"_Customer.CMCCustomerID = @CMCCustomerID", leadSearcher.CMCCustomerID);
            query.WhereAndCondition($"_Customer.BillingAccountNumber like '%'+@BillAccount+'%'", leadSearcher.BillAccount);
            query.WhereAndCondition($"_address.Zip like '%'+@ZipCode+'%' ", leadSearcher.ZipCode);
            query.WhereAndCondition($"_address.StreetAddress1 like '%'+@StAddress+'%' ", leadSearcher.StAddress);
            query.WhereAndCondition($"( _phone.[Phone] like '%' + @PhoneNumber + '%' or _phone.[Mobile] like '%' + @PhoneNumber + '%' or _phone.[Home_Phone__c] like '%'+ @PhoneNumber + '%' )", leadSearcher.PhoneNumber);
            query.WhereAndCondition($"(_Customer.ProgramId = @ProgramGuid)", leadSearcher.ProgramGuid);


            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting no input parameter LeadSearcher={JsonConvert.SerializeObject(leadSearcher)}" + Environment.NewLine +
                                    $"Query: {query.GetQuery()}";
            _logHandler.HandleLog(logCommand);

            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var leadList = await connection.QueryAsync<SFSimpleLeadResults>(query.GetQuery(), leadSearcher);
                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return leadList;
            }
        }



        public async Task<IEnumerable<SfPrioritizedLead>> GetPrioritizedLeadsByUsage(PrioLeadInput input, LogCommand logCommand)
        {
            //Logging Helper String
            const string methodName = "GetSFPrioritizedLeadsByUsage";

            var query = QueryResources.LeadQueries.ResourceManager.GetString("LiurpElectricAndGasAuditQuery");

            //Check if we only want to do GasOnly Heating Audits
            //If Worktype = Heating (BDA) or Apt Heating
            if (input.WorkType == Guid.Parse("4B6CEBE1-E220-4F46-A732-2CCCCDE5F61A") || input.WorkType == Guid.Parse("E08D72D1-A4D5-470E-9558-F1020A774CA9"))
            {
                if (ConfigurationManager.AppSettings["LiurpGasOnlyHeatingAudit"] != "-1")
                    //Limits leads to those only qualified for Gas heating audits
                    query = query + " AND LRC.BaseRate in ('G', 'H') ";
            }


            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter: { input.WorkType} " + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var leadList = await connection.QueryAsync<SfPrioritizedLead>(query, new { @QualifiedAuditTypeGuid = input.WorkType });
                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return leadList;
            }
        }

        public async Task<IEnumerable<SfPrioritizedLead>> GetPrioritizedLeadsByPostalCode(PrioLeadInput input, LogCommand logCommand)
        {
            //Logging Helper String
            const string methodName = "GetSFPrioritizedLeadsByPostalCode";

            //Query Used by Dapper
            var query =
                $" SELECT TOP {input.NumberOfLeadsToQuery.ToString()} CL.CustomerGuid " +
                "    ,CC.FullName " +
                "    ,CoP.Phone PhoneNumber " +
                "    ,CPA.Latitude " +
                "    ,CPA.Longitude " +
                "    ,CPA.ContactGuid " +
                "    ,CONCAT (CPA.StreetAddress1, ' ', CPA.StreetAddress2, ' ', CPA.City + ', ' , CPA.State, ' ', CPA.Zip)  Address " +
                "    ,CL.SfLeadId " +
                "    ,CUU.CAPTier " +
                " FROM Customer.Lead CL " +
                " INNER JOIN [Contact].[Contact] CC ON CC.CustomerGuid = CL.CustomerGuid " +
                " INNER JOIN [Customer].[Address] CPA ON CPA.CustomerGuid = CL.CustomerGuid " +
                " INNER JOIN [CustomerProgram].[PecoCustomer] CPPC ON CPPC.CustomerGuid = CL.CustomerGuid " +
                " INNER JOIN customer.Customer cu on cu.CustomerGuid = cl.CustomerGuid " +
                " INNER JOIN program.Subprogram sp on sp.SubProgramGuid = cu.SubProgramGuid " +
                "  CROSS APPLY(SELECT TOP 1 CUU.Rate, CUU.AverageUsage, CUU.HeatingAverageUsage, lrc.CapTier FROM [Customer].[Usage] CUU " +
                "      INNER JOIN [UsageRaw].[LkpRateCodes] LRC ON LRC.RateCode = CUU.Rate " +
                "      AND LRC.ProgramGuid = CU.ProgramGuid " +
                "      WHERE CUU.CustomerGuid = CL.CustomerGuid) CUU " +
                " CROSS APPLY (SELECT TOP 1 CoP.Phone FROM [Contact].[Phone] CoP " +
                "    WHERE CoP.ContactGuid = CC.ContactGuid " +
                "    AND CoP.Phone != '9999999999') CoP " +
                " OUTER APPLY (Select max (aud.LastModifiedDate) lastModifiedDate FROM [CDCQueue].[AuditLog] aud " +
                "    WHERE aud.ObjectType = 'Shared.Entities.DTO.Customer.Lead' and aud.RecordID = cl.SFLeadID) aud " +
                " WHERE CL.LeadStatusGuid IN ('F06163CC-8196-482C-99DF-0D46185CA0F0','46AB1284-1228-4B12-87AB-776FB7C1583E') " +
                " AND CC.ContactTypeGuid = 'BEF01936-5D80-471D-AF77-4CC2AE5161B4' " +
                "/*AND sp.SubProgramGuid = '14C2E6A1-1650-47CA-AA37-3B732167FA01' */ " +
                " AND CC.IsAnyContactAllowed = 1 " +
                " AND CC.IsVoiceContactAllowed = 1 " +
                " AND CPA.Latitude IS NOT NULL " +
                " AND CPA.Longitude IS NOT NULL " +
                " AND CL.LeadStatusReason IS NULL " +
                " AND cl.ReservedDate != convert(date,GETDATE()) " +
                " AND (aud.lastModifiedDate IS NULL or DATEDIFF(day, aud.lastModifiedDate, GETDATE()) != 0) " +
                $" AND CL.QualifiedAuditTypeGuid = '{input.WorkType}' " +
                "/* AND CPPC.IncomeLevel = 1 */ " +
                $" AND CPA.Zip IN ({input.ZipCodesForQuery}) ";

            //LeadStatusName in ('Not Attempted', 'Attempted Contact')
            //ContactTypeName = Primary 
            //PhoneTypeName = Program PhoneNumber

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter: {input} " + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);

            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var leadList = await connection.QueryAsync<SfPrioritizedLead>(query);
                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return leadList;
            }
        }

        public async Task<IEnumerable<SfPrioritizedLead>> GetPrioritizedLeadsForLeepNoPostalCodes(PrioLeadInput input, LogCommand logCommand)
        {
            //Logging Helper String
            const string methodName = "GetPrioritizedLeadsForLeepNoPostalCodes";

            //Query Used by Dapper
            var query =
                $" SELECT CL.CustomerGuid " +
                "    ,CC.FullName " +
                "    ,CoP.Phone PhoneNumber " +
                "    ,CPA.Latitude " +
                "    ,CPA.Longitude " +
                "    ,CONCAT (CPA.StreetAddress1, ' ', CPA.StreetAddress2, ' ', CPA.City + ', ' , CPA.State, ' ', CPA.Zip)  Address " +
                "    ,CL.SfLeadId " +
                "    ,CUU.CAPTier " +
                " FROM Customer.Lead CL " +
                " INNER JOIN [Contact].[Contact] CC ON CC.CustomerGuid = CL.CustomerGuid " +
                " INNER JOIN [Customer].[Address] CPA ON CPA.CustomerGuid = CL.CustomerGuid " +
                " INNER JOIN [CustomerProgram].[PecoCustomer] CPPC ON CPPC.CustomerGuid = CL.CustomerGuid " +
                " INNER JOIN customer.Customer cu on cu.CustomerGuid = cl.CustomerGuid " +
                " INNER JOIN program.Subprogram sp on sp.SubProgramGuid = cu.SubProgramGuid " +
                "  CROSS APPLY(SELECT TOP 1 CUU.Rate, CUU.AverageUsage, CUU.HeatingAverageUsage, lrc.CapTier FROM [Customer].[Usage] CUU " +
                "      INNER JOIN [UsageRaw].[LkpRateCodes] LRC ON LRC.RateCode = CUU.Rate " +
                "      AND LRC.ProgramGuid = CU.ProgramGuid " +
                "      WHERE CUU.CustomerGuid = CL.CustomerGuid) CUU " +
                " CROSS APPLY (SELECT TOP 1 CoP.Phone FROM [Contact].[Phone] CoP " +
                "    WHERE CoP.ContactGuid = CC.ContactGuid " +
                "    AND CoP.Phone != '9999999999') CoP " +
                " OUTER APPLY (Select max (aud.LastModifiedDate) lastModifiedDate FROM [CDCQueue].[AuditLog] aud " +
                "    WHERE aud.ObjectType = 'Shared.Entities.DTO.Customer.Lead' and aud.RecordID = cl.SFLeadID) aud " +
                " WHERE CL.LeadStatusGuid IN ('F06163CC-8196-482C-99DF-0D46185CA0F0','46AB1284-1228-4B12-87AB-776FB7C1583E') " +
                " AND CC.ContactTypeGuid = 'BEF01936-5D80-471D-AF77-4CC2AE5161B4' " +
                "/*AND sp.SubProgramGuid = '14C2E6A1-1650-47CA-AA37-3B732167FA01' */ " +
                " AND CC.IsAnyContactAllowed = 1 " +
                " AND CC.IsVoiceContactAllowed = 1 " +
                " AND CPA.Latitude IS NOT NULL " +
                " AND CPA.Longitude IS NOT NULL " +
                " and cl.ReservedDate != convert(date,GETDATE()) " +
                " AND CL.LeadStatusReason IS NULL " +
                " AND (aud.lastModifiedDate IS NULL or DATEDIFF(day, aud.lastModifiedDate, GETDATE()) != 0) " +
                $" AND CL.QualifiedAuditTypeGuid = '{input.WorkType}' ";
            //"/* AND CPPC.IncomeLevel = 1 */ " +

            //LeadStatusName in ('Not Attempted', 'Attempted Contact')
            //ContactTypeName = Primary 
            //PhoneTypeName = Program PhoneNumber

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter: {input} " + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);

            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var leadList = await connection.QueryAsync<SfPrioritizedLead>(query);
                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return leadList;
            }
        }


        public async Task<LkpLeadStatus> GetLkpLeadStatusFromName(string leadStatusName, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLkpLeadStatusFromName";

            //Query Used by Dapper
            var query =
                " SELECT  * from " + LeadStatusTable +
                " where LeadStatusName = @LeadStatusName";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter LeadStatusName = {leadStatusName}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var lkpLeadStatus = await connection.QueryFirstAsync<LkpLeadStatus>(query, new { LeadStatusName = leadStatusName });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return lkpLeadStatus;
            }
        }

        public async Task<LkpAuditType> GetLkpLeadAuditTypeFromName(string auditTypeName, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLkpLeadAuditTypeFromName";


            if (string.IsNullOrEmpty(auditTypeName)
                || auditTypeName.ToLower() != "not qualified"
                || auditTypeName.ToLower() != "heating"
                || auditTypeName.ToLower() != "baseload"
                || auditTypeName.ToLower() != "apartment heating")
            {
                if (string.IsNullOrEmpty(auditTypeName))
                {
                    auditTypeName = "Not Qualified";
                }
                else if (auditTypeName.ToLower() == "wta")
                {
                    auditTypeName = "Baseload";
                }
                else if (auditTypeName.ToLower() == "bda")
                {
                    auditTypeName = "Heating";
                }
                else if (auditTypeName.ToLower() == "apt")
                {
                    auditTypeName = "Apartment Heating";
                }
                else if (auditTypeName.ToLower() == "not qualify")
                {
                    auditTypeName = "Not Qualified";
                }
            }

            //Query Used by Dapper
            var query =
                " SELECT  * from " + LeadAuditTypeTable +
                " where [AuditTypeName] = @AuditTypeName";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter AuditTypeName={auditTypeName}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var lkpAuditType = await connection.QueryFirstAsync<LkpAuditType>(query, new { AuditTypeName = auditTypeName });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return lkpAuditType;
            }
        }

        public Task<int> UpdateLeadFromLeadPayload(LeadPayload cDcResponseObject, LogCommand logCommand)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateLeadFromLeadPayload_GUID(LeadPayload leadPayload, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "UpdateLeadFromLeadPayload_GUID";

            var UpdateQuery = " update _LeadTable " +
                              " set  " +
                              "  _LeadTable.[SFLeadID] = @ID " +
                              " ,_LeadTable.[LeadStatusGuid] = _LeadStatus.LeadStatusGuid " +
                              " ,_LeadTable.LeadSource = @LeadSource " +
                              " ,_LeadTable.QualifiedAuditTypeGuid = _AuditType.AuditTypeGuid " +

                              " From [CMC-SFDC_DEV].[Customer].[Lead] as _LeadTable " +
                              " join customer.LkpAuditType as _AuditType " +
                              " on _AuditType.AuditTypeName = @AuditTypeC " +
                              " join Customer.LkpLeadStatus as _LeadStatus " +
                              " on _LeadStatus.LeadStatusName = @Status " +
                              " where _LeadTable.CustomerGuid = @CmcCustomerGuidC ";


            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters CDCResponseObject = {JsonConvert.SerializeObject(leadPayload)}" + Environment.NewLine +
                                    $"Query: {UpdateQuery}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var returnValue = await connection.ExecuteAsync(UpdateQuery, leadPayload);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return returnValue;
            }
        }

        public async Task<int> UpdateLeadAsync(Lead lead, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "UpdateLead";

            const string updateQuery = " update [Customer].[Lead] " +
                                       " SET " +
                                       "  [SFLeadID] = @SFLeadID " +
                                       ", [LeadStatusGuid] = @LeadStatusGuid " +
                                       ", [LeadStatusReason] = @LeadStatusReason " +
                                       ", [QualifiedAuditTypeGuid] = @QualifiedAuditTypeGuid " +
                                       ", [LeadStatusChangeDate] = @LeadStatusChangeDate " +
                                       ", [ReservedDate] = @ReservedDate " +
                                       ", [NextAvailableCallDate] = @NextAvailableCallDate " +
                                       // Skip Qualification Rate. 
                                       " where CustomerGuid = @CustomerGuid";


            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters _Lead = {JsonConvert.SerializeObject(lead)}" + Environment.NewLine +
                                    $"Query: {updateQuery}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var returnValue = await connection.ExecuteAsync(updateQuery, new
                {
                    lead.SFLeadID,
                    lead.LeadStatusGuid,
                    lead.LeadStatusReason,
                    lead.QualifiedAuditTypeGuid,
                    lead.LeadStatusChangeDate,
                    lead.ReservedDate,
                    lead.NextAvailableCallDate,
                    lead.CustomerGuid
                });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return returnValue;
            }
        }

        public async Task<int> InsertLead(Lead lead, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "InsertLead";

            var UpdateQuery = " insert into [Customer].[Lead] " +
                              "( [LeadGuid]" +
                              ", [LeadStatusGuid] " +
                              ", [SFLeadID] " +
                              ", [LeadStatusReason] " +
                              ", [LeadSource] " +
                              ", [QualifiedAuditTypeGuid] " +
                              ", [QualifiedRate] " +
                              ", [CustomerGuid]  " +
                              ", [ReservedDate]  " +
                              ", [LeadStatusChangeDate] " +
                              ", [NextAvailableCallDate] ) " +
                              "VALUES " +
                              "( NEWID() " +
                              ", @LeadStatusGuid " +
                              ", @SFLeadID " +
                              ", @LeadStatusReason" +
                              ", @LeadSource " +
                              ", @QualifiedAuditTypeGuid " +
                              ", @QualifiedRate " +
                              ", @CustomerGuid " +
                              ", @ReservedDate " +
                              ", @LeadStatusChangeDate " +
                              ", @NextAvailableCallDate ) ";


            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters _Lead = {JsonConvert.SerializeObject(lead)}" + Environment.NewLine +
                                    $"Query: {UpdateQuery}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var returnValue = await connection.ExecuteAsync(UpdateQuery, lead);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return returnValue;
            }
        }

        public async Task<int> SetLeadStatusToConverted(Guid customerGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "UpdateLeadFromLeadPayload_SFID";

            var updateQuery = $" update {LeadTable} " +
                               " set  LeadStatusGuid = '9993CA48-467D-42FA-AF02-9F9A34574860' " +
                               " where CustomerGuid = @customerGuid ";


            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameters CustomerGuid = {customerGuid}" + Environment.NewLine +
                                    $"Query: {updateQuery}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var returnValue = await connection.ExecuteAsync(updateQuery, new { customerGuid });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return returnValue;
            }
        }

        public async Task<int> LeadCreatedSetRecordId(Guid customerGuid, string sFLeadId, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "UpdateLeadFromLeadPayload_SFID";

            var updateQuery = $"update {LeadTable} " +
                              " set  SFLeadID = @SFLeadID " +
                              " where CustomerGuid = @CustomerGuid ";


            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameters CustomerGuid = {customerGuid} || SFLeadID = {sFLeadId}" + Environment.NewLine +
                                    $"Query: {updateQuery}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var returnValue = await connection.ExecuteAsync(updateQuery, new { CustomerGuid = customerGuid, SFLeadID = sFLeadId });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return returnValue;
            }
        }

        public async Task<AGuidForYou> GetMeAGuidPlease(LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetMeAGuidPlease";

            //Query Used by Dapper
            var query = $" SELECT Top(1) NEWID() as ANewGuidFromCMCSF from {LeadTable} ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting - no input parameter" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var lead = await connection.QueryFirstAsync<AGuidForYou>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return lead;
            }
        }

        /// <summary>
        /// Updates ReserveDateProperty of a Lead to 15 minutes from now
        /// </summary>
        /// <param name="customerGuid"></param>
        /// <param name="reservedDateTime"></param>
        /// <param name="logCommand"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public async Task<int> UpdateReservedDateAsync(Guid customerGuid, DateTime reservedDateTime, LogCommand logCommand, IDbTransaction trans)
        {
            //Logging Helper for Method
            const string methodName = "UpdateReservedDateAsync";
            var query = $"Update Customer.Lead set ReservedDate = '{reservedDateTime}' where CustomerGuid = @CustomerGuid";
            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter: {customerGuid} " + Environment.NewLine + $"Query: {query}";
            _logHandler.HandleLog(logCommand);

            //NOTE: NO USING STATEMENT CALLING ROUTINE IN THE SERVICE LAYER NEEDS TO MANAGE THE CONNECTION !!!
            //Await the response
            var results = await trans.Connection.ExecuteAsync(query, new { @CustomerGuid = customerGuid }, trans, null, CommandType.Text);

            // connection.Close();

            //Log the output
            logCommand.LogMessage = $"{Repository}.{methodName} completed";
            _logHandler.HandleLog(logCommand);

            return results;

        }

    }
}
