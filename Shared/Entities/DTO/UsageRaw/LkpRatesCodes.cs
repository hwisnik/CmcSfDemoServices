using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.DTO.UsageRaw
{
    public class LkpRateCodes
    {
        public Guid ProgramGuid { get; set; }
        public string RateCode { get; set; }
        public string RateDescription { get; set; }
        public string BaseRate { get; set; }
        public string CAPTier { get; set; }
    }
}
