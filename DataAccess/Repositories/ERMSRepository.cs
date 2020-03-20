using System;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using DataAccess.Repositories.Interfaces;
using Shared.Commands;
using Shared.Entities.DTO.ERMS;
using Shared.Handlers;

namespace DataAccess.Repositories
{
    public class ERMSRepository : IERMSRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        // Repository table(s) for dapper

        public const string ERMSCustomerTable = "[ERMS].[ERMSCustomer]";

        //Logging String Helpers
        private const string Repository = "ERMSRepository";

        public ERMSRepository(IConnectionFactory connectionFactory, LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler), "logging commandhandlerDecorator instance is null");
        }


        public async Task<ERMSCustomer> GetERMSCustomer(Guid CustomerGuid, LogCommand logCommand)
        {
            try
            {
                //Logging Helper for Method
                const string methodName = "GetAccountFromGuid";

                //Query Used by Dapper
                var query =
                    $" select * from {ERMSCustomerTable} " +
                    " where CustomerGuid = @CustomerGuid ";

                //Log the input
                logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameters: CustomerGuid={CustomerGuid}" + Environment.NewLine +
                                        $"Query: {query}";
                _logHandler.HandleLog(logCommand);

                using (var connection = _connectionFactory.GetConnection)
                {
                    //Await the response
                    var ermsCustomer = await connection.QueryFirstOrDefaultAsync<ERMSCustomer>(query, new { CustomerGuid });

                    connection.Close();

                    //Log the output
                    logCommand.LogMessage = $"{Repository}.{methodName} completed";
                    _logHandler.HandleLog(logCommand);

                    return ermsCustomer;
                }
            }
            catch
            {
                return new ERMSCustomer();
            }
        }
    }
}
