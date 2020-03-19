using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Contact;
using Shared.Entities._SearchCriteria.Client;
using Shared.EventPayloads;

namespace DataAccess.SFInterfaces
{
    public interface IEmailRepository
    {
        Task<Email> GetEmailFromGuid(Guid EmailGuid, LogCommand logCommand);
        Task<IEnumerable<Email>> GetEmail(SearchEmail _searchEmail, LogCommand logCommand);

        //Update
        Task<int> UpsertEmail (string Email, string FieldName, Guid ContactGuid, LogCommand logCommand);
    }
}
