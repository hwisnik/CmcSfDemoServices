using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Entities._SearchCriteria.Client;

namespace DataAccess.Repositories.Interfaces
{
    public interface ILkpAccountStatusRepository
    {
        Task<LkpAccountStatus> GetLkpAccountStatusFromGuid(Guid LkpAccountStatusGuid, LogCommand logCommand);
        Task<IEnumerable<LkpAccountStatus>> GetLkpAccountStatus(SearchLkpAccountStatus _searchLkpAccountStatus, LogCommand logCommand);
    }
}
