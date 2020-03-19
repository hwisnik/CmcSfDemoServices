using System;

namespace Shared.Entities.SFDb.Lead
{
    public class SfPrioritizedLead
    {
        public Guid CustomerGuid { get; set; }
        public Guid ContactGuid { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public double AverageUsage { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double AerialDistance { get; set; }
        public string Address { get; set; }
        public string SfLeadId { get; set; }
        public string CapTier { get; set; }
        public double HeatingAverageUsage { get; set; }
        public Guid LeadStatusGuid { get; set; }
        public string LeadStatusReason { get; set; }
        public DateTime LeadStatusChangeDate { get; set; }
        public DateTime NextAvailableCallDate { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public string QualifiedRate { get; set; }
        public string Rate { get; set; }
        public DateTime ReservedDate { get; set; }
        public double Leg0AerialDistance { get; set; }
        public double Leg1AerialDistance { get; set; }

    }
}
