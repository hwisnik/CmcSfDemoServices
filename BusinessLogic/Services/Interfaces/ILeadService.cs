using Shared.Commands;
using Shared.Entities.InputObjects.Lead;
using Shared.EventPayloads;
using System;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface ILeadService
    {

        Task<GenericServiceResponse> GetLead(Guid leadGuid,LogCommand logCommand);

        Task<GenericServiceResponse> GetLead(Shared.Entities._SearchCriteria.Client.SearchLeadSimple LeadSearcher,LogCommand logCommand);

        Task<GenericServiceResponse> GetLeadDetailed(Guid CustomerGuid,LogCommand logCommand);

        //Updates and Inserts
        Task<GenericServiceResponse> ConvertLeadToAccount(PayloadParent ResponseObject,LogCommand logCommand);

        Task<GenericServiceResponse> UpdateLeadFromUpdateEvent(PayloadParent ResponseObject,LogCommand logCommand);

        Task<GenericServiceResponse> UpsertLeadFromInsertEvent(PayloadParent ResponseObject,LogCommand logCommand);
    }
}