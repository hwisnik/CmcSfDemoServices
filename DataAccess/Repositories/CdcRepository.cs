using System;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Infrastructure;
using DataAccess.Repositories.Interfaces;
using Newtonsoft.Json;
using Shared.Commands;
using Shared.Entities.CDCClient;
using Shared.Handlers;

namespace DataAccess.Repositories
{
    public class CdcRepository :ICdcRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;

        // Repository table(s) for dapper
        private const string LeadTable = " [Customer].[LEAD] ";
        private const string LeadStatusTable = " [Customer].[LkpLeadStatus] ";
        private const string LeadAuditTypeTable = " [Customer].[LkpAuditType] ";
        //Logging String Helpers
        private const string Repository = "cdcRepository";

        public CdcRepository(IConnectionFactory connectionFactory, LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler), "logging commandhandlerDecorator instance is null");
        }

        public async Task<int> InsertAuditRecord(CdcAuditEntity auditEntity, LogCommand logCommand)
        {
            //Logging Helper for Method
            var method_name = "InsertAuditRecord";

            var query = " insert into [CDCQueue].[AuditLog] " +
                        " ([AuditLogGuid]  " +
                        ", [ObjectType] " +
                        ", [RecordID] " +
                        ", [EventType] " +
                        ", [CreatedDate] " +
                        ", [CreatedById] " +
                        ", [LastModifiedDate] " +
                        ", [LastModifiedById]) " +
                        "  Values " +
                        " (@AuditLogGuid, @ObjectType, @RecordID, @EventType, @CreatedDate, @CreatedById, @LastModifiedDate, @LastModifiedById); ";


            //Log the input
            logCommand.LogMessage = $"{Repository}.{method_name} Starting input parameters _Lead = {JsonConvert.SerializeObject(auditEntity)}" + Environment.NewLine +
                                    $"Query: {query}";
            _logHandler.HandleLog(logCommand);


            using (var connection = _connectionFactory.GetConnection)
            {
                //Await the response
                var returnValue = await connection.ExecuteAsync(query, auditEntity);

                connection.Close();

                //Log the output
                logCommand.LogMessage = $"{Repository}.{method_name} completed";
                _logHandler.HandleLog(logCommand);

                return returnValue;
            }
        }
    }
}