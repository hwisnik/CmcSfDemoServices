using System;
using System.Threading.Tasks;
using DataAccess.SFInterfaces;
using DataAccess.SFRepositories;
using Shared.Commands;
using Shared.Entities.CDCClient;
using Shared.EventPayloads;

namespace BusinessLogic.Services.Audit
{
    public static class AuditInsert
    {
        public static async Task<GenericServiceResponse> InsertAuditRecordAsync(LogCommand logCommand, PayloadParent payload, string objectFullName, string lastModifiedBy, ICdcRepository auditRepository)
        {
            var response = new GenericServiceResponse();
            var cdcAuditEntity = new CdcAuditEntity
            {
                AuditLogGuid = Guid.NewGuid(),
                CreatedById = payload.CreatedById,
                CreatedDate = payload.CreatedDate,
                EventType = payload.EventTypeC,
                ObjectType = objectFullName,
                LastModifiedById = lastModifiedBy,
                LastModifiedDate = DateTime.Now,
                RecordId = payload.RecordIdC,
            };
            var auditResponse = await auditRepository.InsertAuditRecord(cdcAuditEntity, logCommand);
            if (auditResponse == 1)
            {
                response.Success = true;
            }
            return response;
        }
    }
}
