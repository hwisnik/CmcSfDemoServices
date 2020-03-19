using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities
{
    public class ServiceTypeStatusCodeEntity
    {
        public Guid ServiceTypeStatusCodeGuid { get; set; }
        public int OrgId { get; set; }
        public Guid ServiceTypeGuid { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceTypeStatusCode { get; set; }
        public string ServiceTypeStatusCodeDescription { get; set; }
        public Guid? ServiceTypeCancelCodeGuid { get; set; }
        public string CompletionDateMode { get; set; }
        public string IssueDateMode { get; set; }
        public string ScheduledDateMode { get; set; }
        public string RequiredIssueDate_DefaultMode { get; set; }
        public string RequiredScheduledDate_DefaultMode { get; set; }
        public string RequiredCompletedDate_DefaultMode { get; set; }
        public bool IsCancelCodeRequired { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public bool IsAllowTechnicianNA { get; set; }
        public bool IsAllowServiceTypeWorkOrders { get; set; }
        public bool IsAllowServiceTypeInspections { get; set; }
        public bool IsAllowServiceTypeAudits { get; set; }
        public bool IsAllowServiceTypeIntakes { get; set; }
        public bool IsAllowServiceTypeQA { get; set; }
        public bool IsAllowServiceTypeOtherExpenses { get; set; }
        public bool IsAllowServiceTypeOverallCustomer { get; set; }
        public bool IsAllowServiceTypePostTreatment { get; set; }
        public bool IsAllowServiceTypeUnknown { get; set; }
        public bool IsRequireOverallProjectCompletionDate { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime UpdatedOn { get; set; }
        //public string UpdatedBy { get; set; }
        //public byte[] RowVersion { get; set; }
    }
}
