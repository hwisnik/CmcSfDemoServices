using System;

namespace Shared.Entities.Usage_Entities
{
    public class UsageRawPeco
    {
        public Guid AccountGuid { get; set; }
        public DateTime? ReadDate { get; set; }
        public decimal? Usage { get; set; }
        public string ReadingType { get; set; }
        public string Rate { get; set; }
        public string RateStatus { get; set; }
        public decimal? Revenue { get; set; }
        public int? NumberOfDays { get; set; }
    }

}
