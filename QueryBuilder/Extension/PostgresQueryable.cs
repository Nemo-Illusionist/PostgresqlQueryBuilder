using System;
using System.Linq.Expressions;

namespace QueryBuilder.Extension
{
    public static class PostgresqlQueryableExtension
    {
        public static PostgresqlQueryable<T> Select<T>(
            this PostgresqlQueryable<T> queryable,
            Expression<Func<T, object>> expression)
        {
            queryable.SelectExpression = expression;
            return queryable;
        }

        public static PostgresqlQueryable<T> SelectDistinct<T>(
            this PostgresqlQueryable<T> queryable,
            Expression<Func<T, object>> expression,
            bool isDistinct)
        {
            queryable.IsDistinct = isDistinct;
            return Select(queryable, expression);
        }

        public static PostgresqlQueryable<T> SelectDistinct<T>(
            this PostgresqlQueryable<T> queryable,
            Expression<Func<T, object>> expression,
            Expression<Func<T, object>> distinctExpression)
        {
            queryable.IsDistinct = true;
            queryable.DistinctExpression = distinctExpression;
            return Select(queryable, expression);
        }
    }
}