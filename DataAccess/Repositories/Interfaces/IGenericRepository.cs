using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Get(int Id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);

        IDbConnection GetConnection();
    }
}
