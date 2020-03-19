//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Dapper;
//using DataAccess.Infrastructure;
//using DataAccess.SFInterfaces;
//using Newtonsoft.Json;
//using Shared.Commands;
//using Shared.Entities.SFDb.Lead;
//using Shared.Entities._SearchCriteria.Address;
//using Shared.Handlers;
//using Shared.Entities.DTO.Customer;
//using Shared.EventPayloads;

//namespace DataAccess.SFRepositories
//{
//    public class PremiseRepository : IPremiseRepository
//    {
//        readonly IConnectionFactory _connectionFactory;
//        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

//        // Repository table(s) for dapper
//        private const string RepositoryTable = " [Customer].[Premise] ";
//        public const string PremiseTypeTable = " [Customer].[LkpPremiseType] ";

//        //Logging String Helpers
//        private const string Repository = "PremiseRepository";

//        public PremiseRepository(IConnectionFactory connectionFactory,
//            LoggingCommandHandlerDecorator<LogCommand> loghandler)
//        {
//            _connectionFactory = connectionFactory;
//            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler),
//                              "logging commandhandlerDecorator instance is null");
//        }

//        public async Task<Premise> GetPremiseFromGuid(Guid PremiseGuid, LogCommand logCommand)
//        {
//            //Logging Helper for Method
//            const string methodName = "GetPremiseFromGuid";

//            //Query Used by Dapper
//            var query =
//                " SELECT  * from "     +  RepositoryTable    + 
//                " where PremiseId = @PremiseGuid"; 

//            //Log the input
//            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter PremiseGuid={PremiseGuid}" + Environment.NewLine +
//                                    $"Query: {query}";
//            _logHandler.HandleLog(logCommand);


//            using (var connection = _connectionFactory.GetConnection)
//            {
//                //Await the response
//                var _Premise = await connection.QueryFirstAsync<Premise>(query, new{PremiseGuid});

//                connection.Close();

//                //Log the output
//                logCommand.LogMessage = $"{Repository}.{methodName} completed";
//                _logHandler.HandleLog(logCommand);

//                return _Premise;
//            }
//        }
        
//        public async Task<Premise> GetSinglePremiseFromCustomerGuid(Guid CustomerGuid, LogCommand logCommand)
//        {
//            //Logging Helper for Method
//            const string methodName = "GetPremiseFromGuid";

//            //Query Used by Dapper
//            var query =
//                " SELECT  * from " +  RepositoryTable + 
//                " where CustomerGuid = @CustomerGuid"; 

//            //Log the input
//            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter CustomerGuid={CustomerGuid}" + Environment.NewLine +
//                                    $"Query: {query}";
//            _logHandler.HandleLog(logCommand);


//            using (var connection = _connectionFactory.GetConnection)
//            {
//                //Await the response
//                var _Premise = await connection.QueryFirstAsync<Premise>(query, new{CustomerGuid});

//                connection.Close();

//                //Log the output
//                logCommand.LogMessage = $"{Repository}.{methodName} completed";
//                _logHandler.HandleLog(logCommand);

//                return _Premise;
//            }
//        }

//        public Task<List<SFDetailedPremise>> GetDetailedPremiseFromCustomerGuid(Guid CustomerGuid, LogCommand logCommand)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<SFDetailedPremise>> GetDetailedPremiseFromPremiseGuid(Guid PremiseGuid, LogCommand logCommand)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<Premise>> GetPremiseFromPremiseGuid(Guid PremiseGuid, LogCommand logCommand)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<List<Premise>> GetPremiseFromAccountGuid(Guid CustomerAccountId, LogCommand logCommand)
//        {
//            //Logging Helper for Method
//            const string methodName = "GetPremiseFromGuid";

//            //Query Used by Dapper
//            var query =
//                " SELECT  * from "     +  RepositoryTable    + 
//                " where CustomerAccountId = @CustomerAccountId"; 

//            //Log the input
//            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter CustomerAccountId={CustomerAccountId}" + Environment.NewLine +
//                                    $"Query: {query}";
//            _logHandler.HandleLog(logCommand);


//            using (var connection = _connectionFactory.GetConnection)
//            {
//                //Await the response
//                var _Premise = await connection.QueryAsync<Premise>(query, new{CustomerAccountId});

//                connection.Close();

//                //Log the output
//                logCommand.LogMessage = $"{Repository}.{methodName} completed";
//                _logHandler.HandleLog(logCommand);

//                return _Premise.ToList();
//            }
//        }

//        public Task<List<Premise>> GetPremiseFromLeadGuid(Guid CustomerLeadGuid, LogCommand logCommand)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<Premise>> GetPremiseFromSFAccountID(string SFAccountID, LogCommand logCommand)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<Premise>> GetPremiseFrom
//            (string SFAccountID, LogCommand logCommand)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<Premise>> GetPremiseFromSFLeadID(string SFLeadID, LogCommand logCommand)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<List<Premise>> GetPremiseFromCustomerGuid(Guid CustomerGuid, LogCommand logCommand)
//        {
//            //Logging Helper for Method
//            const string methodName = "GetPremiseFromGuid";

//            //Query Used by Dapper
//            var query =
//                " SELECT  * " + 
//                    " FROM [Customer].[Premise] as _Premise " +
//                " join [Customer].LkpPremiseType as  _PremiseType " +
//                " on _PremiseType.PremiseTypeGuid = _Premise.PremiseTypeGuid " +
//                " where _Premise.CustomerGuid = @CustomerGuid"; 

//            //Log the input
//            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter CustomerGuid={CustomerGuid}" + Environment.NewLine +
//                                    $"Query: {query}";
//            _logHandler.HandleLog(logCommand);


//            using (var connection = _connectionFactory.GetConnection)
//            {
//                //Await the response
//                var _Premise = await connection.QueryAsync<Premise>(query, new{CustomerGuid});

//                connection.Close();

//                //Log the output
//                logCommand.LogMessage = $"{Repository}.{methodName} completed";
//                _logHandler.HandleLog(logCommand);

//                return _Premise.ToList();
//            }
//        }

//        public Task<List<SFDetailedPremise>> GetDetailedPremiseFromLeadGuid(Guid CustomerLeadGuid, LogCommand logCommand)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<SFDetailedPremise>> GetDetailedPremiseFromSFAccountID(string SFAccountID, LogCommand logCommand)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<SFDetailedPremise>> GetDetailedPremiseFromSFLeadID(string SFLeadID, LogCommand logCommand)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<LkpPremiseType> GetPremiseType(string SFFieldNameOrID, LogCommand logCommand)
//        {
//            //Logging Helper for Method
//            const string methodName = "GetPremiseType";


//            var query =
//                $" SELECT  * from {PremiseTypeTable} " +
//                $" where PremiseTypeName = @SFFieldNameOrID "; 

//            //Log the input
//            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter SFFieldNameOrID={SFFieldNameOrID}" + 
//                                    Environment.NewLine +
//                                    $"Query: {query}";
//            _logHandler.HandleLog(logCommand);


//            using (var connection = _connectionFactory.GetConnection)
//            {
//                //Await the response
//                var _PremiseType = await connection.QueryFirstAsync<LkpPremiseType>(query);

//                connection.Close();

//                //Log the output
//                logCommand.LogMessage = $"{Repository}.{methodName} completed";
//                _logHandler.HandleLog(logCommand);

//                return _PremiseType;
//            }
//        }

//        public async Task<IEnumerable<Premise>> GetPremise(SearchPremise _searchPremise, LogCommand logCommand)
//        {
//            //Logging Helper for Method
//            const string methodName = "GetPremise";

//            //Query Used by Dapper
//            var whereClause = _searchPremise.getWhereClause();

//            var query =
//                " SELECT  * from " + RepositoryTable + " "
//                + whereClause;

//            //Log the input
//            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter _searchAddress={JsonConvert.SerializeObject(_searchPremise)}" + 
//                                    Environment.NewLine +
//                                    $"Query: {query}";
//            _logHandler.HandleLog(logCommand);

//            using (var connection = _connectionFactory.GetConnection)
//            {
//                //Await the response
//                var _Premise = await connection.QueryAsync<Premise>(query);

//                connection.Close();

//                //Log the output
//                logCommand.LogMessage = $"{Repository}.{methodName} completed";
//                _logHandler.HandleLog(logCommand);

//                return _Premise;
//            }
//        }

//        public async Task<int> UpdatePremise(Premise premise, LogCommand logCommand)
//        {
//            //Logging Helper for Method
//            var method_name = "UpdatePremise";

//            var query = $"update {RepositoryTable}  " +
//                        "set " +
//                        "[PremiseTypeGuid] = @PremiseTypeGuid " +
//                        ",[IsOwnedByPrimary] = @IsOwnedByPrimary " +
//                        ",[LandLordConsentObtained] = @LandLordConsentObtained " +
//                        "where [CustomerGuid] = @CustomerGuid";
            
//            //Log the input
//            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters premise = " +
//                                    $"{JsonConvert.SerializeObject(premise, Formatting.Indented)} " +
//                                    Environment.NewLine +
//                                    $"Query: {query}";
//            _logHandler.HandleLog(logCommand);


//            using (var connection = _connectionFactory.GetConnection)
//            {
//                //Await the response
//                var _ReturnValue = await connection.ExecuteAsync(query);

//                connection.Close();

//                //Log the output
//                logCommand.LogMessage = $"{Repository}.{method_name} completed";
//                _logHandler.HandleLog(logCommand);

//                return _ReturnValue;
//            }
//        }
        
//        public async Task<int> InsertPremise(Premise premise, LogCommand logCommand)
//        {
//            //Logging Helper for Method
//            var method_name = "InsertPremise";

//            var query = "insert into [Customer].[Premise]  " +
//                        "( [PremiseGuid]" +
//                        ", [PremiseTypeGuid]  " +
//                        ", [IsOwnedByPrimary] " +
//                        ", [LandLordConsentObtained] " +
//                        ", [CustomerGuid] )" +
//                        "VALUES (" +
//                        " NEWID()" +
//                        ",@PremiseTypeGuid " +
//                        ",@IsOwnedByPrimary " +
//                        ",@LandLordConsentObtained " +
//                        ",@CustomerGuid )" ;
            
//            //Log the input
//            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters premise = " +
//                                    $"{JsonConvert.SerializeObject(premise, Formatting.Indented)} " +
//                                    Environment.NewLine +
//                                    $"Query: {query}";
//            _logHandler.HandleLog(logCommand);


//            using (var connection = _connectionFactory.GetConnection)
//            {
//                //Await the response
//                var _ReturnValue = await connection.ExecuteAsync(query);

//                connection.Close();

//                //Log the output
//                logCommand.LogMessage = $"{Repository}.{method_name} completed";
//                _logHandler.HandleLog(logCommand);

//                return _ReturnValue;
//            }
//        }

//    }
//}
