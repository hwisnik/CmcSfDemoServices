using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Entities._SearchCriteria.Client;

namespace DataAccess.SFInterfaces
{
    public interface ILkpLeadStatusRepository
    {
        Task<LkpLeadStatus> GetLkpLeadStatusFromGuid(Guid LkpLeadStatusGuid, LogCommand logCommand);
        Task<IEnumerable<LkpLeadStatus>> GetLkpLeadStatus(SearchLkpLeadStatus _searchLkpLeadStatus, LogCommand logCommand);
    }
}
