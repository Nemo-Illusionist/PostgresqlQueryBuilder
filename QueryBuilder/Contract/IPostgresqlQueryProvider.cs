using System.Linq.Expressions;

namespace QueryBuilder.Contract
{
    public interface IPostgresqlQueryProvider
    {
        IPostgresqlQueryable<T> CreateQuery<T>(QueryMethod method, Expression expression);
    }
}