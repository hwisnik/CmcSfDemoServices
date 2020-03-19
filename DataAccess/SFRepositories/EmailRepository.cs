using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using DataAccess.SFInterfaces;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Entities.DTO.Contact;
using Shared.Entities._SearchCriteria.Client;
using Shared.Handlers;

namespace DataAccess.SFRepositories
{
    public class EmailRepository : IEmailRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        // Repository table(s) for dapper
        private const string RepositoryTable = " [Client].[Email] ";

        //Logging String Helpers
        private const string Repository = "EmailRepository";

        public EmailRepository(IConnectionFactory connectionFactory,
            LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler),
                              "logging commandhandlerDecorator instance is null");
        }

        public async Task<Email> GetEmailFromGuid(Guid EmailGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetEmailFromGuid";

            //Query Used by Dapper
            var query =
                " SELECT  * from " + RepositoryTable +
                " where EmailId = @EmailGuid";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter EmailGuid={EmailGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _Email = await connection.QueryFirstAsync<Email>(query,
                    new { EmailGuid });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _Email;
            }
        }

        public async Task<IEnumerable<Email>> GetEmail(SearchEmail _SearchEmail, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetEmail";

            //Query Used by Dapper
            var whereClause = _SearchEmail.getWhereClause();

            var query =
                " SELECT  * from " + RepositoryTable + " "
                + whereClause;

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter _SearchEmail={JsonConvert.SerializeObject(_SearchEmail)}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _Email = await connection.QueryAsync<Email>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _Email;
            }
        }

        

        public async Task<int> UpsertEmail(string Email, string FieldName, Guid ContactGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "UpsertEmail";

            var query = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; " +
                "BEGIN TRANSACTION; " +
                $" UPDATE [Customer].Email  SET " +
                        $" [EmailAddress] = '{Email}' " +
                $"  where [ContactGuid] = '{ContactGuid}' and [SFFieldName] = '{FieldName}'; " +
                "IF @@ROWCOUNT = 0 " +
                "BEGIN " +
                " Insert into [CMC-SFDC_DEV].[Contact].[Email] " +
                $" values (newID(), '{ContactGuid}', '{FieldName}', '{Email}' )" +
                "END " +
                "COMMIT TRANSACTION; ";
            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameter " +
                                    $"Email={Email}" + Environment.NewLine +
                                    $"Email={Email}" + Environment.NewLine +
                                    $"Email={Email}" + Environment.NewLine +
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
        ////Only useful for updates. Inserts/upserts are more complicated. 
        //private string RecordOrStringGeneration(string[] RecordIDs)
        //{
        //    var record_where_clause = string.Empty;
        //    var recordIDCountEmail = 0;
        //    foreach (var SFrecordID in RecordIDs)
        //    {
        //        if (recordIDCountEmail > 0)
        //        {
        //            record_where_clause += " or ";
        //        }

        //        record_where_clause += $" _account.SFAccountGuid = '{SFrecordID}' ";
        //        recordIDCountEmail++;
        //    }

        //    return " " + record_where_clause + " ";
        //}
        private List<string> UpdateEmailByContactIDList( bool? IsEmailContactAllowed, string[] RecordIDs)
        {
            bool? OppositeOfExpected = null; 
            if (IsEmailContactAllowed != null)
            {
                OppositeOfExpected = !IsEmailContactAllowed;
            }  
            
            List<string> EmailList = new List<string>();
            foreach (var RecordID in RecordIDs)
            {
                EmailList.Add ( "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; " +
                                "BEGIN TRANSACTION; " +
                                $"UPDATE [Client].Email  SET IsEmailContactAllowed='{OppositeOfExpected}'  " +
                                "from [Client].Email as _email  " +
                                "  join [Client].[Contact] as _contact  " +
                                "  on _email.ContactID = _contact.ContactId " +
                                "  join [Customer].[Account] as _account  " +
                                "  on _contact.AccountGuid = _account.CustomerAccountId " +
                                $"  where _account.SFAccountGuid = '{RecordID}'  and _contact.IsPrimaryContact = 1; " +
  
                                "IF @@ROWCOUNT = 0 " +
                                "BEGIN " +
                                " declare @ContactID UniqueIdentifier; " +
                                "  select @ContactID = _contact.ContactId from [Client].[Contact] as _contact  " +
                                "  join [Customer].[Account] as _account  " +
                                "  on _contact.AccountGuid = _account.CustomerAccountId " +
                                $"  where _account.SFAccountGuid = '{RecordID}' and _contact.IsPrimaryContact = 1 " +
                                $"  INSERT [Client].Email values (NewID(), @ContactID, '', {OppositeOfExpected}, 'Email')    " +
                                "  SELECT * from [Client].[Email]  where ContactID = @ContactID; " +
                                "END " +
                                "COMMIT TRANSACTION; ");
            }
            return EmailList;
        }

        private List<string> EmailAddressQueryGenerator(string FieldName, string EmailAddress, string[] RecordIDs)
        {
            List<string> EmailList = new List<string>();
            foreach (var RecordID in RecordIDs)
            {
                EmailList.Add ( "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; " +
                                "BEGIN TRANSACTION; " +
                                $"UPDATE [Client].Email  SET EmailAddress='{EmailAddress}'  " +
                                "from [Client].Email as _email  " +
                                "  join [Client].[Contact] as _contact  " +
                                "  on _email.ContactID = _contact.ContactId " +
                                "  join [Customer].[Account] as _account  " +
                                "  on _contact.AccountGuid = _account.CustomerAccountId " +
                                $"  where _account.SFAccountGuid = '{RecordID}'  and _contact.IsPrimaryContact = 1 and _email.SFFieldName = '{FieldName}'; " +
  
                                "IF @@ROWCOUNT = 0 " +
                                "BEGIN " +
                                " declare @ContactID UniqueIdentifier; " +
                                "  select @ContactID = _contact.ContactId from [Client].[Contact] as _contact  " +
                                "  join [Customer].[Account] as _account  " +
                                "  on _contact.AccountGuid = _account.CustomerAccountId " +
                                $"  where _account.SFAccountGuid = '{RecordID}' and _contact.IsPrimaryContact = 1 " +
                                $"  INSERT [Client].Email values (NewID(), @ContactID, '{EmailAddress}', 1, '{FieldName}')    " +
                                "  SELECT * from [Client].[Email]  where ContactID = @ContactID; " +
                                "END " +
                                "COMMIT TRANSACTION; ");
            }
            return EmailList;
        }
    }


}
