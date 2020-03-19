using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.EventPayloads;

namespace BusinessLogic.Services.Interfaces
{
    public interface ISchedulingService
    {
        Task<GenericServiceResponse> GetSchedulingList(
            LogCommand logCommand);

        //Updates and Inserts
        Task<GenericServiceResponse> ServiceAppointmentUpdate(
            PayloadParent ResponseObject,
            LogCommand logCommand);

        Task<GenericServiceResponse> SimpleServiceAppointmentHandler(PayloadParent responseObject,
            LogCommand logCommand);
    }
}
