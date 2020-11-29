using System;
using System.Linq.Expressions;
using QueryBuilder.Contract;

namespace QueryBuilder.Extension.Queryable
{
    public static class PgQueryableSelectExtension
    {
        public static IPgQueryable<TResult> Select<T, TResult>(
            this IPgQueryable<T> queryable,
            Expression<Func<T, TResult>> expression)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(Select), typeof(T), queryable.Node, expression);
            return queryable.Provider.CreateQuery<TResult>(node);
        }

        public static IPgQueryable<T> SelectDistinct<T>(this IPgQueryable<T> queryable)
        {
            return SelectDistinct(queryable, x => x);
        }

        public static IPgQueryable<TResult> SelectDistinct<T, TResult>(
            this IPgQueryable<T> queryable,
            Expression<Func<T, TResult>> expression)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(SelectDistinct), typeof(T), queryable.Node, expression);
            return queryable.Provider.CreateQuery<TResult>(node);
        }

        public static IPgQueryable<T> SelectDistinctOn<T, TDistinct>(
            this IPgQueryable<T> queryable,
            Expression<Func<T, TDistinct>> distinctExpression)
        {
            return SelectDistinctOn(queryable, x => x, distinctExpression);
        }

        public static IPgQueryable<TResult> SelectDistinctOn<T, TResult, TDistinct>(
            this IPgQueryable<T> queryable,
            Expression<Func<T, TResult>> expression,
            Expression<Func<T, TDistinct>> distinctExpression)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(SelectDistinctOn), typeof(T), queryable.Node, expression,
                distinctExpression);
            return queryable.Provider.CreateQuery<TResult>(node);
        }
    }
}