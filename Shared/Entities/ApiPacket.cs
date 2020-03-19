using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shared.Entities
{
    public class ApiPacket
    {
        public string CallerIdentity { get; set; }
        public Guid CorrelationId { get; set; }
        public string RequestUri { get; set; }
        public string RequestBody { get; set; }
        public string RequestHeaders { get; set; }
        public long RequestLength { get; set; }
        public DateTime RequestTimestamp { get; set; }
        public string Response { get; set; }
        public string ResponseHeaders { get; set; }
        public long ResponseLength { get; set; }
        public DateTime ResponseTimestamp { get; set; }
        public double Duration { get; set; }
        public string Verb { get; set; }
        public int StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public int ReplayId { get; set; }
    }
}