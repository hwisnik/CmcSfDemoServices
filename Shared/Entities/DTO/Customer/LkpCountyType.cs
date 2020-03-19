using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.DTO.Customer
{
    public class LkpCountyType
    {
        public Guid CountyTypeGuid { get; set; }
        public string CountyTypeName { get; set; }
        public string CountyTypeDescription { get; set; }
    }
}
