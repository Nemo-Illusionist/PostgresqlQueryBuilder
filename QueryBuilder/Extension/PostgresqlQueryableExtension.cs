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

        public static IPostgresqlQueryable<TResult> Select<T, TResult>(
            this IPostgresqlQueryable<T> queryable,
            Expression<Func<T, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public static IPostgresqlQueryable<TResult> SelectDistinct<T, TResult>(
            this IPostgresqlQueryable<T> queryable,
            Expression<Func<T, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public static IPostgresqlQueryable<T> SelectDistinct<T>(this IPostgresqlQueryable<T> queryable)
        {
            return SelectDistinct(queryable, x => x);
        }

        public static IPostgresqlQueryable<TResult> SelectDistinctOn<T, TResult, TDistinct>(
            this IPostgresqlQueryable<T> queryable,
            Expression<Func<T, TResult>> expression,
            Expression<Func<T, TDistinct>> distinctExpression)
        {
            throw new NotImplementedException();
        }

        public static IPostgresqlQueryable<T> SelectDistinctOn<T, TDistinct>(
            this IPostgresqlQueryable<T> queryable,
            Expression<Func<T, TDistinct>> distinctExpression)
        {
            return SelectDistinctOn(queryable, x => x, distinctExpression);
        }
    }
}