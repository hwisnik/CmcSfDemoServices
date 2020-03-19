using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.DTO.Customer
{
    public class Usage
    {
        public Guid UsageGuid { get; set; }
        public Guid CustomerGuid { get; set; }
        public string Rate { get; set; }
        public string RateDescription { get; set; }
        public bool IsActive { get; set; }
        public double? AverageUsage { get; set; }
        public int? UsageDays { get; set; }
        public double? HeatingAverageUsage { get;set; }
        public int? HeatingUsageDays { get; set; }
    }
}
