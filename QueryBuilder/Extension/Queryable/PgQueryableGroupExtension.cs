using System;
using System.Linq.Expressions;
using QueryBuilder.Contract;

namespace QueryBuilder.Extension.Queryable
{
    public static class PgQueryableGroupExtension
    {
        public static IPgGroupQueryable<T> GroupBy<T, TKey>(
            this IPgQueryable<T> queryable,
            Expression<Func<T, TKey>> expression)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(GroupBy), typeof(T), queryable.Node, expression);
            return (IPgGroupQueryable<T>) queryable.Provider.CreateQuery<T>(node);
        }        
        
        public static IPgGroupQueryable<T> GroupBy<T>(
            this IPgQueryable<T> queryable)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(GroupBy), typeof(T), queryable.Node);
            return (IPgGroupQueryable<T>) queryable.Provider.CreateQuery<T>(node);
        }        
        
        public static IPgGroupQueryable<T> GroupingSets<T>(
            this IPgGroupQueryable<T> queryable,
            params Expression<Func<T, object>>[] expressions)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(GroupingSets), typeof(T), queryable.Node, expressions);
            return (IPgGroupQueryable<T>) queryable.Provider.CreateQuery<T>(node);
        }        
        
        public static IPgGroupQueryable<T> Cube<T>(
            this IPgGroupQueryable<T> queryable,
            params Expression<Func<T, object>>[] expressions)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(Cube), typeof(T), queryable.Node, expressions);
            return (IPgGroupQueryable<T>) queryable.Provider.CreateQuery<T>(node);
        }     
        
        public static IPgGroupQueryable<T> Rollup<T>(
            this IPgGroupQueryable<T> queryable,
            params Expression<Func<T, object>>[] expressions)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(Rollup), typeof(T), queryable.Node, expressions);
            return (IPgGroupQueryable<T>) queryable.Provider.CreateQuery<T>(node);
        }

        public static IPgQueryable<T> Having<T>(
            this IPgGroupQueryable<T> queryable,
            Expression<Func<T, bool>> expression)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(Having), typeof(T), queryable.Node, expression);
            return queryable.Provider.CreateQuery<T>(node);
        }
    }
}