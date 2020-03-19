using System;
using System.Collections.Generic;
using System.Text;
using Shared.EventPayloads;

namespace Shared.Entities.DTO.Customer
{
    public class Customer
    {
        public Guid CustomerGuid { get; set; }
        public int CMCCustomerID { get; set; }
        public Guid ProgramGuid { get; set; }
        public Guid SubProgramGuid { get; set; }
        public string BillingAccountNumber { get; set; }
        public Guid CustomerAccountTypeGuid { get; set; }

        public string ProgramName { get; set; }
        public string SubProgramName { get; set; }

        public Customer ()
        {

        }
        public Customer(
            LeadPayload _LeadPayload, 
            Guid programGuid, 
            Guid subprogramGuid)
        {
            CustomerGuid = _LeadPayload._PayloadHeader.CmcCustomerGuidC;
            ProgramGuid = programGuid;
            SubProgramGuid = subprogramGuid;
            BillingAccountNumber = _LeadPayload._PayloadHeader.BillAccountNumberC;
            //CustomerAccountTypeGuid = customerAccountTypeGuid;
        }
        public Customer(
            LeadPayload _LeadPayload, 
            Guid programGuid, 
            Guid subprogramGuid, 
            Guid customerAccountTypeGuid)
        {
            CustomerGuid = _LeadPayload._PayloadHeader.CmcCustomerGuidC;
            ProgramGuid = programGuid;
            SubProgramGuid = subprogramGuid;
            BillingAccountNumber = _LeadPayload._PayloadHeader.BillAccountNumberC;
            CustomerAccountTypeGuid = customerAccountTypeGuid;
            //CustomerAccountTypeGuid = customerAccountTypeGuid;
        }
    }
}
