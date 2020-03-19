using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public static class ServiceHelper
    {
        public static GenericServiceResponse SetGenericServiceResponseForEntityList<TEntity>( IEnumerable<TEntity> entityList) 
            {
            var response = new GenericServiceResponse
            {
                Entity = entityList
            };

            if (entityList == null)
            {
                response.RestResponseStatus = GenericServiceResponse.RestStatus.Error;
            }
            else if (!entityList.Any())
            {
                response.RestResponseStatus = GenericServiceResponse.RestStatus.Empty;
            }
            else
            {
                response.RestResponseStatus = GenericServiceResponse.RestStatus.Success;
            }
            return response;
        }

        public static GenericServiceResponse SetGenericServiceResponseForEntity<TEntity>(TEntity entity)
        {
            var response = new GenericServiceResponse
            {
                Entity = entity, RestResponseStatus = entity == null ? GenericServiceResponse.RestStatus.Error : GenericServiceResponse.RestStatus.Success
            };

            return response;
        }

        public static GenericServiceResponse SetErrorGenericServiceResponse(Exception ex)
        {
            var response = new GenericServiceResponse
            {
                Success = false,
                OperationException =  ex,
                RestResponseStatus = GenericServiceResponse.RestStatus.Error
            };

            return response;
        }
    }
}
