using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Shared.EventPayloads;

namespace Shared.Entities.DTO.Contact
{
    public class PhoneNumbers
    {

        public Guid? PhoneNumberGuid { get; set; }
        public Guid? ContactGuid { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Home_Phone__c { get; set; }
        public string Work_Phone__c { get; set; }
        public string Other_Phone__c { get; set; }
        public string Additional_Phone_Data { get; set; }

        public PhoneNumbers()
        {

        }

        public PhoneNumbers(LeadPayload _LeadPayload, Guid contactGuid)
        {
            ContactGuid = contactGuid;
            Phone = _LeadPayload._PayloadBody.Phone;
            MobilePhone = _LeadPayload._PayloadBody.MobilePhone;
            Home_Phone__c = _LeadPayload._PayloadBody.Home_Phone__c;
            Work_Phone__c = _LeadPayload._PayloadBody.Work_Phone__c;
            Other_Phone__c = _LeadPayload._PayloadBody.Other_Phone__c;
            Additional_Phone_Data = _LeadPayload._PayloadBody.Additional_Phone_Data;
    }
    }
}
