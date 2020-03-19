using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.InputObjects.Lead
{
    public class InputConvertLeadToAccount
    {
        //Map To account.
        public string SFAccountRecordID { get; set; }
        public Guid LeadGuid { get; set; }
        public Guid ClientAccountStatusGuid { get; set; }
    }
}
