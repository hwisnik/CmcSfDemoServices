using System;
using System.Collections.Generic;

namespace Shared.Entities
{
    public class DaySLots
    {
        public DateTime Date { get; set; }
        public List<Slot> Slots { get; set; }
    }
}
