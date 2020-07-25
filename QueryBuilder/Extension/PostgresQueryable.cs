using System;
using System.Linq.Expressions;
using QueryBuilder.Contract;

namespace QueryBuilder.Extension
{
    public static class PostgresqlQueryableExtension
    {
        public static IPostgresqlQueryable<T> Where<T>(
            this IPostgresqlQueryable<T> queryable,
            Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public static ISelectPostgresqlQueryable<TResult> Select<T, TResult>(
            this IPostgresqlQueryable<T> queryable,
            Expression<Func<T, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public static ISelectPostgresqlQueryable<TResult> SelectDistinct<T, TResult>(
            this IPostgresqlQueryable<T> queryable,
            Expression<Func<T, TResult>> expression,
            bool isDistinct)
        {
            throw new NotImplementedException();
        }

        public static ISelectPostgresqlQueryable<TResult> SelectDistinct<T, TResult, TDistinct>(
            this IPostgresqlQueryable<T> queryable,
            Expression<Func<T, TResult>> expression,
            Expression<Func<TResult, TDistinct>> distinctExpression)
        {
            throw new NotImplementedException();
        }
    }
}