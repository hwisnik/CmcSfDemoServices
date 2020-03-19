using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using DataAccess.SFInterfaces;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Entities._SearchCriteria.Client;
using Shared.Handlers;
using Shared.Entities.DTO.Customer;
using Shared.EventPayloads;

namespace DataAccess.SFRepositories
{
    public class UsageRepository : IUsageRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        // Repository table(s) for dapper
        private const string RepositoryTableUsage = " [Customer].[Usage] ";

        //Logging String Helpers
        private const string Repository = "UsageRepository";

        public UsageRepository(
            IConnectionFactory connectionFactory,
            LoggingCommandHandlerDecorator<LogCommand> loghandler
            )
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler),
                              "logging commandhandlerDecorator instance is null");
        }


        public async Task<IEnumerable<Usage>> GetUsage(SearchUsage searchUsage,LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetUsage";

            //Query Used by Dapper
            var whereClause = searchUsage.getWhereClause();

            var query =
                " SELECT  * from " + RepositoryTableUsage + " "
                + whereClause;

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter _SearchUsage={JsonConvert.SerializeObject(searchUsage)}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var usage = await connection.QueryAsync<Usage>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return usage;
            }
        }


        public async Task<IEnumerable<Usage>> GetUsageFromCustomerGuid(Guid customerGuid,LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetUsageFromCustomerGuid";

            var query = QueryResources.UsageQueries.ResourceManager.GetString("GetUsageByCustomerGuid");

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting no input parameter customerGuid={customerGuid}" + Environment.NewLine + $"Query: {query}";
            _logHandler.HandleLog(logCommand);

            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var usageList = await connection.QueryAsync<Usage>(query, new { @CustomerGuid = customerGuid });
                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return usageList;
            }
        }


        public async Task<CapQualified> GetIsCapQualifiedFromCustomerGuid(Guid customerGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetIsCapQualifiedFromCustomerGuid";

            //Query Used by Dapper
            var query =
                " SELECT CC.CustomerGuid, " +
                " MAX(CASE " +
                " WHEN LRC.CAPTier != '' and LRC.CAPTier IS NOT NULL THEN 1 " +
                " ELSE 0 " +
                " END) AS IsCapQualified " +
                " FROM [Customer].[Customer] CC " +
                "    INNER JOIN [Customer].[Usage] CU ON CU.CustomerGuid = CC.CustomerGuid " +
                " INNER JOIN [UsageRaw].[LkpRateCodes] LRC ON LRC.RateCode = CU.Rate AND LRC.ProgramGuid = CC.ProgramGuid " +
                " WHERE CC.CustomerGuid = @CustomerGuid " +
                " GROUP BY CC.CustomerGuid";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter CustomerGuid={customerGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                CapQualified usage;
                try
                {
                     usage = await connection.QueryFirstOrDefaultAsync<CapQualified>(query, new {CustomerGuid = customerGuid});
                }
                catch
                {
                    return new CapQualified()
                    {
                        CustomerGuid = customerGuid,
                        IsCapQualified = false
                    };
                }

                    
                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return usage;
                
            }
        }


        public async Task<int> UpdateUsageFromLeadCDC(LeadPayload cdcResponseObject, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "UpdateUsageFromLeadCDC";

            var BaseQuery = "update [Customer].[Usage]  ";

            var set_statement = " set ";
            var JoinQuery = "  FROM [Client].[Usage] as _usage " +
                            "  join Customer.Account as _account " +
                            "  on _usage.AccountGuid = _account.CustomerAccountId   ";

            //var CountSets = 0;

            //Build the Query
            var query = BaseQuery + " " +
                        set_statement + " " +
                        JoinQuery + " ";
            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters CDCResponseObject = {JsonConvert.SerializeObject(cdcResponseObject)}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var returnValue = await connection.ExecuteAsync(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return returnValue;
            }
        }
    }
}
