using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.DTO.ERMS
{
    public class ERMSCustomer
    {
        public Guid ERMSCustomerGuid { get; set; }
        public Guid CustomerGuid { get; set; }
        public int? ContactAttempts { get; set; }
    }
}
