using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using DataAccess.SFInterfaces;
using Shared.Commands;
using Shared.Entities.DTO.Scheduling;
using Shared.Handlers;

namespace DataAccess.SFRepositories
{
    public class SchedulingRepository : ISchedulingRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;
        //Logging String Helpers
        private const string Repository = "SchedulingRepository";

        // Repository table(s) for dapper
        private const string SchedulingTable = " [Scheduling].[Scheduling] ";
        private const string SimpleSchedulingTable = " [Scheduling].[SimpleScheduling]  ";
        private const string LkpSchedulingTypeTable = " [Scheduling].[LkpSchedulingType] ";
        private const string UsernameMappingSFtoADTable = " [Scheduling].[UsernameMappingSFtoAD] ";
        private const string WorkOrderScoreWeightingTable = " [Scheduling].[LKPWorkOrderScoreMapping] ";
       

        public SchedulingRepository(IConnectionFactory connectionFactory,
            LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler),
                              "logging commandhandlerDecorator instance is null");
        }

        public async Task<IEnumerable<SimpleScheduling>> GetSimpleScheduling(string SfRecordId, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetSimpleScheduling";

            //Query Used by Dapper
            var query = " SELECT  * from " + SimpleSchedulingTable + " where [SfRecordID] = @SfRecordID ";

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting: No Input Parameter " + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _returnModel = await connection.QueryAsync<SimpleScheduling>(query, new { SfRecordId } );

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _returnModel;
            }
        }
        public async Task<int> CreateSimpleSchedulingRecordFirst(SimpleScheduling scheduling, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "CreateNewSchedulingRecord";

            //Query Used by Dapper
            var query = $" IF not EXISTS (SELECT * FROM {SimpleSchedulingTable}  " +
                        " WHERE [SfRecordID] = @SfRecordID)  " +
                        " BEGIN " +
                        "     insert into [Scheduling].[SimpleScheduling]  " +
                        " 	( " +
                        " 	[SfRecordID], " +
                        " 	[SalesforceUser], " +
                        " 	[Technician], " +
                        " 	[WorkOrderType], " +
                        " 	[ScheduleBit], " +
                        " 	[ScheduleDateTime], " +
                        " 	[LastModifiedDateTime] " +
                        " 	) " +
                        " 	values " +
                        "   ( " +
                        " 	@SfRecordID, " +
                        " 	@SalesforceUser, " +
                        " 	@Technician, " +
                        " 	@WorkOrderType, " +
                        " 	'1', " +
                        " 	@ScheduleDateTime, " +
                        " 	@LastModifiedDateTime " +
                        " 	) " +
                        " END ";


            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting: No Input Parameter " + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _returnModel = await connection.ExecuteAsync(query, scheduling);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _returnModel;
            }
        }

        public async Task<int> CreateSimpleSchedulingRecordReschedule(SimpleScheduling scheduling, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "CreateNewSchedulingRecord";

            //Query Used by Dapper
            var query =
                $"insert into {SimpleSchedulingTable} " +
                " 	( " +
                " 	[SfRecordID], " +
                " 	[SalesforceUser], " +
                " 	[Technician], " +
                " 	[WorkOrderType], " +
                " 	[ScheduleBit], " +
                " 	[ScheduleDateTime], " +
                " 	[LastModifiedDateTime] " +
                " 	) " +
                " 	values " +
                "   ( " +
                " 	@SfRecordID, " +
                " 	@SalesforceUser, " +
                " 	@Technician, " +
                " 	@WorkOrderType, " +
                " 	'0', " +
                " 	@ScheduleDateTime, " +
                " 	@LastModifiedDateTime " +
                " 	) ";


            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting: No Input Parameter " + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _returnModel = await connection.ExecuteAsync(query, scheduling);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _returnModel;
            }
        }

        public async Task<IEnumerable<Scheduling>> GetSchedulingEnum(LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetSchedulingEnum";

            //Query Used by Dapper
            var query = " SELECT  * from " + SchedulingTable; 

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting: No Input Parameter " + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _returnModel = await connection.QueryAsync<Scheduling>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _returnModel;
            }
        }

        public async Task<IEnumerable<LkpSchedulingType>> GetLkpSchedulingTypeEnum(LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetLkpSchedulingTypeEnum";

            //Query Used by Dapper
            var query = " SELECT  * from " + LkpSchedulingTypeTable; 

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting: No Input Parameter " + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _returnModel = await connection.QueryAsync<LkpSchedulingType>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _returnModel;
            }
        }

        public async Task<IEnumerable<UsernameMappingSFtoAD>> GetUsernameMappingSFtoADEnum(LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetUsernameMappingSFtoADEnum";

            //Query Used by Dapper
            var query = " SELECT  * from " + UsernameMappingSFtoADTable; 

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting: No Input Parameter " + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _returnModel = await connection.QueryAsync<UsernameMappingSFtoAD>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _returnModel;
            }
        }

        public async Task<IEnumerable<LKPWorkOrderScoreMapping>> GetWorkOrderScoreWeightingEnum(LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "GetWorkOrderScoreWeightingEnum";

            //Query Used by Dapper
            var query = " SELECT  * from " + WorkOrderScoreWeightingTable; 

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting: No Input Parameter " + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _returnModel = await connection.QueryAsync<LKPWorkOrderScoreMapping>(query);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _returnModel;
            }
        }
        //
        public async Task<int> CreateNewSchedulingRecord(Scheduling scheduling, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "CreateNewSchedulingRecord";

            //Query Used by Dapper
            var query = $" insert into {SchedulingTable}  " +
                        "( " +
                        "  [SchedulingGuid] " +
                        ", [WorkOrderScoreWeightingGuid] " +
                        ", [UsernameMappingSFtoADGuid] " +
                        ", [LkpSchedulingTypeGuid] " +
                        ", [ProgramGuid] " +
                        ", [SubprogramGuid] " +
                        ", [SalesforceRecordID] " +
                        ", [CreatedDateTime]" +
                        ") " +
                        "Values" +
                        "( " +
                        "  newid() " +
                        ", @WorkOrderScoreWeightingGuid " +
                        ", @UsernameMappingSFtoADGuid " +
                        ", @LkpSchedulingTypeGuid " +
                        ", @ProgramGuid " +
                        ", @SubprogramGuid " +
                        ", @SalesforceRecordID " +
                        ", @CreatedDateTime" +
                        ")";
                       

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting: No Input Parameter " + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _returnModel =  await connection.ExecuteAsync(query, scheduling);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _returnModel;
            }
        }

        public async Task<int> CreateNewUsernameMappingSFtoADRecord(UsernameMappingSFtoAD usernameMappingSFtoAD, LogCommand logCommand)
        {
            //Logging Helper for Method
            const string methodName = "CreateNewUsernameMappingSFtoADRecord";

            //Query Used by Dapper
            var query = $" insert into {SchedulingTable}  " +
                        "( " +
                        "  [UsernameMappingSFtoADGuid] " +
                        ", [SalesForceUserRecordID] " +
                        ", [ADUserName] " +
                        ", [SFUsername] " +
                        ") " +
                        "Values" +
                        "( " +
                        "  @UsernameMappingSFtoADGuid " +
                        ", @SalesForceUserRecordID " +
                        ", @ADUserName " +
                        ", @SFUsername " +
                        ") ";
                       

            //Log the input
            logCommand.LogMessage = $"{Repository}.{methodName} Starting: No Input Parameter " + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var _returnModel =  await connection.ExecuteAsync(query, usernameMappingSFtoAD);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{methodName} completed";
                _logHandler.HandleLog(logCommand);

                return _returnModel;
            }
        }
    }
}
