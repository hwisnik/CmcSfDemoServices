using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.DTO.Customer
{
    public class LkpCounties
    {
        public Guid CountyGuid { get; set; }
        public Guid CountyTypeGuid { get; set; }
        public string CountyCode { get; set; }
        public string CountyDescription { get; set; }
        public string ReportingCountyCode { get; set; }
        public bool IsActive { get; set; }
    }
}
