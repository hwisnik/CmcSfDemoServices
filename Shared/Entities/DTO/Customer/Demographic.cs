using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.DTO.Customer
{
    public class Demographic
    {
        public Guid DemographicGuid { get; set; }
        public Guid CustomerGuid { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
    }
}
