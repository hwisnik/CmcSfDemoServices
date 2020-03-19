using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.DTO.Customer
{
    public class CapQualified
    {
        public Guid CustomerGuid { get; set; }
        public bool? IsCapQualified { get; set; } = false; 
    }
}
