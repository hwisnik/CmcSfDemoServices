using System.Collections.Generic;
using Shared.Entities.DTO.Customer;

namespace Shared.Entities
{
    public class Slot
    {
        public int SlotPosition { get; set; }
        public bool Filled { get; set; }
        public Lead Lead { get; set; }
        public List<PotentialLead> PotentialLeadList { get; set; }
    }
}
