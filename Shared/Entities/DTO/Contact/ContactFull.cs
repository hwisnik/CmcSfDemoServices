using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.DTO.Contact
{
    public class ContactFull : Contact
    {
        public List<PhoneNumbers> ContactPhones { get; set; }
        public List<Email> ContactEmails { get;set; }
        public List<MailAddress> ContactMailAddresses { get;set; }
    }
}
