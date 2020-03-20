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
    public class LkpOwnerTypeRepository : ILkpOwnerTypeRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        // Repository table(s) for dapper
        private const string RepositoryTable = " [Address].[LkpOwnerType] ";

        //Logging String Helpers
        private const string Repository = "LkpOwnerTypeRepository";

        public LkpOwnerTypeRepository(IConnectionFactory connectionFactory,
            LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler),
                              "logging commandhandlerDecorator instance is null");
        }

        public async Task<LkpOwnerType> GetLkpOwnerTypeFromGuid(Guid LkpOwnerTypeGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLkpOwnerTypeFromGuid";

            //Query Used by Dapper
            var query =
                " SELECT  * from "     +  RepositoryTable    + 
                " where OwnerTypeId = @LkpOwnerTypeGuid" ; 

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter LkpOwnerTypeGuid={LkpOwnerTypeGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _LkpOwnerType = await connection.QueryFirstAsync<LkpOwnerType>(query, new{LkpOwnerTypeGuid});

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _LkpOwnerType;
            }
        }
        

        public async Task<IEnumerable<LkpOwnerType>> GetLkpOwnerType(SearchLkpOwnerType _SearchLkpOwnerType, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLkpOwnerType";

            //Query Used by Dapper
            var whereClause = _SearchLkpOwnerType.getWhereClause();

            var query =
                " SELECT  * from " + RepositoryTable + " "
                + whereClause;

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter _SearchLkpOwnerType={JsonConvert.SerializeObject(_SearchLkpOwnerType)}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _LkpOwnerType = await connection.QueryAsync<LkpOwnerType>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _LkpOwnerType;
            }
        }

        
    }
}
