using System;

using Newtonsoft.Json;

namespace Shared.Entities.InputObjects.Lead
{
    public class PrioLeadInput
    {
        public string SfProgramId { get; set; }
        public string SfSubProgramId { get; set; }
        public double PrevAppointmentLat { get; set; }
        public double PrevAppointmentLong { get; set; }
        public double NextAppointmentLat { get; set; }
        public double NextAppointmentLong { get; set; }
        public Guid WorkType { get; set; }
        public string AppointmentDateTime { get; set; }
        [JsonIgnore]
        public DateTime AppointmentDateTimeFormatted { get; set; }
        public int? NumberOfLeadsToReturn { get; set; }
        public int? NumberOfLeadsToQuery { get; set; }
        public string[] PostalCodes { get; set; }
        [JsonIgnore]
        public string ZipCodesForQuery { get; set; }

    }
}
