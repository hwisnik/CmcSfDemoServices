using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataAccess
{
    public class QueryBuilder
    {
        private StringBuilder InitialQuery { get; set; }

        private List<string> AndConditions { get; set; }
        private List<string> OrConditions { get; set; }
        private StringBuilder WhereClause { get; set; }

        private List<string> HavingOrConditions { get; set; }
        private List<string> HavingAndConditions { get; set; }

        public StringBuilder HavingClause { get; set; }
        public StringBuilder GroupByClause { get; set; }

        public QueryBuilder(string SelectQuery)
        {
            InitialQuery = new StringBuilder();
            InitialQuery.Append(SelectQuery);
            AndConditions = new List<string>();
            OrConditions = new List<string>();
            HavingAndConditions = new List<string>();
            HavingOrConditions = new List<string>();
            WhereClause = new StringBuilder();
            HavingClause= new StringBuilder();
            GroupByClause = new StringBuilder();
        }

        public void WhereAndCondition(string WhereClauseContent)
        {
            AndConditions.Add(" " + WhereClauseContent + " ");
        }
        public void WhereOrCondition(string WhereClauseContent)
        {
            OrConditions.Add(" " + WhereClauseContent + " ");
            
        }
        public void WhereAndCondition(string WhereClauseContent, string StringParameter)
        {
            if (string.IsNullOrEmpty(StringParameter)) return;
            AndConditions.Add(" " + WhereClauseContent + " ");
        }

        public void WhereOrCondition(string WhereClauseContent, string StringParameter)
        {
            if (string.IsNullOrEmpty(StringParameter)) return;
            OrConditions.Add(" " + WhereClauseContent + " "); 
        }

        public void WhereAndCondition(string WhereClauseContent, Guid? GuidParameter)
        {
            if (GuidParameter == Guid.Empty || GuidParameter == null) return;
            AndConditions.Add(" " + WhereClauseContent + " ");
        }

        public void WhereOrCondition(string WhereClauseContent, Guid? GuidParameter)
        {
            if (GuidParameter == Guid.Empty || GuidParameter == null) return;
            OrConditions.Add(" " + WhereClauseContent + " ");
            
        }
        public void WhereAndCondition(string WhereClauseContent, int? IntParameter)
        {
            if (IntParameter == null) return;
            AndConditions.Add(" " + WhereClauseContent + " ");

        }

        public void WhereOrCondition(string WhereClauseContent, int? IntParameter)
        {
            if (IntParameter == null) return;
            OrConditions.Add(" " + WhereClauseContent + " ");
        }
        

        public void WhereAndCondition(string WhereClauseContent, double? DoubleParameter)
        {
            if (DoubleParameter == null) return;
            AndConditions.Add(" " + WhereClauseContent + " ");
        }

        public void WhereOrCondition(string WhereClauseContent, double? DoubleParameter)
        {
            if (DoubleParameter == null) return;
            OrConditions.Add(" " + WhereClauseContent + " ");
        }

        public string GetQuery()
        {
            if (!AndConditions.Any() && !OrConditions.Any()) return InitialQuery + " " + WhereClause;
            
            var IsFirst = true;
            WhereClause = new StringBuilder(" where ");

            // Add And Conditions
            if (AndConditions.Any())
            {
                foreach (var condition in AndConditions)
                {
                    if (!IsFirst)
                    {
                        WhereClause.Append(" and ");
                    }
                    WhereClause.Append(" " + condition + " ");
                    IsFirst = false;
                }
            }

            //Add Or Conditions
            if (OrConditions.Any())
            {
                foreach (var condition in OrConditions)
                {
                    if (!IsFirst)
                    {
                        WhereClause.Append(" or ");
                    }
                    WhereClause.Append(" " + condition + " ");
                    IsFirst = false;
                }
            }
            return InitialQuery + " " + WhereClause + " " + GroupByClause + " " + HavingClause;
        }
    }

}
