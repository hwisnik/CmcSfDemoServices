using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using DataAccess.SFInterfaces;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Entities._SearchCriteria.Client;
using Shared.Handlers;

namespace DataAccess.SFRepositories
{
    public class LkpLeadStatusRepository : ILkpLeadStatusRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        // Repository table(s) for dapper
        private const string RepositoryTable = " [Client].[LkpLeadStatus] ";

        //Logging String Helpers
        private const string Repository = "LkpLeadStatusRepository";

        public LkpLeadStatusRepository(IConnectionFactory connectionFactory,
            LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler),
                              "logging commandhandlerDecorator instance is null");
        }

        public async Task<LkpLeadStatus> GetLkpLeadStatusFromGuid(Guid LkpLeadStatusGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLkpLeadStatusFromGuid";

            //Query Used by Dapper
            var query =
                " SELECT  * from "     +  RepositoryTable    + 
                " where LeadStatusGuid = @LkpLeadStatusGuid"; 

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter LkpLeadStatusGuid={LkpLeadStatusGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _LkpLeadStatus = await connection.QueryFirstAsync<LkpLeadStatus>(query, new{LkpLeadStatusGuid});

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _LkpLeadStatus;
            }
        }

        public async Task<IEnumerable<LkpLeadStatus>> GetLkpLeadStatus(SearchLkpLeadStatus _SearchLkpLeadStatus, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLkpLeadStatus";

            //Query Used by Dapper
            var whereClause = _SearchLkpLeadStatus.getWhereClause();

            var query =
                " SELECT  * from " + RepositoryTable + " "
                + whereClause;

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter _SearchLkpLeadStatus={JsonConvert.SerializeObject(_SearchLkpLeadStatus)}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _LkpLeadStatus = await connection.QueryAsync<LkpLeadStatus>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _LkpLeadStatus;
            }
        }

        
    }
}
