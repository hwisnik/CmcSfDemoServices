using System;

namespace Shared.StoredProcedureParameters
{
    public class IntakeMasterServiceParams
    {
        public Guid SessionGuid { get; set; }
        public Guid? IntakeMasterServiceGuid { get; set; }
        public Guid? IntakeMasterGuid { get; set; }
        public Guid? EntityGuid { get; set; }
        public string ReportYear { get; set; }
        public string CustomerName { get; set; }
        public char? PremiseId { get; set; }
        public string CustomerBillAccountId { get; set; }
        public string ZipCode { get; set; }
        public bool? IsShowOnlyUnassignedServices { get; set; }
        public int? TopN { get; set; }
        public Guid? ServiceTypeStatusCodeGuid { get; set; }
        public Guid? ServiceTypeGuid { get; set; }
        public Guid? ServiceCodeGuid { get; set; }
        public string CompletedOnCriteria { get; set; }
        public DateTime? CompletedOnDateStart { get; set; }
        public DateTime? CompletedOnDateStop { get; set; }
        public string IssuedDateCriteria { get; set; }
        public DateTime? IssuedDateDateStart { get; set; }
        public DateTime? IssuedDateDateStop { get; set; }
        public string ScheduledDateCriteria { get; set; }
        public DateTime? ScheduledDateDateStart { get; set; }
        public DateTime? ScheduledDateDateStop { get; set; }
        
    }
}
