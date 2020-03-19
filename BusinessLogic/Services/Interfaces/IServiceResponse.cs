using System;

namespace BusinessLogic.Services.Interfaces
{

    //IGenericRepository<TEntity> where TEntity : class
    public interface IServiceResponse<TEntity>
    {
        TEntity Entity { get; set; }
        Exception OperationException { get; set; }
        bool Success { get; set; }
    }
}
