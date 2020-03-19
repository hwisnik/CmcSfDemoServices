using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Shared.EventPayloads
{
    public class LeadPayload
    {
        public PayloadParent _PayloadHeader { get; set; }
        public LeadObjectPayload _PayloadBody { get; set; }

        public LeadPayload(PayloadParent Payload)
        {
            _PayloadHeader = Payload;
            _PayloadBody = JsonConvert.DeserializeObject<LeadObjectPayload>(Payload.PayloadC);
        }
    }

    public class LeadObjectPayload
    {
        public string MobilePhone { get; set; }
        public string Home_Phone__c { get; set; }
        public string Work_Phone__c { get; set; }
        public string Other_Phone__c { get; set; }
        public string Additional_Phone_Data { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("IsDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("RecordTypeId")]
        public string RecordTypeId { get; set; }

        [JsonProperty("Company")]
        public string Company { get; set; }

        [JsonProperty("Street")]
        public string Street { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("PostalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("Latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("Address")]
        public SFPAddress Address { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("PhotoUrl")]
        public string PhotoUrl { get; set; }

        [JsonProperty("LeadSource")]
        public string LeadSource { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("OwnerId")]
        public string OwnerId { get; set; }

        [JsonProperty("HasOptedOutOfEmail")]
        public bool HasOptedOutOfEmail { get; set; }

        [JsonProperty("IsConverted")]
        public bool IsConverted { get; set; }

        [JsonProperty("ConvertedDate")]
        public DateTimeOffset ConvertedDate { get; set; }

        [JsonProperty("ConvertedAccountId")]
        public string ConvertedAccountId { get; set; }

        [JsonProperty("ConvertedContactId")]
        public string ConvertedContactId { get; set; }

        [JsonProperty("IsUnreadByOwner")]
        public bool IsUnreadByOwner { get; set; }

        [JsonProperty("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [JsonProperty("CreatedById")]
        public string CreatedById { get; set; }

        [JsonProperty("LastModifiedDate")]
        public DateTimeOffset LastModifiedDate { get; set; }

        [JsonProperty("LastModifiedById")]
        public string LastModifiedById { get; set; }

        [JsonProperty("SystemModstamp")]
        public DateTime? SystemModstamp { get; set; }

        [JsonProperty("DoNotCall")]
        public bool DoNotCall { get; set; }

        [JsonProperty("HasOptedOutOfFax")]
        public bool HasOptedOutOfFax { get; set; }

        [JsonProperty("LastViewedDate")]
        public DateTime? LastViewedDate { get; set; }

        [JsonProperty("LastReferencedDate")]
        public DateTime? LastReferencedDate { get; set; }

        [JsonProperty("LastTransferDate")]
        public DateTime? LastTransferDate { get; set; }

        [JsonProperty("Audit_Type__c")]
        public string AuditTypeC { get; set; }

        [JsonProperty("Number_of_Contact_Attempts__c")]
        public int? NumberOfContactAttemptsC { get; set; }

        [JsonProperty("Program__c")]
        public string ProgramC { get; set; }

        [JsonProperty("Sub_Program__c")]
        public string SubProgramC { get; set; }

        [JsonProperty("IsActive__c")]
        public bool? IsActiveC { get; set; }

        [JsonProperty("Utility_Account_Number__c")]
        public string UtilityAccountNumberC { get; set; }

        [JsonProperty("CAP_Status__c")]
        public bool? CapStatusC { get; set; }

        [JsonProperty("CMC_Customer_GUID__c")]
        public Guid? CmcCustomerGuidC { get; set; }

        [JsonProperty("CMC_Customer_ID__c")]
        public int? CmcCustomerIdC { get; set; }

        [JsonProperty("Contact_Type__c")]
        public string ContactTypeC { get; set; }

        [JsonProperty("County__c")]
        public string CountyC { get; set; }

        [JsonProperty("Data_Source__c")]
        public string DataSourceC { get; set; }

        [JsonProperty("Is_Any_Contact_Allowed__c")]
        public bool IsAnyContactAllowedC { get; set; }

        [JsonProperty("Is_Auto_Call_Allowed__c")]
        public bool? IsAutoCallAllowedC { get; set; }

        [JsonProperty("Is_Email_Contact_Allowed__c")]
        public bool? IsEmailContactAllowedC { get; set; }

        [JsonProperty("Is_Primary_Contact__c")]
        public bool? IsPrimaryContactC { get; set; }

        [JsonProperty("Is_SMS_Contact_Allowed__c")]
        public bool? IsSmsContactAllowedC { get; set; }

        [JsonProperty("Is_Voice_Contact_Allowed__c")]
        public bool? IsVoiceContactAllowedC { get; set; }

        [JsonProperty("Landlord_Consent__c")]
        public bool LandlordConsentC { get; set; }

        [JsonProperty("Premise_Address_Type__c")]
        public string PremiseAddressTypeC { get; set; }

        [JsonProperty("Premise_Type__c")]
        public string PremiseTypeC { get; set; }

        [JsonProperty("Premise_is_Owned_by_Primary_Contact__c")]
        public bool? PremiseIsOwnedByPrimaryContactC { get; set; }

        [JsonProperty("Status_Reason__c")]
        public string StatusReasonC { get; set; }

        [JsonProperty("Created_by_Upsert__c")]
        public bool? CreatedByUpsertC { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("Salutation")]
        public string Salutation { get; set; }

        [JsonProperty("MiddleName")]
        public string MiddleName { get; set; }

        [JsonProperty("Suffix")]
        public string Suffix { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        //[JsonProperty("GeocodeAccuracy")]
        //public string GeocodeAccuracy { get; set; }
        

        [JsonProperty("Fax")]
        public string Fax { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Website")]
        public string Website { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Industry")]
        public string Industry { get; set; }

        [JsonProperty("Rating")]
        public string Rating { get; set; }

        [JsonProperty("AnnualRevenue")]
        public string AnnualRevenue { get; set; }

        [JsonProperty("NumberOfEmployees")]
        public string NumberOfEmployees { get; set; }

        //[JsonProperty("Jigsaw")]
        //public string Jigsaw { get; set; }

        [JsonProperty("EmailBouncedReason")]
        public string EmailBouncedReason { get; set; }

        [JsonProperty("EmailBouncedDate")]
        public DateTime? EmailBouncedDate { get; set; }

        [JsonProperty("IndividualId")]
        public string IndividualId { get; set; }

        [JsonProperty("Alternate_Email_1__c")]
        public string AlternateEmail1__C { get; set; }

        [JsonProperty("Alternate_Email_2__c")]
        public string AlternateEmail2__C { get; set; }

        [JsonProperty("Alternate_Email_3__c")]
        public string AlternateEmail3__C { get; set; }

        [JsonProperty("Heating_UsageDays__c")]
        public int? HeatingUsageDaysC { get; set; }

        [JsonProperty("Informal_Name__c")]
        public string InformalNameC { get; set; }

        [JsonProperty("Other_Phone_1__c")]
        public string OtherPhone1__C { get; set; }

        [JsonProperty("Other_Phone_2__c")]
        public string OtherPhone2__C { get; set; }

        [JsonProperty("Other_Phone_3__c")]
        public string OtherPhone3__C { get; set; }

        [JsonProperty("Age__c")]
        public int? AgeC { get; set; }

        [JsonProperty("Average_Heat_Usage__c")]
        public double? AverageHeatUsageC { get; set; }

        [JsonProperty("Last_Service_Date__c")]
        public DateTime? LastServiceDateC { get; set; }

        [JsonProperty("Monthly_Average_Usage__c")]
        public double? MonthlyAverageUsageC { get; set; }

        [JsonProperty("Service_Type__c")]
        public string ServiceTypeC { get; set; }

        [JsonProperty("Landlord__c")]
        public string LandlordC { get; set; }
        

        [JsonProperty("Additional_Email_Data__c")]
        public string AdditionalEmailDataC { get; set; }

        [JsonProperty("Assistant_Phone__c")]
        public string AssistantPhoneC { get; set; }

        [JsonProperty("Audit_Type_Name__c")]
        public string AuditTypeNameC { get; set; }

        [JsonProperty("UsageDays__c")]
        public string UsageDaysC { get; set; }

        [JsonProperty("Reserved_Date__c")]
        public DateTime? ReservedDateC { get; set; }

        [JsonProperty("Next_Available_Call_Date__c")]
        public DateTime? NextAvailableCallDateC { get; set; }
    }

    public partial class SFPAddress
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("geocodeAccuracy")]
        public string GeocodeAccuracy { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }
    }

    public partial class Attributes
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

}
