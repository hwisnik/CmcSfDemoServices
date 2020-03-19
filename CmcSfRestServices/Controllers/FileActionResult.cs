using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

#pragma warning disable 1591

namespace CmcSfRestServices.Controllers
{
    public class FileActionResult : IHttpActionResult
    {
        private readonly string _filePath;
        private readonly string _contentType;

        public FileActionResult(string filePath, string contentType = null)
        {
            _filePath = filePath;
           _contentType = contentType;
        }
        /// <inheritdoc />
        /// <summary>
        /// Streams File contents if File exists, else returns HttpStatusCode.NotFound)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>StreamContent(File) or HttpStatusCode.NotFound</returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            if (!File.Exists(_filePath))
            {
                return Task.Run(() => new HttpResponseMessage(HttpStatusCode.NotFound), cancellationToken);
            }

            return Task.Run(() =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(File.OpenRead(_filePath))
                };

                var contentType = _contentType ?? MimeMapping.GetMimeMapping(Path.GetExtension(_filePath));
                var fileNameNoPath = string.Empty;
                var idx = _filePath.LastIndexOf("\\", StringComparison.CurrentCulture);
                if (idx != -1)
                {
                    fileNameNoPath = (_filePath.Substring(idx + 1));
                }

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = fileNameNoPath
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                return response;
            }, cancellationToken);
        }
    }
}