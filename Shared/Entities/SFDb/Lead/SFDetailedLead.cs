using System.Collections.Generic;
using Shared.Entities.DTO.Customer;
using Shared.Entities.DTO.ERMS;
using Shared.Entities.SFDb.Lead;


namespace Shared.Entities.SFDb.Lead
{
    public class SFDetailedLead 
    {
        public DTO.Customer.Lead LeadData { get; set; }

        public Customer CustomerData { get; set; }

        public CapQualified CapStatus { get; set; }

        public SFDetailedContact PrimaryContactData {get; set; }

        public IEnumerable<SFDetailedPremise> PremiseData { get; set; }

        public IEnumerable<Usage> UsageData { get; set; }

        public ERMSCustomer ERMSData { get; set; }

    }

    public class SFDetailedPremise
    {
        public SFPremise PremiseData { get;set; } = new SFPremise();
        public IEnumerable<Address> AddressData { get; set; }
    }

    public class SFPremise
    {

    }
}
