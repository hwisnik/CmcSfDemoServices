using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Entities._SearchCriteria.Address;

namespace DataAccess.Repositories.Interfaces
{
    public interface ILkpAddressTypeRepository
    {
        Task<LkpAddressType> GetLkpAddressTypeFromGuid(Guid addressGuid, LogCommand logCommand);
        Task<IEnumerable<LkpAddressType>> GetLkpAddressType(SearchLkpAddressType _searchLkpAddressType, LogCommand logCommand);
    }
}
