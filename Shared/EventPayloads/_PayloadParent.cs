using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Shared.EventPayloads
{
    public class PayloadParent
    {
        [JsonProperty("CMC_Customer_GUID__c")] 
        public Guid CmcCustomerGuidC { get; set; }

        [JsonProperty("Bill_Account_Number__c")]
        public string BillAccountNumberC { get; set; }

        [JsonProperty("Salesforce_Sub_program_Id__c")]
        public string SalesforceSubProgramIDC { get; set; }

        [JsonProperty("Salesforce_Program_Id__c")]
        public string SalesforceProgramIDC { get; set; }

        [JsonProperty("CreatedById")] 
        public string CreatedById { get; set; }

        [JsonProperty("Payload__c")] 
        public string PayloadC { get; set; }

        [JsonProperty("RecordId__c")] 
        public string RecordIdC { get; set; }

        [JsonProperty("CreatedDate")] 
        public DateTime CreatedDate { get; set; }

        [JsonProperty("Event_Type__c")] 
        public string EventTypeC { get; set; }

        [JsonProperty("Object_Name__c")] 
        public string ObjectNameC { get; set; }

        [JsonProperty("ReplayID")]
        public int ReplayId { get; set; }
    }
}
