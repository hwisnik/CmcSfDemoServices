using System.Collections.Generic;
using Shared.Entities.DTO.Contact;

namespace Shared.Entities.SFDb.Lead
{
    public class SFDetailedContact 
    {
        public Contact ContactData { get;set; }
        public PhoneNumbers ContactPhones { get; set; }
        public Email ContactEmails { get; set; }
    }
}
