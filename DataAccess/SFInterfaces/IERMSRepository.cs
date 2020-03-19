using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.ERMS;

namespace DataAccess.SFInterfaces
{
    public interface IERMSRepository
    {
        Task<ERMSCustomer> GetERMSCustomer(Guid CustomerGuid, LogCommand logCommand);
    }
}
