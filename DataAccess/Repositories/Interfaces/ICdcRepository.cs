using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.CDCClient;

namespace DataAccess.Repositories.Interfaces
{
    public interface ICdcRepository
    {
        Task<int> InsertAuditRecord(CdcAuditEntity auditEntity, LogCommand logCommand);
    }
}