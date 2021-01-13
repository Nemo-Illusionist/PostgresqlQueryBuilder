using System;
using System.Linq.Expressions;
using QueryBuilder.Contract;
using QueryBuilder.Entities;

namespace QueryBuilder.Extension.Queryable
{
    public static class PgQueryableExtension
    {
        public static IPgQueryable<T> Where<T>(
            this IPgFromQueryable<T> queryable,
            Expression<Func<T, bool>> expression)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(Where), typeof(T), queryable.Node, expression);
            return queryable.Provider.CreateQuery<T>(node);
        }

        public static IPgQueryable<T> Limit<T>(
            this IPgFromQueryable<T> queryable,
            int count)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(Limit), typeof(T), queryable.Node,
                Expression.Lambda(Expression.Constant(count)));
            return queryable.Provider.CreateQuery<T>(node);
        }

        public static IPgQueryable<T> Offset<T>(
            this IPgFromQueryable<T> queryable,
            int count)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(Offset), typeof(T), queryable.Node,
                Expression.Lambda(Expression.Constant(count)));
            return queryable.Provider.CreateQuery<T>(node);
        }
        
        public static IPgQueryable<T> Having<T>(
            this IPgQueryable<T> queryable,
            Expression<Func<T, bool>> expression)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(Having), typeof(T), queryable.Node, expression);
            return queryable.Provider.CreateQuery<T>(node);
        }

        public static string ToQueryString<T>(this IPgQueryable<T> queryable)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            return queryable.Provider.Execute(queryable.Node);
        }        
    }
}