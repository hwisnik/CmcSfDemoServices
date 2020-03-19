using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.DTO.Contact
{
    public class MailAddress
    {
        public Guid MailAddressGuid { get; set; }
        public Guid ContactGuid { get; set; }
        public string SFFieldName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
