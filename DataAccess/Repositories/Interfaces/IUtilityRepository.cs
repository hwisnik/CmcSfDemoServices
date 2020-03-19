using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUtilityRepository
    {
        IDbConnection GetConnection();
        IDbTransaction Transaction { get; set; }
    }
}