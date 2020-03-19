using System;
using System.Collections.Generic;
using System.Text;
using Shared.EventPayloads;

namespace Shared.Entities.DTO.Customer
{
    public class Address
    {
        public Guid AddressGuid { get; set; }
        public Guid CustomerGuid { get; set; }
        public string SFAddressRecordID { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string AddressSource { get; set; }
        public DateTime? LastServiceDate { get; set; }

        public Guid CountyGuid { get; set; }
        public string CountyDescription { get; set; }

        public Guid AddressTypeGuid { get; set; }
        public string AddressTypeName { get; set; }
        
        public Guid OwnershipGuid { get; set; }
        public string OwnershipType { get; set; }
        

        public Address()
        {

        }
        

        public Address(AddressPayload payloadParent, Guid addressTypeGuid, Guid countyGuid, Guid ownershipGuid)
        {
            CustomerGuid = payloadParent._PayloadHeader.CmcCustomerGuidC; 
            SFAddressRecordID = payloadParent._PayloadHeader.RecordIdC;
            CountyGuid = countyGuid;
            AddressTypeGuid = addressTypeGuid;
            OwnershipGuid = ownershipGuid; 

            StreetAddress1 = payloadParent._PayloadBody.StreetC;
            Country = payloadParent._PayloadBody.CountryC;
            City = payloadParent._PayloadBody.CityC;
            State = payloadParent._PayloadBody.StateC;
            Zip = payloadParent._PayloadBody.ZipCodeC;
            Latitude = payloadParent._PayloadBody.LatitudeLongitudeLatitudeS;
            Longitude = payloadParent._PayloadBody.LatitudeLongitudeLongitudeS;
            LastServiceDate = payloadParent._PayloadBody.LastServiceDateC;
            AddressSource = payloadParent._PayloadBody.Address_SourceC; 
        }
    }
}
