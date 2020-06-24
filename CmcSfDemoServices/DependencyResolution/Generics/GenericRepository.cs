#pragma warning disable 1591
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using StructureMap.Pipeline;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CmcSfDemoServices.DependencyResolution.Generics
{
    //public static class GenericRepositoryBuilder
    //{
        //public static IGenericRepository<TEntity> Build<TEntity>() where TEntity : class
        //{
        //    return new GenericRepository<TEntity>();
        //}
    //}


    //public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    //{
    ////    public void Add(TEntity entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Delete(TEntity entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public TEntity Get(int Id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<TEntity> GetAll()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IDbConnection GetConnection()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Update(TEntity entity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class GenericRepositoryInstance<TEntity> : LambdaInstance<IGenericRepository<TEntity>> where TEntity : class
    //{
    //    public GenericRepositoryInstance() : base(() => GenericRepositoryBuilder.Build<TEntity>())
    //    {
    //    }

    //    // This is purely to make the diagnostic views prettier
    //    public override string Description
    //    {
    //        get
    //        {
    //            return string.Format("RepositoryBuilder.Build<{0}>()", typeof(TEntity).Name);
    //        }
    //    }
    //}
}