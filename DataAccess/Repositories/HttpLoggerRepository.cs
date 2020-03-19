using Dapper;
using Shared.Entities;
using DataAccess.Infrastructure;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class HttpLoggerRepository : GenericRepository<ApiPacket>, IHttpLoggerRepository
    {
        readonly IConnectionFactory _connectionFactory;
        public HttpLoggerRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Dapper is handling mapping of POCO ApiPacket to insert parameters
        /// </summary>
        /// <param name="isLoggingEnabled"></param>
        /// <param name="logThisError"></param>
        /// <param name="apiPacket"></param>
        /// <returns></returns>
        public async Task<int> InsertApiPacketAsync(bool isLoggingEnabled, ApiPacket apiPacket, bool logThisError = false)
        {
            if (!logThisError)
            {
                if (isLoggingEnabled == false || apiPacket.Response == null) return 0;
            }

            const string insertQuery = @"INSERT INTO [EnterpriseLogging].[Ent].[HttpLog] ([CallerIdentity], [CorrelationId], [RequestUri], [RequestBody], [RequestHeaders], [RequestLength], [RequestTimestamp], [Response], [ResponseHeaders], " +
                                       "[ResponseLength], [ResponseDurationInSeconds], [Verb], [StatusCode], [ReasonPhrase], [ReplayId]) VALUES (@CallerIdentity, @CorrelationId, @RequestUri, @RequestBody, @RequestHeaders, @RequestLength, " +
                                       "@RequestTimestamp, @Response, @ResponseHeaders, @ResponseLength, @Duration, @Verb, @StatusCode, @ReasonPhrase, @ReplayId)";
            using (var connection = _connectionFactory.GetConnection)
            {
                var result = await connection.ExecuteAsync(insertQuery, apiPacket);
                connection.Close();
                return result;
            }

        }


        public int InsertApiPacket(bool isLoggingEnabled, ApiPacket apiPacket, bool logThisError = false)
        {
            if (!logThisError)
            {
                if (isLoggingEnabled == false || apiPacket.Response == null) return 0;
            }

            if (isLoggingEnabled == false || apiPacket.Response == null) return 0;
            const string insertQuery = @"INSERT INTO [EnterpriseLogging].[Ent].[HttpLog] ([CallerIdentity], [CorrelationId], [RequestUri], [RequestBody], [RequestHeaders], [RequestLength], [RequestTimestamp], [Response], [ResponseHeaders], " +
                                       "[ResponseLength], [ResponseDurationInSeconds], [Verb], [StatusCode], [ReasonPhrase]) VALUES (@CallerIdentity, @CorrelationId, @RequestUri, @RequestBody, @RequestHeaders, @RequestLength, " +
                                       "@RequestTimestamp, @Response, @ResponseHeaders, @ResponseLength, @Duration, @Verb, @StatusCode, @ReasonPhrase)";
            using (var connection = _connectionFactory.GetConnection)
            {
                var result = connection.Execute(insertQuery, apiPacket);
                connection.Close();
                return result;
            }

        }

        /// <summary>
        /// Sample showing manual creation of insert query and map to POCO object
        /// </summary>
        /// <param name="IsLoggingEnabled"></param>
        /// <param name="CallerIdentity"></param>
        /// <param name="CorrelationId"></param>
        /// <param name="RequestUri"></param>
        /// <param name="RequestBody"></param>
        /// <param name="RequestHeaders"></param>
        /// <param name="RequestLength"></param>
        /// <param name="RequestTimestamp"></param>
        /// <param name="Response"></param>
        /// <param name="ResponseHeaders"></param>
        /// <param name="ResponseLength"></param>
        /// <param name="Duration"></param>
        /// <param name="Verb"></param>
        /// <param name="StatusCode"></param>
        /// <param name="ReasonPhrase"></param>
        /// <returns></returns>
        public async Task<int> InsertApiPacket(bool IsLoggingEnabled, string CallerIdentity, Guid CorrelationId, string RequestUri, string RequestBody, string RequestHeaders, long RequestLength,
            DateTime RequestTimestamp, string Response, string ResponseHeaders, long ResponseLength, double Duration, string Verb, int StatusCode, string ReasonPhrase)
        {

            if (IsLoggingEnabled == false || string.IsNullOrEmpty(Response)) return 0;
            var insertQuery = @"INSERT INTO [EnterpriseLogging].[Ent].[HttpLog] ([CallerIdentity], [CorrelationId], [RequestUri], [RequestBody], [RequestHeaders], [RequestLength], [RequestTimestamp], [Response], [ResponseHeaders], " +
                           "[ResponseLength], [ResponseDurationInSeconds], [Verb], [StatusCode], [ReasonPhrase]) VALUES (@CallerIdentity, @CorrelationId, @RequestUri, @RequestBody, @RequestHeaders, @RequestLength, " +
                            "@RequestTimestamp, @Response, @ResponseHeaders, @ResponseLength, @Duration, @Verb, @StatusCode, @ReasonPhrase)";


            using (var connection = _connectionFactory.GetConnection)
            //using (var connection = new SqlConnection(connectionstring))
            {
                //connection.Open();
                //await connection.OpenAsync();
                var result = await connection.ExecuteAsync(insertQuery, new
                {
                    CallerIdentity,
                    CorrelationId,
                    RequestUri,
                    RequestBody,
                    RequestHeaders,
                    RequestLength,
                    RequestTimestamp,
                    Response,
                    ResponseHeaders,
                    ResponseLength,
                    Duration,
                    Verb,
                    StatusCode,
                    ReasonPhrase
                });
                connection.Close();
                return result;
            }
        }


    }
}
