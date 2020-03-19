
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Shared.Commands;
using Shared.EventPayloads;

namespace BusinessLogic.Services.Interfaces
{
    public interface ICustomerService
    {
        //Updates and Inserts
        Task<GenericServiceResponse> CdcEventAddressCreate(
            PayloadParent payloadParent,
            LogCommand logCommand);

        Task<GenericServiceResponse> CdcEventAddressUpdate(
            PayloadParent payloadParent,
            LogCommand logCommand);

        Task<GenericServiceResponse> CdcEventContactCreate(
            JObject cdcPayload,
            LogCommand logCommand);

        Task<GenericServiceResponse> CdcEventContactUpdate(
            JObject cdcPayload,
            LogCommand logCommand);

        Task<GenericServiceResponse> TaskEventInsertUpdate(PayloadParent payloadResponse, LogCommand logCommand);
    }
}
