using System;

namespace Shared.Entities.DTO.Technician
{
    public class ScheduleDay
    {
        public Guid ScheduleDayGuid { get; set; }
        public DateTime Date { get; set; }
        public Guid TechnicianGuid { get; set; }
        public Guid? Slot0Guid { get; set; }
        public Guid? Slot1Guid { get; set; }
        public Guid? Slot2Guid { get; set; }
        public Guid? Slot3Guid { get; set; }
        public Guid? Slot4Guid { get; set; }
        public Guid? Slot5Guid { get; set; }
        public Guid? Slot6Guid { get; set; }
        public Guid? Slot7Guid { get; set; }
    }
}
