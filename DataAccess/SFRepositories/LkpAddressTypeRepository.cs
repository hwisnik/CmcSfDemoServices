using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using DataAccess.SFInterfaces;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Entities._SearchCriteria.Address;
using Shared.Handlers;

namespace DataAccess.SFRepositories
{
    public class LkpAddressTypeRepository : ILkpAddressTypeRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        // Repository table(s) for dapper
        private const string RepositoryTable = " [Address].[LkpAddressType] ";

        //Logging String Helpers
        private const string Repository = "LkpAddressTypeRepository";

        public LkpAddressTypeRepository(IConnectionFactory connectionFactory,
            LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler),
                              "logging commandhandlerDecorator instance is null");
        }

        public async Task<LkpAddressType> GetLkpAddressTypeFromGuid(Guid LkpAddressTypeGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLkpAddressTypeFromGuid";

            //Query Used by Dapper
            var query =
                " SELECT  * from "     +  RepositoryTable    + 
                " where AddressTypeId = @LkpAddressTypeGuid"; 

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter LkpAddressTypeGuid={LkpAddressTypeGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _LkpAddressType = await connection.QueryFirstAsync<LkpAddressType>(query , new{LkpAddressTypeGuid});

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _LkpAddressType;
            }
        }

        public async Task<IEnumerable<LkpAddressType>> GetLkpAddressType(SearchLkpAddressType _SearchLkpAddressType, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLkpAddressType";

            //Query Used by Dapper
            var whereClause = _SearchLkpAddressType.getWhereClause();

            var query =
                " SELECT  * from " + RepositoryTable + " "
                + whereClause;

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter _SearchLkpAddressType={JsonConvert.SerializeObject(_SearchLkpAddressType)}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _LkpAddressType = await connection.QueryAsync<LkpAddressType>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _LkpAddressType;
            }
        }

        
    }
}
