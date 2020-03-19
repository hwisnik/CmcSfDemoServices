using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Entities.DTO.Program;
using Shared.Entities.SFDb.Lead;
using Shared.Entities._SearchCriteria.Customer;
using Shared.EventPayloads;

namespace DataAccess.SFInterfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetAccountFromGuid(Guid CustomerGuid, LogCommand logCommand);
        Task<Customer> GetDetailedCustomerFromGuid(Guid CustomerGuid, LogCommand logCommand);

        Task<IEnumerable<LkpAccountType>> GetCustomerAccountTypes(LogCommand logCommand);
        Task<LkpAccountType> GetCustomerAccountTypeFromName(string AccountTypeName, LogCommand logCommand);

        //Updates
        Task<int> UpdateAccountSFAccountIDFromLeadGuid(Guid LeadGuid, string SFAccountID, LogCommand logCommand);
        Task<int> UpdateCustomer(Customer customer, LogCommand logCommand);
        Task<int> InsertCustomer(Customer customer, LogCommand logCommand);

    }
}
