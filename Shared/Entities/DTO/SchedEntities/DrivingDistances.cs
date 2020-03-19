using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shared.Entities.DTO.SchedEntities
{
    public class DrivingDistances
    {
        public Guid CustomerGuid{ get; set; }
        public string SfLeadId { get; set; }
        public string Fullname { get; set; }
        //public string PhoneNumber { get; set; }
        public string AppointmentAddress { get; set; }
        public string Leg0StartAddress { get; set; }
        //public string Leg0EndAddress { get; set; }
        //public string Leg1StartAddress { get; set; }
        public string Leg1EndAddress { get; set; }
        [JsonIgnore]
        public double AverageUsage { get; set; }
        public int Leg0Duration { get; set; }
        public double Leg0Distance { get; set; }
        public int Leg1Duration { get; set; }
        public double Leg1Distance { get; set; }
        [JsonIgnore]
        public double TotalAerialDistance { get; set; }
        [JsonIgnore]
        public double TotalDrivingDistance { get; set; }
        [JsonIgnore]
        public int TotalDuration { get; set; }

    }
}
