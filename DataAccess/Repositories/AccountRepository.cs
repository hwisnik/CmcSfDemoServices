using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Handlers;

namespace DataAccess.Repositories
{
    public class AccountRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        // Repository table(s) for dapper

        public const string AccountTable = "[Customer].[Account]";
        public const string AccountStatusTable = "[Customer].[LkpAccountStatus]";

        //Logging String Helpers
        private const string Repository = "AccountRepository";

        public AccountRepository(IConnectionFactory connectionFactory, LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler), "logging commandhandlerDecorator instance is null");
        }


        public async Task<Account> GetAccountSingle (Guid AccountGuid, string SFAccountID, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetAccountFromGuid";

            //Query Used by Dapper
            var query =
                $" select * from {AccountTable} " +
                " where AccountGuid = @AccountGuid  || ( SFAccountID = @SFAccountID and SFAccountID is not null )";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameters: AccountGuid={AccountGuid}, SFAccountID={SFAccountID}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var account = await connection.QueryFirstAsync<Account>(query, new { AccountGuid });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return account;
            }
        }

        public async Task<IEnumerable<LkpAccountStatus>> GetAccountStatuses(LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetAccountStatuses";

            //Query Used by Dapper
            var query =
                $" select * from {AccountStatusTable} ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter " + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var accountStatuses = await connection.QueryAsync<LkpAccountStatus>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return accountStatuses;
            }
        }

        public async Task<int> UpdateAccount(Account account, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "UpdateAccount";

            //Query Used by Dapper
            var query =
                $" update  from {AccountTable} " +
                " set [AccountStatusGuid] = @AccountStatusGuid " +
                ", JobId = @JobId " +
                " where SFAccountID = @SFAccountID  ";

            if (account.AccountStatusGuid == Guid.Empty)
            {
                throw new Exception("Missing Fields"); 
            }

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter account={JsonConvert.SerializeObject(account)}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var Records = await connection.ExecuteAsync(query, account);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return Records;
            }
        }

        public async Task<int> InsertAccount(Account account, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "InsertAccount";

            //Query Used by Dapper
            var query =
                $" insert into {AccountTable} " +
                "  [AccountGuid] " +
                ", [CustomerGuid] " +
                ", [SFAccountID] " +
                ", [AccountStatusGuid] " +
                ", [JobId]  ";

            if (account.CustomerGuid == Guid.Empty || account.AccountStatusGuid == Guid.Empty) 
            {
                throw new Exception("Missing Fields"); 
            }

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter account={JsonConvert.SerializeObject(account)}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var Records = await connection.ExecuteAsync(query, account);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return Records;
            }
        }
    }
}
