using System;
using Shared.Entities.DTO.Customer;

namespace Shared.Entities._SearchCriteria.Client
{
    public class SearchLead : SearchParent
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public string getWhereClause()
        {
            var WhereClause = string.Empty;
            if (!string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Email) || !string.IsNullOrEmpty(Phone) ||
                !string.IsNullOrEmpty(Address) || ProgramGuid != Guid.Empty)
            {
                WhereClause = " where (_contact.FirstName like @Name or _contact.LastName like @Name) " +
                    " and ( _email.EmailAddress like @Email  or  _phone.phoneNumber  like @Phone ) " +
                    " and ( _address.StreetAddress1 like @Address  or _address.City like @Address  " +
                              " or _address.State like @Address  or _address.Zip like @Address )";

                if (ProgramGuid != Guid.Empty)
                {
                    WhereClause += " and ( _account.ProgramId = @ProgramGuid) ";
                }
            }


            return WhereClause;
        }

        public string GetWhereFromObject(Lead lead)
        {
            return string.Empty;
        }
    }

    public class DetailedLeadGet
    {
        public Guid CustomerGuid { get;set; }
    }

    //Used for incoming call. 
    public class SearchLeadSimple : SearchParent
    {
        public int? NumberOfRecords { get; set; }
        public int? CMCCustomerID { get; set; }
        public string CustomerName { get; set; }
        public string BillAccount { get; set; }
        public string StAddress { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public string getWhereClause()
        {
            var WhereClause = " where ";
            WhereClause += "  _account.SFAccountGuid is null ";
            var IsFirst = false; 
            
            //Checking for nulls
            if (string.IsNullOrEmpty(CustomerName) && string.IsNullOrEmpty(BillAccount) &&
                string.IsNullOrEmpty(PhoneNumber) && string.IsNullOrEmpty(EmailAddress) &&
                string.IsNullOrEmpty(ZipCode) && string.IsNullOrEmpty(StAddress) && 
                (CMCCustomerID == null || CMCCustomerID <= 0) &&
                ProgramGuid == Guid.Empty) return WhereClause;
            
            if (!string.IsNullOrEmpty(EmailAddress))
            {
                if (!IsFirst)
                {
                    WhereClause += " and ";
                }

                IsFirst = false;
                WhereClause += $" _email.EmailAddress like '%{EmailAddress}%' ";
            }
            if (!string.IsNullOrEmpty(CustomerName))
            {
                if (!IsFirst)
                {
                    WhereClause += " and ";
                }

                IsFirst = false;

                WhereClause += $" _contact.FullName like '%{CustomerName}%' ";
            }
            if (CMCCustomerID != null && CMCCustomerID >= 0)
            {
                if (!IsFirst)
                {
                    WhereClause += " and ";
                }

                IsFirst = false;
                WhereClause += $" _account.CMCAccountID = {CMCCustomerID} ";
            }
            if (!string.IsNullOrEmpty(BillAccount))
            {
                if (!IsFirst)
                {
                    WhereClause += " and ";
                }

                IsFirst = false;
                WhereClause += $" _account.BillingAccount like '%{BillAccount}%' ";
            }
            if (!string.IsNullOrEmpty(ZipCode))
            {
                if (!IsFirst)
                {
                    WhereClause += " and ";
                }

                IsFirst = false;
                WhereClause += $" _address.Zip like '%{ZipCode}%' ";
            }
            if (!string.IsNullOrEmpty(StAddress))
            {
                if (!IsFirst)
                {
                    WhereClause += " and ";
                }

                IsFirst = false;
                WhereClause += $" _address.StreetAddress1 like '%{StAddress}%' ";
            }
            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                if (!IsFirst)
                {
                    WhereClause += " and ";
                }

                IsFirst = false;
                WhereClause += $" _phone.phoneNumber like '%{PhoneNumber}%' ";
            }
            if (ProgramGuid != Guid.Empty)
            {
                if (!IsFirst)
                {
                    WhereClause += " and ";
                }

                IsFirst = false;
                WhereClause += $" ( _account.ProgramId = '{ProgramGuid}') ";
            }
            
            return WhereClause;
        }

       
    }

}
