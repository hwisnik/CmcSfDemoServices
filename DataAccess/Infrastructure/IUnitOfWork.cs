using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void CommitChanges();
        void DropChanges();
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity:class;

 //       IGenericRepository<ErmsUserRepository> Repo {get;}
//        IErmsUserRepository ErmsUserRepository { get; }
        void Complete();
    }
}
