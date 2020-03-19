using System;

namespace Shared.Entities.DTO.Technician
{
    public class Slot
    {
        public Guid SlotGuid { get; set; }
        public DateTime? Date { get; set; }
        public int SlotPosition { get; set; }
        public Guid SlotTypeId { get; set; }
        public bool Filled { get; set; }
        public Guid? LeadGuid { get; set; }
        public Guid? TechnicianGuid { get; set; }
    }
}
