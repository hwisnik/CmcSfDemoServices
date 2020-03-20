using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using DataAccess.Repositories.Interfaces;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Entities.DTO.Contact;
using Shared.Entities._SearchCriteria.Client;
using Shared.Handlers;

namespace DataAccess.Repositories
{
    public class PhoneRepository : IPhoneRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        // Repository table(s) for dapper
        private const string RepositoryTable = " [Contact].[Phone] ";

        //Logging String Helpers
        private const string Repository = "PhoneRepository";

        public PhoneRepository(IConnectionFactory connectionFactory,
            LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler),
                              "logging commandhandlerDecorator instance is null");
        }

        public async Task<PhoneNumbers> GetPhoneFromGuid(Guid PhoneGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetPhoneFromGuid";

            //Query Used by Dapper
            var query =
                " SELECT  * from " + RepositoryTable +
                " where PhoneId = @PhoneGuid"; 

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter PhoneGuid={PhoneGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _Phone = await connection.QueryFirstAsync<PhoneNumbers>(query, 
                    new{PhoneGuid});

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _Phone;
            }
        }

        public async Task<IEnumerable<PhoneNumbers>> GetPhone(SearchPhone _SearchPhone, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetPhone";

            //Query Used by Dapper
            var whereClause = _SearchPhone.getWhereClause();

            var query =
                " SELECT  * from " + RepositoryTable + " "
                + whereClause;

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter _SearchPhone={JsonConvert.SerializeObject(_SearchPhone)}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _Phone = await connection.QueryAsync<PhoneNumbers>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _Phone;
            }
        }

        

        public async Task<int> UpsertPhone(string PhoneNumber, string FieldName, Guid ContactGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "UpsertEmail";

            var query = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; " +
                        "BEGIN TRANSACTION; " +
                        $" UPDATE [Customer].Phone  SET " +
                        $" [PhoneNumber] = '{PhoneNumber}' , " +
                        $" [PhoneTypeGuid] = '17813AB5-5C5F-41DA-A230-633A0CB2FC60' " +
                        $"  where [ContactGuid] = '{ContactGuid}' and [SFFieldName] = '{FieldName}'; " +
                        "IF @@ROWCOUNT = 0 " +
                        "BEGIN " +
                        " Insert into [CMC-SFDC_DEV].[Contact].[Phone] " +
                        $" values (newID(), '{ContactGuid}', '{FieldName}', '{PhoneNumber}', '17813AB5-5C5F-41DA-A230-633A0CB2FC60' )" +
                        "END " +
                        "COMMIT TRANSACTION; ";
            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameter " +
                                    $"ContactGuid={ContactGuid}" + Environment.NewLine +
                                    $"FieldName={FieldName}" + Environment.NewLine +
                                    $"PhoneNumber={PhoneNumber}" + Environment.NewLine +
                                    $"______________" + Environment.NewLine +
                                    $"Query: {query}";

            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _Email = await connection.ExecuteAsync(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return _Email;
            }
        }
        
        private List<string> UpdateAllPhonesForAccount ( 
            bool? IsVoiceContactAllowed, 
            bool IVCAChanged,
            bool? IsSmsContactAllowed,
            bool ISCAChnaged,
            bool? IsAutoCallAllowed, 
            bool IACAChanged,
            string[] RecordIDs)
        {

            var count = 0; 
            var set_statement = " set ";
            if (IVCAChanged)
            {
                if (count > 0)
                {
                    set_statement += " , ";
                }

                count++;
                set_statement += $" IsVoiceContactAllowed = '{IsVoiceContactAllowed}' ";
            }
            if (ISCAChnaged)
            {
                if (count > 0)
                {
                    set_statement += " , ";
                }
                count++;
                set_statement += $" IsSmsContactAllowed = '{IsSmsContactAllowed}' ";
            }
            if (IACAChanged)
            {
                if (count > 0)
                {
                    set_statement += " , ";
                }
                count++;
                set_statement += $" IsAutoCallAllowed = '{IsAutoCallAllowed}' ";
            }

            List<string> PhoneList = new List<string>();
            if (count == 0)
            {
                return PhoneList; 
            }
            foreach (var RecordID in RecordIDs)
            {
                PhoneList.Add ( "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; " +
                                "BEGIN TRANSACTION; " +
                                $" UPDATE [Client].Phone  " +
                                set_statement +
                                " from [Client].Phone as _phone  " +
                                "  join [Client].[Contact] as _contact  " +
                                "  on _phone.ContactID = _contact.ContactId " +
                                "  join [Customer].[Account] as _account  " +
                                "  on _contact.AccountGuid = _account.CustomerAccountId " +
                                $"  where _account.SFAccountGuid = '{RecordID}'  and _contact.IsPrimaryContact = 1; " +
  
                                " IF @@ROWCOUNT = 0 " +
                                " BEGIN " +
                                " declare @ContactID UniqueIdentifier; " +
                                "  select @ContactID = _contact.ContactId from [Client].[Contact] as _contact  " +
                                "  join [Customer].[Account] as _account  " +
                                "  on _contact.AccountGuid = _account.CustomerAccountId " +
                                $"  where _account.SFAccountGuid = '{RecordID}' and _contact.IsPrimaryContact = 1 " +
                                $"  INSERT [Client].Phone values (NewID(), @ContactID, '','{IsVoiceContactAllowed}', '{IsSmsContactAllowed}', Null, '{IsAutoCallAllowed}','8B8C2EBE-24C3-48ED-AA69-640FFD973C95', 'Phone') " +
                                "  SELECT * from [Client].[Phone]  where ContactID = @ContactID; " +
                                " END " +
                                " COMMIT TRANSACTION; ");
            }
            return PhoneList;
        }

        private List<string> PhoneNumberQueryList(string FieldName, string PhoneNumber, string[] RecordIDs)
        {
            var GuidForInsert = "8B8C2EBE-24C3-48ED-AA69-640FFD973C95";
            if (FieldName.Contains("Mobile"))
            {
                GuidForInsert = "CC3BB51F-BEC7-42AB-A8A7-17B5A8767CC1";
            }
            List<string> PhoneList = new List<string>();
            foreach (var RecordID in RecordIDs)
            {
                PhoneList.Add ( "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; " +
                                "BEGIN TRANSACTION; " +
                                $"UPDATE [Client].Phone  SET PhoneNumber='{PhoneNumber}'  " +
                                "from [Client].Phone as _phone  " +
                                "  join [Client].[Contact] as _contact  " +
                                "  on _phone.ContactID = _contact.ContactId " +
                                "  join [Customer].[Account] as _account  " +
                                "  on _contact.AccountGuid = _account.CustomerAccountId " +
                                $"  where _account.SFAccountGuid = '{RecordID}'  and _contact.IsPrimaryContact = 1 and _phone.SFFieldName = '{FieldName}'; " +
  
                                "IF @@ROWCOUNT = 0 " +
                                "BEGIN " +
                                " declare @ContactID UniqueIdentifier; " +
                                "  select @ContactID = _contact.ContactId from [Client].[Contact] as _contact  " +
                                "  join [Customer].[Account] as _account  " +
                                "  on _contact.AccountGuid = _account.CustomerAccountId " +
                                $"  where _account.SFAccountGuid = '{RecordID}' and _contact.IsPrimaryContact = 1 " +
                                $"  INSERT [Client].Phone values (NewID(), @ContactID, '{PhoneNumber}', 1, 1, 1, 1,'{GuidForInsert}', '{FieldName}') " +
                                "  SELECT * from [Client].[Phone]  where ContactID = @ContactID; " +
                                "END " +
                                "COMMIT TRANSACTION; ");
            }
            return PhoneList;
        }
    }
}