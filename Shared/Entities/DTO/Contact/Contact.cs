using System;
using System.Collections.Generic;
using System.Text;
using Shared.EventPayloads;

namespace Shared.Entities.DTO.Contact
{
    public class Contact
    {
        public Guid ContactGuid { get; set; }
        public Guid CustomerGuid { get; set; }
        public string SFContactId { get; set; }
        public Guid ContactTypeGuid { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string InformalName { get; set; }
        public string Salutation { get; set; }
        public string Suffix { get; set; }
        public string Title { get; set; }

        public bool? IsAnyContactAllowed { get; set; }
        public bool? IsVoiceContactAllowed { get; set; }
        public bool? IsSmsContactAllowed { get; set; }
        public bool? IsAutoCallAllowed { get; set; }
        public bool? IsEmailContactAllowed { get; set; }
        
        public string ContactTypeName { get; set; }

        public Contact()
        {

        }
        // For Primary Contact
        public Contact(LeadPayload _LeadPayload, Guid contactGuid, Guid contactTypeGuid)
        {
            ContactGuid = contactGuid; 
            CustomerGuid = _LeadPayload._PayloadHeader.CmcCustomerGuidC;
            ContactTypeGuid = contactTypeGuid; 

            ContactTypeName = _LeadPayload._PayloadBody.ContactTypeC;
            FirstName = _LeadPayload._PayloadBody.FirstName;
            MiddleName= _LeadPayload._PayloadBody.MiddleName;
            LastName = _LeadPayload._PayloadBody.LastName;
            FullName = _LeadPayload._PayloadBody.Name;
            InformalName = _LeadPayload._PayloadBody.InformalNameC;
            Salutation = _LeadPayload._PayloadBody.Salutation;
            Suffix = _LeadPayload._PayloadBody.Suffix;
            Title = _LeadPayload._PayloadBody.Title;
            IsAnyContactAllowed = _LeadPayload._PayloadBody.IsAnyContactAllowedC;
            IsVoiceContactAllowed = _LeadPayload._PayloadBody.IsVoiceContactAllowedC;
            IsSmsContactAllowed = _LeadPayload._PayloadBody.IsSmsContactAllowedC;
            IsAutoCallAllowed = _LeadPayload._PayloadBody.IsAutoCallAllowedC;
            IsEmailContactAllowed = _LeadPayload._PayloadBody.IsEmailContactAllowedC;
        }

        // For Primary Contact
        public Contact(LeadPayload _LeadPayload, Guid contactTypeGuid)
        {
            CustomerGuid = _LeadPayload._PayloadHeader.CmcCustomerGuidC;
            ContactTypeGuid = contactTypeGuid;
            
            ContactTypeName = _LeadPayload._PayloadBody.ContactTypeC;
            FirstName = _LeadPayload._PayloadBody.FirstName;
            MiddleName= _LeadPayload._PayloadBody.MiddleName;
            LastName = _LeadPayload._PayloadBody.LastName;
            FullName = _LeadPayload._PayloadBody.Name;
            InformalName = _LeadPayload._PayloadBody.InformalNameC;
            Salutation = _LeadPayload._PayloadBody.Salutation;
            Suffix = _LeadPayload._PayloadBody.Suffix;
            Title = _LeadPayload._PayloadBody.Title;
            IsAnyContactAllowed = _LeadPayload._PayloadBody.IsAnyContactAllowedC;
            IsVoiceContactAllowed = _LeadPayload._PayloadBody.IsVoiceContactAllowedC;
            IsSmsContactAllowed = _LeadPayload._PayloadBody.IsSmsContactAllowedC;
            IsAutoCallAllowed = _LeadPayload._PayloadBody.IsAutoCallAllowedC;
            IsEmailContactAllowed = _LeadPayload._PayloadBody.IsEmailContactAllowedC;
        }

    }
}
