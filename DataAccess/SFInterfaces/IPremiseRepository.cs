using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Entities._SearchCriteria.Address;
using Shared.EventPayloads;

namespace DataAccess.SFInterfaces
{
    //public interface IPremiseRepository
    //{
    //    Task<Premise> GetPremiseFromGuid(Guid PremiseGuid, LogCommand logCommand);
    //    Task<Premise> GetSinglePremiseFromCustomerGuid(Guid CustomerGuid, LogCommand logCommand);

    //    Task<List<Premise>> GetPremiseFromPremiseGuid(Guid PremiseGuid, LogCommand logCommand);
    //    Task<List<Premise>> GetPremiseFromCustomerGuid(Guid CustomerGuid, LogCommand logCommand);
    //    Task<List<Premise>> GetPremiseFromLeadGuid(Guid CustomerLeadGuid, LogCommand logCommand);
    //    Task<List<Premise>> GetPremiseFromSFAccountID(string SFAccountID, LogCommand logCommand);
    //    Task<List<Premise>> GetPremiseFromSFLeadID(string SFLeadID, LogCommand logCommand);

    //    Task<IEnumerable<Premise>> GetPremise(SearchPremise _searchPremise, LogCommand logCommand);
    //    Task<LkpPremiseType> GetPremiseType(string SFFieldNameOrID, LogCommand logCommand);

    //    //Update
    //    Task<int> UpdatePremise(Premise premise, LogCommand logCommand);
    //    Task<int> InsertPremise(Premise premise, LogCommand logCommand);
    //}
}
