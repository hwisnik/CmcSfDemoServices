using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Shared.EventPayloads
{
    public class ServiceAppointmentPayload
    {
        public PayloadParent _PayloadHeader { get; set; }
        public ServiceAppointmentObjectPayload _PayloadBody { get; set; }

        public ServiceAppointmentPayload(PayloadParent Payload)
        {
            _PayloadHeader = Payload;
            _PayloadBody = JsonConvert.DeserializeObject<ServiceAppointmentObjectPayload>(Payload.PayloadC);
        }
        public partial class ServiceAppointmentObjectPayload
        {
            [JsonProperty("WorkOrder")]
            public WorkOrder WorkOrder { get; set; }

            [JsonProperty("ServiceAppointment")]
            public ServiceAppointment ServiceAppointment { get; set; }
        }

        public partial class ServiceAppointment
        {
            [JsonProperty("attributes")]
            public Attributes Attributes { get; set; }

            [JsonProperty("Id")]
            public string Id { get; set; }

            [JsonProperty("OwnerId")]
            public string OwnerId { get; set; }

            [JsonProperty("IsDeleted")]
            public bool IsDeleted { get; set; }

            [JsonProperty("AppointmentNumber")]
            public string AppointmentNumber { get; set; }

            [JsonProperty("CreatedDate")]
            public string CreatedDate { get; set; }

            [JsonProperty("CreatedById")]
            public string CreatedById { get; set; }

            [JsonProperty("LastModifiedDate")]
            public string LastModifiedDate { get; set; }

            [JsonProperty("LastModifiedById")]
            public string LastModifiedById { get; set; }

            [JsonProperty("SystemModstamp")]
            public string SystemModstamp { get; set; }

            [JsonProperty("LastViewedDate")]
            public string LastViewedDate { get; set; }

            [JsonProperty("LastReferencedDate")]
            public string LastReferencedDate { get; set; }

            [JsonProperty("ParentRecordId")]
            public string ParentRecordId { get; set; }

            [JsonProperty("ParentRecordType")]
            public string ParentRecordType { get; set; }

            [JsonProperty("AccountId")]
            public string AccountId { get; set; }

            [JsonProperty("WorkTypeId")]
            public string WorkTypeId { get; set; }

            [JsonProperty("ContactId")]
            public string ContactId { get; set; }

            [JsonProperty("Street")]
            public string Street { get; set; }

            [JsonProperty("City")]
            public string City { get; set; }

            [JsonProperty("State")]
            public string State { get; set; }

            [JsonProperty("PostalCode")]
            public string PostalCode { get; set; }

            [JsonProperty("Country")]
            public string Country { get; set; }

            [JsonProperty("Latitude")]
            public double? Latitude { get; set; }

            [JsonProperty("Longitude")]
            public double? Longitude { get; set; }

            [JsonProperty("GeocodeAccuracy")]
            public string GeocodeAccuracy { get; set; }

            [JsonProperty("Address")]
            public Address Address { get; set; }

            [JsonProperty("EarliestStartTime")]
            public string EarliestStartTime { get; set; }

            [JsonProperty("DueDate")]
            public string DueDate { get; set; }

            [JsonProperty("Duration")]
            public double? Duration { get; set; }

            [JsonProperty("Status")]
            public string Status { get; set; }

            [JsonProperty("DurationType")]
            public string DurationType { get; set; }

            [JsonProperty("DurationInMinutes")]
            public double? DurationInMinutes { get; set; }

            [JsonProperty("ServiceTerritoryId")]
            public string ServiceTerritoryId { get; set; }

            [JsonProperty("ParentRecordStatusCategory")]
            public string ParentRecordStatusCategory { get; set; }

            [JsonProperty("StatusCategory")]
            public string StatusCategory { get; set; }

            [JsonProperty("FSL__Auto_Schedule__c")]
            public bool? FslAutoScheduleC { get; set; }

            [JsonProperty("FSL__Emergency__c")]
            public bool? FslEmergencyC { get; set; }

            [JsonProperty("FSL__InJeopardy__c")]
            public bool? FslInJeopardyC { get; set; }

            [JsonProperty("FSL__IsFillInCandidate__c")]
            public bool? FslIsFillInCandidateC { get; set; }

            [JsonProperty("FSL__IsMultiDay__c")]
            public bool? FslIsMultiDayC { get; set; }

            [JsonProperty("FSL__Pinned__c")]
            public bool? FslPinnedC { get; set; }

            [JsonProperty("FSL__Prevent_Geocoding_For_Chatter_Actions__c")]
            public bool? FslPreventGeocodingForChatterActionsC { get; set; }

            [JsonProperty("FSL__Same_Day__c")]
            public bool? FslSameDayC { get; set; }

            [JsonProperty("FSL__Same_Resource__c")]
            public bool? FslSameResourceC { get; set; }

            [JsonProperty("FSL__Schedule_Mode__c")]
            public string FslScheduleModeC { get; set; }

            [JsonProperty("FSL__Schedule_over_lower_priority_appointment__c")]
            public bool? FslScheduleOverLowerPriorityAppointmentC { get; set; }

            [JsonProperty("FSL__Scheduling_Policy_Used__c")]
            public string FslSchedulingPolicyUsedC { get; set; }

            [JsonProperty("FSL__UpdatedByOptimization__c")]
            public bool? FslUpdatedByOptimizationC { get; set; }

            [JsonProperty("FSL__Use_Async_Logic__c")]
            public bool? FslUseAsyncLogicC { get; set; }

            [JsonProperty("FSL__Virtual_Service_For_Chatter_Action__c")]
            public bool? FslVirtualServiceForChatterActionC { get; set; }

            [JsonProperty("FSL__Duration_In_Minutes__c")]
            public double? FslDurationInMinutesC { get; set; }

            [JsonProperty("Schedule_Flexibility__c")]
            public bool? ScheduleFlexibilityC { get; set; }

            [JsonProperty("Technician_GUID__c")]
            public string TechnicianGuidC { get; set; }

            [JsonProperty("Weekend_Appointment__c")]
            public bool? WeekendAppointmentC { get; set; }

            [JsonProperty("Cancellation_Reason__c")]
            public string CancellationReasonC { get; set; }

            [JsonProperty("Description")]
            public string Description { get; set; }

            [JsonProperty("ArrivalWindowStartTime")]
            public DateTime? ArrivalWindowStartTime { get; set; }

            [JsonProperty("ArrivalWindowEndTime")]
            public DateTime? ArrivalWindowEndTime { get; set; }

            [JsonProperty("SchedStartTime")]
            public DateTime? SchedStartTime { get; set; }

            [JsonProperty("SchedEndTime")]
            public DateTime? SchedEndTime { get; set; }

            [JsonProperty("ActualStartTime")]
            public DateTime? ActualStartTime { get; set; }

            [JsonProperty("ActualEndTime")]
            public DateTime? ActualEndTime { get; set; }

            [JsonProperty("ActualDuration")]
            public string ActualDuration { get; set; }

            [JsonProperty("Subject")]
            public string Subject { get; set; }

            [JsonProperty("ServiceNote")]
            public string ServiceNote { get; set; }

            [JsonProperty("FSL__Appointment_Grade__c")]
            public string FslAppointmentGradeC { get; set; }

            [JsonProperty("FSL__GanttColor__c")]
            public string FslGanttColorC { get; set; }

            [JsonProperty("FSL__GanttIcon__c")]
            public string FslGanttIconC { get; set; }

            [JsonProperty("FSL__GanttLabel__c")]
            public string FslGanttLabelC { get; set; }

            [JsonProperty("FSL__Gantt_Display_Date__c")]
            public DateTime? FslGanttDisplayDateC { get; set; }

            [JsonProperty("FSL__InJeopardyReason__c")]
            public string FslInJeopardyReasonC { get; set; }

            [JsonProperty("FSL__InternalSLRGeolocation__Latitude__s")]
            public double? FslInternalSlrGeolocationLatitudeS { get; set; }

            [JsonProperty("FSL__InternalSLRGeolocation__Longitude__s")]
            public double? FslInternalSlrGeolocationLongitudeS { get; set; }

            [JsonProperty("FSL__MDS_Calculated_length__c")]
            public string FslMdsCalculatedLengthC { get; set; }

            [JsonProperty("FSL__MDT_Operational_Time__c")]
            public string FslMdtOperationalTimeC { get; set; }

            [JsonProperty("FSL__Related_Service__c")]
            public string FslRelatedServiceC { get; set; }

            [JsonProperty("FSL__Time_Dependency__c")]
            public string FslTimeDependencyC { get; set; }
        }

        public partial class Address
        {
            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("geocodeAccuracy")]
            public string GeocodeAccuracy { get; set; }

            [JsonProperty("latitude")]
            public double? Latitude { get; set; }

            [JsonProperty("longitude")]
            public double? Longitude { get; set; }

            [JsonProperty("postalCode")]
            public string PostalCode { get; set; }

            [JsonProperty("state")]
            public string State { get; set; }

            [JsonProperty("street")]
            public string Street { get; set; }
        }

        public partial class Attributes
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }
        }

        public partial class WorkOrder
        {
            [JsonProperty("attributes")]
            public Attributes Attributes { get; set; }

            [JsonProperty("Id")]
            public string Id { get; set; }

            [JsonProperty("OwnerId")]
            public string OwnerId { get; set; }

            [JsonProperty("IsDeleted")]
            public bool? IsDeleted { get; set; }

            [JsonProperty("WorkOrderNumber")]
            public string WorkOrderNumber { get; set; }

            [JsonProperty("CreatedDate")]
            public string CreatedDate { get; set; }

            [JsonProperty("CreatedById")]
            public string CreatedById { get; set; }

            [JsonProperty("LastModifiedDate")]
            public string LastModifiedDate { get; set; }

            [JsonProperty("LastModifiedById")]
            public string LastModifiedById { get; set; }

            [JsonProperty("SystemModstamp")]
            public string SystemModstamp { get; set; }

            [JsonProperty("LastViewedDate")]
            public string LastViewedDate { get; set; }

            [JsonProperty("LastReferencedDate")]
            public string LastReferencedDate { get; set; }

            [JsonProperty("AccountId")]
            public string AccountId { get; set; }

            [JsonProperty("ContactId")]
            public string ContactId { get; set; }

            [JsonProperty("Street")]
            public string Street { get; set; }

            [JsonProperty("City")]
            public string City { get; set; }

            [JsonProperty("State")]
            public string State { get; set; }

            [JsonProperty("PostalCode")]
            public string PostalCode { get; set; }

            [JsonProperty("Country")]
            public string Country { get; set; }

            [JsonProperty("Latitude")]
            public double? Latitude { get; set; }

            [JsonProperty("Longitude")]
            public double? Longitude { get; set; }

            [JsonProperty("GeocodeAccuracy")]
            public string GeocodeAccuracy { get; set; }

            [JsonProperty("Address")]
            public Address Address { get; set; }

            [JsonProperty("RootWorkOrderId")]
            public string RootWorkOrderId { get; set; }

            [JsonProperty("Status")]
            public string Status { get; set; }

            [JsonProperty("Priority")]
            public string Priority { get; set; }

            [JsonProperty("Subtotal")]
            public double? Subtotal { get; set; }

            [JsonProperty("TotalPrice")]
            public double? TotalPrice { get; set; }

            [JsonProperty("LineItemCount")]
            public double? LineItemCount { get; set; }

            [JsonProperty("Discount")]
            public double? Discount { get; set; }

            [JsonProperty("GrandTotal")]
            public double? GrandTotal { get; set; }

            [JsonProperty("IsClosed")]
            public bool? IsClosed { get; set; }

            [JsonProperty("Duration")]
            public double? Duration { get; set; }

            [JsonProperty("DurationType")]
            public string DurationType { get; set; }

            [JsonProperty("DurationInMinutes")]
            public double? DurationInMinutes { get; set; }

            [JsonProperty("ServiceAppointmentCount")]
            public double? ServiceAppointmentCount { get; set; }

            [JsonProperty("WorkTypeId")]
            public string WorkTypeId { get; set; }

            [JsonProperty("ServiceTerritoryId")]
            public string ServiceTerritoryId { get; set; }

            [JsonProperty("StatusCategory")]
            public string StatusCategory { get; set; }

            [JsonProperty("IsGeneratedFromMaintenancePlan")]
            public bool? IsGeneratedFromMaintenancePlan { get; set; }

            [JsonProperty("FSL__IsFillInCandidate__c")]
            public bool? FslIsFillInCandidateC { get; set; }

            [JsonProperty("FSL__Prevent_Geocoding_For_Chatter_Actions__c")]
            public bool? FslPreventGeocodingForChatterActionsC { get; set; }

            [JsonProperty("FSL__Scheduling_Priority__c")]
            public double? FslSchedulingPriorityC { get; set; }

            [JsonProperty("Bill_Account_Number__c")]
            public string BillAccountNumberC { get; set; }

            [JsonProperty("CMC_Customer_GUID__c")]
            public Guid CmcCustomerGuidC { get; set; }

            [JsonProperty("Program_ID__c")]
            public string ProgramIdC { get; set; }

            [JsonProperty("Sub_Program_ID__c")]
            public string SubProgramIdC { get; set; }

            [JsonProperty("AssetId")]
            public string AssetId { get; set; }

            [JsonProperty("Description")]
            public string Description { get; set; }

            [JsonProperty("StartDate")]
            public DateTime? StartDate { get; set; }

            [JsonProperty("EndDate")]
            public DateTime? EndDate { get; set; }

            [JsonProperty("Subject")]
            public string Subject { get; set; }

            [JsonProperty("Tax")]
            public string Tax { get; set; }

            [JsonProperty("ParentWorkOrderId")]
            public string ParentWorkOrderId { get; set; }

            [JsonProperty("BusinessHoursId")]
            public string BusinessHoursId { get; set; }

            [JsonProperty("SuggestedMaintenanceDate")]
            public DateTime? SuggestedMaintenanceDate { get; set; }

            [JsonProperty("MinimumCrewSize")]
            public int? MinimumCrewSize { get; set; }

            [JsonProperty("RecommendedCrewSize")]
            public int? RecommendedCrewSize { get; set; }

            [JsonProperty("ServiceReportTemplateId")]
            public string ServiceReportTemplateId { get; set; }

            [JsonProperty("ServiceReportLanguage")]
            public string ServiceReportLanguage { get; set; }
        }
    }
}
