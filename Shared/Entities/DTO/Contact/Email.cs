using System;
using System.Collections.Generic;
using System.Text;
using Shared.EventPayloads;

namespace Shared.Entities.DTO.Contact
{
    public class Email
    {
        public Guid EmailGuid { get; set; }
        public Guid ContactGuid { get; set; }
        public string Email_Address { get; set; }
        public string Alternate_Email_1__c { get; set; }
        public string Alternate_Email_2__c { get; set; }
        public string Alternate_Email_3__c { get; set; }
        public string Additional_Email_Data { get; set; }

        public Email ()
        {

        }
        public Email(LeadPayload _LeadPayload, Guid contactGuid)
        {
            ContactGuid = contactGuid;
            Email_Address = _LeadPayload._PayloadBody.Email;
            Alternate_Email_1__c = _LeadPayload._PayloadBody.AlternateEmail1__C;
            Alternate_Email_2__c = _LeadPayload._PayloadBody.AlternateEmail2__C;
            Alternate_Email_3__c = _LeadPayload._PayloadBody.AlternateEmail3__C;
        }
    }



}
