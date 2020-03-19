using System;
using Shared.EventPayloads;

namespace Shared.Entities.DTO.Customer
{
    public class Lead
    {
        public Guid LeadGuid { get; set; }
        public Guid CustomerGuid { get; set; }
        public string SFLeadID { get; set; }
        public Guid LeadStatusGuid { get; set; }
        public string LeadStatusName { get; set; }
        public string LeadStatusReason { get; set; }
        public string LeadSource { get; set; }
        public Guid QualifiedAuditTypeGuid { get; set; }
        public string QualifiedRate { get; set; }
        public string AuditTypeName { get; set; }
        public DateTime? ReservedDate { get; set; }
        public DateTime? LeadStatusChangeDate { get; set; }
        public DateTime? NextAvailableCallDate { get; set; }

        public Lead()
        {

        }

        public Lead(LeadPayload leadPayload, Guid leadStatusGuid, Guid CmcAuditType)
        {
            CustomerGuid = leadPayload._PayloadHeader.CmcCustomerGuidC;
            SFLeadID = leadPayload._PayloadHeader.RecordIdC;
            
            LeadStatusGuid = leadStatusGuid;
            QualifiedAuditTypeGuid = CmcAuditType;

            LeadStatusName = leadPayload._PayloadBody.Status;
            LeadSource  = leadPayload._PayloadBody.LeadSource;
            LeadStatusReason = leadPayload._PayloadBody.StatusReasonC;
            AuditTypeName   = leadPayload._PayloadBody.AuditTypeC;
            //ReservedDate is a non nullable column in the database
            ReservedDate = leadPayload._PayloadBody.ReservedDateC ?? Convert.ToDateTime("1900-01-01");
            NextAvailableCallDate = leadPayload._PayloadBody.NextAvailableCallDateC ?? Convert.ToDateTime("1900-01-01");
            //TODO Not there yet? 
            QualifiedRate = null; 
        }


    }
}
