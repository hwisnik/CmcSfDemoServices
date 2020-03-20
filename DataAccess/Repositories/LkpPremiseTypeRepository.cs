using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using DataAccess.Repositories.Interfaces;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Entities._SearchCriteria.Address;
using Shared.Handlers;

namespace DataAccess.Repositories
{
    public class LkpPremiseTypeRepository : ILkpPremiseTypeRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        // Repository table(s) for dapper
        private const string RepositoryTable = " [Address].[LkpPremiseType] ";

        //Logging String Helpers
        private const string Repository = "LkpPremiseTypeRepository";

        public LkpPremiseTypeRepository(IConnectionFactory connectionFactory,
            LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler),
                              "logging commandhandlerDecorator instance is null");
        }

        public async Task<LkpPremiseType> GetLkpPremiseTypeFromGuid(Guid LkpPremiseTypeGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLkpPremiseTypeFromGuid";

            //Query Used by Dapper
            var query =
                " SELECT  * from "     +  RepositoryTable    + 
                " where PremiseTypeId = @LkpPremiseTypeGuid"; 

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter LkpPremiseTypeGuid={LkpPremiseTypeGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _LkpPremiseType = await connection.QueryFirstAsync<LkpPremiseType>(query, new{LkpPremiseTypeGuid});

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _LkpPremiseType;
            }
        }

        public async Task<IEnumerable<LkpPremiseType>> GetLkpPremiseType(SearchLkpPremiseType _SearchLkpPremiseType, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLkpPremiseType";

            //Query Used by Dapper
            var whereClause = _SearchLkpPremiseType.getWhereClause();

            var query =
                " SELECT  * from " + RepositoryTable + " "
                + whereClause;

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter _searchAddress={JsonConvert.SerializeObject(_SearchLkpPremiseType)}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _LkpPremiseType = await connection.QueryAsync<LkpPremiseType>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _LkpPremiseType;
            }
        }
        
    }
}
