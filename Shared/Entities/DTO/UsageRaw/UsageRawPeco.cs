using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.DTO.UsageRaw
{
    public class UsageRawPeco
    {
        public Guid CustomerGuid { get; set; }
        public DateTime? ReadDate { get; set; }
        public decimal? Usage { get; set; }
        public string ReadingType { get; set; }
        public string Rate { get; set; }
        public string RateStatus { get; set; }
        public decimal? Revenue { get; set; }
        public int? NumberOfDays { get; set; }
    }
}
