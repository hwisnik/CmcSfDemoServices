using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Technician;
using Shared.Entities._SearchCriteria.Technician;
using Technician = Shared.Entities.Technician;

namespace DataAccess.SFInterfaces
{
    public interface ITechnicianRepository
    {
        //Technician Gets
        Task<Technician> GetTechnicianFromGuid(Guid TechnicianGuid, LogCommand logCommand);
        Task<IEnumerable<Technician>> GetGetTechnician (SearchTechnician TechnicianSearcher, LogCommand logCommand);

        //Slot Type
        Task<LkpSlotType> GetSlotTypeFromGuid(Guid SlotTypeGuid, LogCommand logCommand);

        //ScheduleDay
        Task<ScheduleDay> GetScheduleDayFromTechnicianGuid(Guid TechnicianGuid, DateTime TargetDay, LogCommand logCommand);
        Task<ScheduleDay> GetScheduleDayFromScheduleDayGuid(Guid ScheduleDayGuid, LogCommand logCommand);
        Task<IEnumerable<ScheduleDay>> GetScheduleDays (SearchScheduleDay ScheduleDaySearcher, LogCommand logCommand);

        //Slot
        Task<Slot> GetSlotFromGuid(Guid SlotGuid, LogCommand logCommand);
        Task<IEnumerable<Slot>> GetSlots (SearchSlots SlotSearcher, LogCommand logCommand);
        
    }
}
