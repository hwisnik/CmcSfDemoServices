using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Shared.Commands;

namespace DataAccess.Repositories.Interfaces
{
    public interface ISecurityRepository
    {
        IDbConnection GetConnection();
        IDbTransaction Transaction { get; set; }
         Task<Guid> InsertSecurityTokenAsync(string userName, Guid ermsSecurityToken, LogCommand logCommand);
        Task<Guid> GetUserGuidByUserNameAsPartOfTransaction(string userName, IDbConnection connection, LogCommand logCommand);
        Task<Guid> GetUserSecurityTokenGuidByUserGuid(Guid userGuid, IDbConnection coconnectionnn, LogCommand logCommand);
        Task<int> UpdateSecurityTokenCreateAndUpdateDatesAsPartOfTransaction(Guid userGuid, IDbConnection connection, LogCommand logCommand);
        Task<int> InsertSecurityTokenAsPartOfTransaction(Guid correlationId, Guid userGuid, string userName, IDbConnection connection, LogCommand logCommand);

    }
}
