﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.QueryResources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class LeadQueries {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LeadQueries() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DataAccess.QueryResources.LeadQueries", typeof(LeadQueries).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT [LeadGuid]
        ///      ,[CustomerGuid]
        ///      ,[SFLeadID]
        ///      ,[LeadStatusGuid]
        ///      ,[LeadSource]
        ///      ,[QualifiedAuditTypeGuid]
        ///      ,[QualifiedRate]
        ///      ,[LeadStatusReason]
        ///      ,[ReservedDate]
        ///      ,[LeadStatusChangeDate]
        ///  FROM [Customer].[Lead] cl
        ///  WHERE cl.CustomerGuid = @customerGuid.
        /// </summary>
        internal static string GetLeadByCustomerGuid {
            get {
                return ResourceManager.GetString("GetLeadByCustomerGuid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select cp.Phone_Number from contact.phone cp
        ///where cp.ContactGuid= @ContactGuid.
        /// </summary>
        internal static string GetPhoneNumberByContactGuidQuery {
            get {
                return ResourceManager.GetString("GetPhoneNumberByContactGuidQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///     CL.CustomerGuid
        ///    ,CC.ContactGuid
        ///    ,CC.FullName 
        ///    ,cl.QualifiedRate
        ///    ,CUS.Rate 
        ///    ,CUS.AverageUsage 
        ///    ,CUS.HeatingAverageUsage 
        ///    ,CA.Latitude 
        ///    ,CA.Longitude 
        ///    ,CONCAT(CA.StreetAddress1, &apos; &apos;, CA.StreetAddress2, &apos; &apos;, CA.City + &apos;, &apos;, CA.State, &apos; &apos;, CA.Zip)  Address 
        ///    ,CL.SfLeadId 
        ///    ,LRC.CapTier
        ///    ,CL.LeadStatusGuid
        ///    ,Cl.LeadStatusReason
        ///    ,CL.ReservedDate
        ///    ,CL.NextAvailableCallDate
        ///   ,CL.LeadStatusChangeDate
        ///
        ///FROM [Customer].[Lead] CL         /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string LiurpElectricAndGasAuditQuery {
            get {
                return ResourceManager.GetString("LiurpElectricAndGasAuditQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT TOP {input.NumberOfLeadsToQuery.ToString()} CL.CustomerGuid 
        ///    , CC.FullName 
        ///    ,CoP.Phone_Number PhoneNumber 
        ///	,cl.QualifiedRate
        ///    , CU.Rate 
        ///    ,CU.AverageUsage 
        ///    ,CU.HeatingAverageUsage 
        ///    ,CA.Latitude 
        ///    ,CA.Longitude 
        ///    ,CONCAT(CA.StreetAddress1, &apos; &apos;, CA.StreetAddress2, &apos; &apos;, CA.City + &apos;, &apos;, CA.State, &apos; &apos;, CA.Zip)  Address 
        ///    ,CL.SfLeadId 
        ///    ,CU.CapTier CapStatus
        ///FROM [Customer].[Lead] CL 
        ///INNER JOIN[Contact].[Contact] CC ON CC.CustomerGuid = CL.CustomerGuid 
        ///INN [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string LiurpGasHeatingOnlyQuery {
            get {
                return ResourceManager.GetString("LiurpGasHeatingOnlyQuery", resourceCulture);
            }
        }
    }
}
