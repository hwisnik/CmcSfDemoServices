using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.CDCClient;
using Shared.Entities.DTO.Customer;

namespace DataAccess.SFInterfaces
{
    public interface ICdcRepository
    {
        Task<int> InsertAuditRecord(CdcAuditEntity auditEntity, LogCommand logCommand);
    }
}