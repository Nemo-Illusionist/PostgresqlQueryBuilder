using System;
using System.Linq.Expressions;
using QueryBuilder.Contract;
using QueryBuilder.Entities;
using QueryBuilder.Helpers;

namespace QueryBuilder.Extension.Queryable
{
    public static partial class PgQueryableJoinExtension
    {
        public static IPgFromQueryable<IPgJoin<T1, TAdd>> CrossJoin<T1, TAdd>(
            this IPgFromQueryable<T1> queryable,
            EmptyGeneric<TAdd> _ = default)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(CrossJoin), typeof(IPgJoin<T1, TAdd>), queryable.Node);
            return (IPgFromQueryable<IPgJoin<T1, TAdd>>) queryable.Provider.CreateQuery<IPgJoin<T1, TAdd>>(node);
        }            

        public static IPgFromQueryable<IPgJoin<T1, TAdd>> Join<T1, TAdd>(
            this IPgFromQueryable<T1> queryable,
            Expression<Func<T1, TAdd, bool>> expression,
            EmptyGeneric<TAdd> _ = default)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(Join), typeof(IPgJoin<T1, TAdd>), queryable.Node, expression);
            return (IPgFromQueryable<IPgJoin<T1, TAdd>>) queryable.Provider.CreateQuery<IPgJoin<T1, TAdd>>(node);
        }

        public static IPgFromQueryable<IPgJoin<T1, TAdd>> LeftJoin<T1, TAdd>(
            this IPgFromQueryable<T1> queryable,
            Expression<Func<T1, TAdd, bool>> expression,
            EmptyGeneric<TAdd> _ = default)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(LeftJoin), typeof(IPgJoin<T1, TAdd>), queryable.Node, expression);
            return (IPgFromQueryable<IPgJoin<T1, TAdd>>) queryable.Provider.CreateQuery<IPgJoin<T1, TAdd>>(node);
        }

        public static IPgFromQueryable<IPgJoin<T1, TAdd>> RightJoin<T1, TAdd>(
            this IPgFromQueryable<T1> queryable,
            Expression<Func<T1, TAdd, bool>> expression,
            EmptyGeneric<TAdd> _ = default)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(RightJoin), typeof(IPgJoin<T1, TAdd>), queryable.Node, expression);
            return (IPgFromQueryable<IPgJoin<T1, TAdd>>) queryable.Provider.CreateQuery<IPgJoin<T1, TAdd>>(node);
        }

        public static IPgFromQueryable<IPgJoin<T1, TAdd>> FullJoin<T1, TAdd>(
            this IPgFromQueryable<T1> queryable,
            Expression<Func<T1, TAdd, bool>> expression,
            EmptyGeneric<TAdd> _ = default)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof(FullJoin), typeof(IPgJoin<T1, TAdd>), queryable.Node, expression);
            return (IPgFromQueryable<IPgJoin<T1, TAdd>>) queryable.Provider.CreateQuery<IPgJoin<T1, TAdd>>(node);
        }
    }
}