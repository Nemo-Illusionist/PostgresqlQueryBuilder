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
            return queryable.AddDiff(x =>
            {
                x.SelectExpression = expression;
                return x;
            });
        }

        public static PostgresqlQueryable<T> SelectDistinct<T>(
            this PostgresqlQueryable<T> queryable,
            Expression<Func<T, object>> expression,
            bool isDistinct)
        {
            return queryable.AddDiff(x =>
            {
                x.SelectExpression = expression;
                x.IsDistinct = isDistinct;
                return x;
            });
        }

        public static PostgresqlQueryable<T> SelectDistinct<T>(
            this PostgresqlQueryable<T> queryable,
            Expression<Func<T, object>> expression,
            Expression<Func<T, object>> distinctExpression)
        {
            return queryable.AddDiff(x =>
            {
                x.SelectExpression = expression;
                x.IsDistinct = true;
                x.DistinctExpression = distinctExpression;

                return x;
            });
        }
    }
}