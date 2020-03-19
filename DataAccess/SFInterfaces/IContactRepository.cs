using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Contact;
using Shared.Entities.SFDb.Lead;
using Shared.Entities._SearchCriteria.Client;
using Shared.EventPayloads;

namespace DataAccess.SFInterfaces
{
    public interface IContactRepository
    {
        IDbConnection GetConnection();

        Task<Contact> GetPrimaryContactFromCustomerGuid(Guid CustomerGuid, LogCommand logCommand);
        Task<Contact> GetContactFromSFContactRecordID(string SFContactRecordID, LogCommand logCommand);

        //Phones, email address By ContactGuid
        Task<PhoneNumbers> GetPhoneFromContactGuid(Guid ContactGuid, LogCommand logCommand);
        Task<Email> GetEmailFromContactGuid(Guid ContactGuid, LogCommand logCommand);
        Task<IEnumerable<string>> GetPhoneNumberByContactGuidAsync(Guid contactGuid, LogCommand logCommand, IDbTransaction transConnection);

        //Update (Example: For Primary) 
        Task<int> UpdatePrimaryContact(Contact contact, LogCommand logCommand);
        Task<int> InsertContact(Contact contact, LogCommand logCommand);
        Task<int> UpdateContact(Contact contact, LogCommand logCommand);

        //Phone Email
        Task<int> InsertPhone(PhoneNumbers phone, LogCommand logCommand);
        Task<int> UpdatePhone(PhoneNumbers phone, LogCommand logCommand);
        Task<int> InsertEmail(Email email, LogCommand logCommand);
        Task<int> UpdateEmail(Email email, LogCommand logCommand);


    }
}
