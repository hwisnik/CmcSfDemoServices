using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Entities._SearchCriteria.Address;

namespace DataAccess.SFInterfaces
{
    public interface ILkpOwnerTypeRepository
    {
        Task<LkpOwnerType> GetLkpOwnerTypeFromGuid (Guid LkpOwnerTypeGuid, LogCommand logCommand);
        Task<IEnumerable<LkpOwnerType>> GetLkpOwnerType (SearchLkpOwnerType _SearchLkpOwnerType, LogCommand logCommand);
    }
}

