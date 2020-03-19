using System;

namespace Shared.Entities.SFDb.Lead
{
    public class SFSimpleLeadResults
    {
        public Guid LeadGuid { get; set; }
        public Guid CustomerGuid { get; set; }

        public string BillAccount { get; set; }
        public int? CMCCustomerID { get; set; }

        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string STAddress { get; set; }
        public string County { get; set; }
        public string ZipCode { get; set; }

        public string CAPTier { get; set; }
    }
}
