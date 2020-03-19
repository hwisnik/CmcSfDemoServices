using System;
using System.Collections.Generic;
using System.Text;
using Shared.Entities.InputObjects.Address;

namespace Shared.Entities.CDCClient
{
    public class CdcAuditEntity
    {
        public Guid AuditLogGuid { get; set; }
        public string ObjectType { get; set; }
        public string EventType { get; set; }
        public string CreatedById { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string RecordId { get; set; }

    }
}
