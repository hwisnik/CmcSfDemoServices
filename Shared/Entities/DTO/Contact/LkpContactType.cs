using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.DTO.Contact
{
    public class LkpContactType
    {
        public Guid ContactTypeGuid { get; set; }
        public string ContactTypeName { get; set; }
        public string ContactTypeDescription { get; set; }
    }
}
