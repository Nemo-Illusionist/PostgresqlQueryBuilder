using System;
using System.Linq.Expressions;
using QueryBuilder.Contract;

namespace QueryBuilder.Extension
{
    public static class PgQueryableExtension
    {
        public static IPgQueryable<T> Where<T>(
            this IPgQueryable<T> queryable,
            Expression<Func<T, bool>> expression)
        {
            var node = new PgQueryNode(nameof(Where), typeof(T), queryable.Node, expression);
            return queryable.Provider.CreateQuery<T>(node);
        }

        public static IPgQueryable<TResult> Select<T, TResult>(
            this IPgQueryable<T> queryable,
            Expression<Func<T, TResult>> expression)
        {
            var node = new PgQueryNode(nameof(Select), typeof(T), queryable.Node, expression);
            return queryable.Provider.CreateQuery<TResult>(node);
        }

        public static IPgQueryable<TResult> SelectDistinct<T, TResult>(
            this IPgQueryable<T> queryable,
            Expression<Func<T, TResult>> expression)
        {
            var node = new PgQueryNode(nameof(SelectDistinct), typeof(T), queryable.Node, expression);
            return queryable.Provider.CreateQuery<TResult>(node);
        }

        public static IPgQueryable<T> SelectDistinct<T>(this IPgQueryable<T> queryable)
        {
            return SelectDistinct(queryable, x => x);
        }

        public static IPgQueryable<TResult> SelectDistinctOn<T, TResult, TDistinct>(
            this IPgQueryable<T> queryable,
            Expression<Func<T, TResult>> expression,
            Expression<Func<T, TDistinct>> distinctExpression)
        {
            var node = new PgQueryNode(nameof(SelectDistinctOn), typeof(T), queryable.Node, expression,
                distinctExpression);
            return queryable.Provider.CreateQuery<TResult>(node);
        }

        public static IPgQueryable<T> SelectDistinctOn<T, TDistinct>(
            this IPgQueryable<T> queryable,
            Expression<Func<T, TDistinct>> distinctExpression)
        {
            return SelectDistinctOn(queryable, x => x, distinctExpression);
        }

        public static string ToQueryString<T>(this IPgQueryable<T> queryable)
        {
            return queryable.Provider.Execute(queryable.Node);
        }
    }
}