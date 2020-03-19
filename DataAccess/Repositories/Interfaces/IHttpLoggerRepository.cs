using DataAccess.Repositories.Interfaces;
using Shared.Entities;
using System;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IHttpLoggerRepository : IGenericRepository<ApiPacket>
    {
        Task<int> InsertApiPacket(bool IsLoggingEnabled, string CallerIdentity, Guid CorrelationId, string RequestUri, string RequestBody, string RequestHeaders, long RequestLength,
                DateTime RequestTimestamp, string Response, string ResponseHeaders, long ResponseLength, double Duration, string Verb,int StatusCode, string ReasonPhrase);
        Task<int> InsertApiPacketAsync(bool IsLoggingEnabled, ApiPacket apiPacket, bool logThisError = false);
        int InsertApiPacket(bool IsLoggingEnabled, ApiPacket apiPacket, bool logThisError = false);
    }
}
