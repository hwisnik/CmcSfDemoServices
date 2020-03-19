using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork<TUnitOfWork> : IUnitOfWork where TUnitOfWork : class
    {
        private readonly IGenericRepository<TUnitOfWork> _genericRepository;
        public IDbTransaction Transaction { get; }

        public UnitOfWork(IGenericRepository<TUnitOfWork> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        //public IDbTransaction BeginTransaction(IGenericRepository<TUnitOfWork> repository)
        //{
        //    return repository.GetConnection().BeginTransaction();
        //    //return _genericRepository.GetConnection().BeginTransaction();
        //}

        public IDbTransaction BeginTransaction<TUnitOfWork1>(IGenericRepository<TUnitOfWork1> repository) where TUnitOfWork1 : class
        {
            return repository.GetConnection().BeginTransaction();
        }


        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }



        void IUnitOfWork.Complete()
        {
            throw new NotImplementedException();
        }



        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls



        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        public void CommitChanges()
        {
            throw new NotImplementedException();
        }

        public void DropChanges()
        {
            throw new NotImplementedException();
        }









        #endregion
    }
}
