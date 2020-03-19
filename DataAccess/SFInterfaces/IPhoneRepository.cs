using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Contact;
using Shared.Entities.SFDb.Lead;
using Shared.Entities._SearchCriteria.Client;
using Shared.EventPayloads;

namespace DataAccess.SFInterfaces
{
    public interface IPhoneRepository
    {
        Task<PhoneNumbers> GetPhoneFromGuid(Guid PhoneGuid, LogCommand logCommand);
        Task<IEnumerable<PhoneNumbers>> GetPhone(SearchPhone _searchPhone, LogCommand logCommand);
        
        //Update 17813AB5-5C5F-41DA-A230-633A0CB2FC60
        Task<int> UpsertPhone(string PhoneNumber, string FieldName, Guid ContactGuid, LogCommand logCommand);
    }
}
