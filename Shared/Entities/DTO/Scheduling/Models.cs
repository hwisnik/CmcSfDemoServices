using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities.DTO.Scheduling
{
    public class LkpSchedulingType
    {
        public Guid LkpSchedulingTypeGuid { get; set; }
        public string SchedulingTypeName { get; set; }
        public string SchedulingTypeDescription { get; set; }
    }

    public class SimpleScheduling
    {
        public int SSID { get; set; }
        public string SfRecordID { get; set; }
        public Guid ProgramGuid { get; set; }
        public string SalesforceUser { get; set; }
        public string Technician { get; set; }
        public string WorkOrderType { get; set; }
        public bool ScheduleBit { get; set; }
        public DateTime ScheduleDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
    }

    public class   Scheduling
    {
        public Guid SchedulingGuid { get; set; }
        public Guid WorkOrderScoreWeightingGuid { get; set; }
        public Guid UsernameMappingSFtoADGuid { get; set; }
        public Guid LkpSchedulingTypeGuid { get; set; }
        public Guid ProgramGuid { get; set; }
        public Guid SubprogramGuid { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string SalesforceRecordID { get; set; }
    }

    // Just because,,,,,,,,,,,
    public class SFUsernameAndID
    {
        public string SalesForceUserRecordID { get; set; }
        public string UserName { get; set; }
    }

    public class UsernameMappingSFtoAD
    {
        public Guid UsernameMappingSFtoADGuid { get; set; }
        public string SalesForceUserRecordID { get; set; }
        public string ADUserName { get; set; }
        public string SFUsername { get; set; }
    }

    public class LKPWorkOrderScoreMapping
    {
        public Guid WorkOrderScoreWeightingGuid { get; set; }
        public string SalesforceRecordID { get; set; }
        public string WorkOrderTypeName { get; set; }
        public double? Score { get; set; }
        public bool? ProcessBuilderAccount { get; set; }
    }


}
