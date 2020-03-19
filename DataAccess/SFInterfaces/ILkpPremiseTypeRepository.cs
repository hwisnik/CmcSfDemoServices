using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Entities._SearchCriteria.Address;

namespace DataAccess.SFInterfaces
{
    public interface ILkpPremiseTypeRepository
    {
        Task<LkpPremiseType> GetLkpPremiseTypeFromGuid(Guid addressGuid, LogCommand logCommand);
        Task<IEnumerable<LkpPremiseType>> GetLkpPremiseType(SearchLkpPremiseType _searchLkpPremiseType, LogCommand logCommand);
    }
}
