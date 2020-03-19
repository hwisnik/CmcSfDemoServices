using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using DataAccess.SFInterfaces;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Entities.DTO.Program;
using Shared.Entities._SearchCriteria.Customer;
using Shared.Handlers;

namespace DataAccess.SFRepositories
{
    public class ProgramRepository: IProgramRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        // Repository table(s) for dapper
        private const string ProgramTable = " [Program].[Program] ";
        private const string SubProgramTable = " [Program].[Subprogram] ";

        //Logging String Helpers
        private const string Repository = "ProgramRepository";

        public ProgramRepository(IConnectionFactory connectionFactory, LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler), "logging commandhandlerDecorator instance is null");
        }
        
        public async Task<Program> GetProgramFromAccountGuid(Guid AccountGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetProgramFromAccountGuid";

            //Query Used by Dapper
            var query = " select * from "+ProgramTable+" where AccountGuid = @AccountGuid" ;

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter AccountGuid={AccountGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var Program = await connection.QueryFirstAsync<Program>(query, new {AccountGuid});

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return Program;
            }
        }

        public async Task<Program> GetProgramFromGuid(Guid ProgramGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetProgramFromGuid";

            //Query Used by Dapper
            var query = " select * from "+ProgramTable+" where ProgramID = @ProgramGuid";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter ProgramGuid={ProgramGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var Program = await connection.QueryFirstAsync<Program>(query, new {ProgramGuid});

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return Program;
            }
        }

        public async Task<Program> GetProgramNameFromProgramGuid(Guid? ProgramGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetProgramNameFromProgramGuid";

            //Query Used by Dapper
            var query =
                $" select * from {ProgramTable} " +
                " where ProgramId = @ProgramGuid  ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter ProgramGuid={ProgramGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _Program = await connection.QueryFirstAsync<Program>(query, new { ProgramGuid });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _Program;
            }
        }

        public async  Task<Program> GetProgramFromSFID(string SFProgramRecordID, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetProgramGuid";

            //Query Used by Dapper
            var query =
                $" select * from {ProgramTable} " +
                " where SfProgramId = @SFProgramRecordID  ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter" + 
                                    Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _Program = await connection.QueryFirstAsync<Program>(query, new {SFProgramRecordID});

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _Program;
            }
        }

        public async Task<Subprogram> GetSubProgramSFID(string SFSubprogramRecordID, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetSubProgramGuid";

            //Query Used by Dapper
            var query =
                $" select * from {SubProgramTable} " +
                " where SfSubProgramId = @SFSubprogramRecordID  ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _SubProgram = await connection.QueryFirstAsync<Subprogram>(query, new {SFSubprogramRecordID});

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _SubProgram;
            }
        }

        public async Task<IEnumerable<Program>> GetPrograms(LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetPrograms";

            //Query Used by Dapper
            var query =
                $" select * from {ProgramTable} ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _Program = await connection.QueryAsync<Program>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _Program;
            }
        }

        public async Task<IEnumerable<Subprogram>> GetSubPrograms(LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetSubPrograms";

            //Query Used by Dapper
            var query =
                $" select * from {SubProgramTable} ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _SubProgram = await connection.QueryAsync<Subprogram>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _SubProgram;
            }
        }

        public async Task<Subprogram> GetSubProgramNameFromSubProgramGuid(Guid? SubProgramGuid, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetSubProgramNameFromSubProgramGuid";

            //Query Used by Dapper
            var query =
                $" select * from {SubProgramTable} " +
                " where SubProgramId = @SubProgramGuid  ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting input parameter SubProgramGuid={SubProgramGuid}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _SubProgram = await connection.QueryFirstAsync<Subprogram>(query, new { SubProgramGuid });

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _SubProgram;
            }
        }

        public Task<Program> GetProgramFromCustomerGuid(Guid CustomerGuid, LogCommand logCommand)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Program>> FindProgram(SearchProgram _SearchProgram, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "GetClientAsync";

            //Query Used by Dapper
            var query =
                " SELECT  * from " + ProgramTable +
                _SearchProgram.getWhereClause();
                

            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameter _SearchProgram={JsonConvert.SerializeObject(_SearchProgram)}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _Program = await connection.QueryAsync<Program>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return _Program;
            }
        }

        public async Task<IEnumerable<Subprogram>> GetProgramAndSubprogramBySfSubProgramId(string sfSubProgramId, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "GetProgramAndSubprogramBySfSubProgramId";

            //Query Used by Dapper
            var query =
                " SELECT ProgramGuid, SubProgramGuid, SubProgramName from Program.SubProgram " +
                $" WHERE SfSubProgramId = '{sfSubProgramId}'" ;

            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameter: {sfSubProgramId} + Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var program = await connection.QueryAsync<Subprogram>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return program;
            }
        }

    }
}