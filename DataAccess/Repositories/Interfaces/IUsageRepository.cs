using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Entities._SearchCriteria.Client;
using Shared.EventPayloads;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUsageRepository
    {
        Task<IEnumerable<Usage>> GetUsage(SearchUsage _searchUsage, LogCommand logCommand);
        Task<IEnumerable<Usage>> GetUsageFromCustomerGuid(Guid CustomerGuid, LogCommand logCommand);
        Task<CapQualified> GetIsCapQualifiedFromCustomerGuid(Guid CustomerGuid, LogCommand logCommand);

        //Update
        Task<int> UpdateUsageFromLeadCDC(LeadPayload CDCResponse, LogCommand logCommand);
    }
}
