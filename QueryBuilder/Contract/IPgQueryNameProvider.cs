using System.Linq.Expressions;

namespace QueryBuilder.Contract
{
    internal interface IPgQueryNameProvider
    {
        string GetTableName(string key);
        string GetTableName(Expression expression);
    }
}