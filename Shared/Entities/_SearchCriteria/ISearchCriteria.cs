namespace Shared.Entities._SearchCriteria
{
    public interface ISearchCriteria
    {
        string getWhereClause();
        string getGroupByClause();
        string getOrderByClause();
        string getQuery(); 
    }
}
