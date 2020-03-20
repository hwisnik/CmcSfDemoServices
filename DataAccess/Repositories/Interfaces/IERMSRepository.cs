using System;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.ERMS;

namespace DataAccess.Repositories.Interfaces
{
    public interface IERMSRepository
    {
        Task<ERMSCustomer> GetERMSCustomer(Guid CustomerGuid, LogCommand logCommand);
    }
}
