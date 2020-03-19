using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Shared.EventPayloads
{
    public class AddressPayload
    {
        public PayloadParent _PayloadHeader { get; set; }
        public AddressObjectPayload _PayloadBody { get; set; }

        public AddressPayload(PayloadParent Payload)
        {
            _PayloadHeader = Payload;
            _PayloadBody = JsonConvert.DeserializeObject<AddressObjectPayload>(Payload.PayloadC);
        }
    }

    public class AddressObjectPayload
    {
        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("OwnerId")]
        public string OwnerId { get; set; }

        [JsonProperty("IsDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("CreatedDate")]
        public DateTimeOffset CreatedDate { get; set; }

        [JsonProperty("CreatedById")]
        public string CreatedById { get; set; }

        [JsonProperty("LastModifiedDate")]
        public DateTimeOffset LastModifiedDate { get; set; }

        [JsonProperty("LastModifiedById")]
        public string LastModifiedById { get; set; }

        [JsonProperty("SystemModstamp")]
        public DateTimeOffset SystemModstamp { get; set; }

        [JsonProperty("LastViewedDate")]
        public DateTimeOffset LastViewedDate { get; set; }

        [JsonProperty("LastReferencedDate")]
        public DateTimeOffset LastReferencedDate { get; set; }

        [JsonProperty("Account__c")]
        public string AccountC { get; set; }

        [JsonProperty("Address_Type__c")]
        public string AddressTypeC { get; set; }

        [JsonProperty("City__c")]
        public string CityC { get; set; }

        [JsonProperty("Country__c")]
        public string CountryC { get; set; }

        [JsonProperty("Latitude_Longitude__Latitude__s")]
        public double? LatitudeLongitudeLatitudeS { get; set; }

        [JsonProperty("Latitude_Longitude__Longitude__s")]
        public double? LatitudeLongitudeLongitudeS { get; set; }

        [JsonProperty("Lead__c")]
        public string LeadC { get; set; }

        [JsonProperty("Premise__c")]
        public string PremiseC { get; set; }

        [JsonProperty("State__c")]
        public string StateC { get; set; }

        [JsonProperty("Street__c")]
        public string StreetC { get; set; }

        [JsonProperty("Zip_Code__c")]
        public string ZipCodeC { get; set; }

        [JsonProperty("County__c")]
        public string CountyC { get; set; }

        [JsonProperty("Address_Source__c")]
        public string Address_SourceC { get; set; }

        [JsonProperty("Bill_Account_Number__c")]
        public string BillAccountNumberC { get; set; }

        [JsonProperty("CMC_Customer_GUID__c")]
        public Guid CmcCustomerGuidC { get; set; }

        [JsonProperty("CMC_Program_GUID__c")]
        public Guid CmcProgramGuidC { get; set; }

        [JsonProperty("CMC_Sub_Program_GUID__c")]
        public Guid CmcSubProgramGuidC { get; set; }

        [JsonProperty("Premise_Type__c")]
        public string PremiseTypeC { get; set; }

        [JsonProperty("Contact__c")]
        public string ContactC { get; set; }

        [JsonProperty("Last_Service_Date__c")]
        public DateTime? LastServiceDateC { get; set; }
    }


    public partial class LatitudeLongitudeC
    {
        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }
    }
}
