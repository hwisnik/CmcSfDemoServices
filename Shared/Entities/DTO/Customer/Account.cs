using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.DTO.Customer
{
    public class Account
    {
        public Guid AccountGuid { get; set; }
        public Guid CustomerGuid { get; set; }
        public string SFAccountID { get; set; }
        public Guid AccountStatusGuid { get; set; }
        public string JobId { get; set; }
    }
}
