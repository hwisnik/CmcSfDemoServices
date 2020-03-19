using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Customer;
using Shared.Entities.DTO.Helper;
using Shared.Entities.InputObjects.Lead;
using Shared.Entities.SFDb.Lead;
using Shared.Entities._SearchCriteria.Client;

namespace DataAccess.SFInterfaces
{
    public interface ILeadRepository
    {
        Task<Lead> GetLead(Guid leadGuid, LogCommand logCommand);
        Task<IEnumerable<Lead>> GetLeads(Guid customerGuid, LogCommand logCommand);
        Task<Lead> GetDetailedLead(Guid customerGuid, LogCommand logCommand);
        Task<AGuidForYou> GetMeAGuidPlease(LogCommand logCommand);

        Task<LkpLeadStatus> GetLkpLeadStatusFromName(string leadStatusName, LogCommand logCommand);
        Task<LkpAuditType> GetLkpLeadAuditTypeFromName(string auditTypeName, LogCommand logCommand);

        Task<IEnumerable<SfPrioritizedLead>> GetPrioritizedLeadsByUsage(PrioLeadInput input, LogCommand logCommand);
        Task<IEnumerable<SfPrioritizedLead>> GetPrioritizedLeadsByPostalCode(PrioLeadInput input, LogCommand logCommand);
        Task<IEnumerable<SfPrioritizedLead>> GetPrioritizedLeadsForLeepNoPostalCodes(PrioLeadInput input, LogCommand logCommand);


        Task<IEnumerable<SFSimpleLeadResults>> GetSfLeads(SearchLeadSimple leadSearcher, LogCommand logCommand);

        
        //Update
        Task<int> UpdateLeadAsync(Lead lead, LogCommand logCommand);
        Task<int> InsertLead(Lead lead, LogCommand logCommand);

        Task<int> SetLeadStatusToConverted(Guid customerGuid, LogCommand logCommand);
        Task<int> LeadCreatedSetRecordId(Guid customerGuid, string sfLeadId, LogCommand logCommand);
        Task<IEnumerable<Lead>> GetLeadByCustomerGuidAsync(Guid customerGuid, LogCommand logCommand);
        Task<int> UpdateReservedDateAsync(Guid customerGuid, DateTime reserveDateTime, LogCommand logCommand, IDbTransaction trans);


    }
}