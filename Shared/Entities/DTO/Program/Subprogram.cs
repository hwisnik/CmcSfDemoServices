using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.DTO.Program
{
    public class Subprogram
    {
        public Guid SubProgramGuid { get; set; }
        public Guid ProgramGuid { get; set; }
        public string SubProgramName { get; set; }
    }
}
