using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Contact;
using Shared.Entities._SearchCriteria.Client;

namespace DataAccess.Repositories.Interfaces
{
    public interface ILkpContactTypeRepository
    {
        Task<LkpContactType> GetLkpContactTypeFromGuid(Guid LkpContactTypeGuid, LogCommand logCommand);
        Task<IEnumerable<LkpContactType>> GetLkpContactType(SearchLkpContactType _searchLkpContactType, LogCommand logCommand);
    }
}
