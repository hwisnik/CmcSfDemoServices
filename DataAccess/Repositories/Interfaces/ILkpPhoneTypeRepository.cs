using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Contact;
using Shared.Entities._SearchCriteria.Client;

namespace DataAccess.Repositories.Interfaces
{
    public interface ILkpPhoneTypeRepository
    {
        Task<LkpPhoneType> GetLkpPhoneTypeFromGuid(Guid LkpPhoneTypeGuid, LogCommand logCommand);
        Task<IEnumerable<LkpPhoneType>> GetLkpPhoneType(SearchLkpPhoneType _searchLkpPhoneType, LogCommand logCommand);
    }
}
