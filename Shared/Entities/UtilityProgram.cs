using System.Collections.Generic;

namespace Shared.Entities
{
    public class UtilityProgram
    {
        public int ProgramId { get; set; }
        public string Description { get; set; }
        public List<Technician> Technicians { get; set; }
    }
}
