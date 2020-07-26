using System;
using System.Linq.Expressions;
using QueryBuilder.Entities;

namespace QueryBuilder.Extension
{
    public static class PostgresqlQueryableExtension
    {
        public static PostgresqlQueryable<T> Select<T>(
            this PostgresqlQueryable<T> queryable,
            Expression<Func<T, object>> expression)
        {
            void Mutate(ref PostgresqlQueryableInfo<T> info)
            {
                info.SelectExpression = expression;
            }

            return queryable.AddMutation(Mutate);
        }

        public static PostgresqlQueryable<T> SelectDistinct<T>(
            this PostgresqlQueryable<T> queryable,
            Expression<Func<T, object>> expression,
            bool isDistinct)
        {
            void Mutate(ref PostgresqlQueryableInfo<T> info)
            {
                info.SelectExpression = expression;
                info.IsDistinct = isDistinct;
            }

            return queryable.AddMutation(Mutate);
        }

        public static PostgresqlQueryable<T> SelectDistinct<T>(
            this PostgresqlQueryable<T> queryable,
            Expression<Func<T, object>> expression,
            Expression<Func<T, object>> distinctExpression)
        {
            void Mutate(ref PostgresqlQueryableInfo<T> info)
            {
                info.SelectExpression = expression;
                info.IsDistinct = true;
                info.DistinctExpression = distinctExpression;
            }

            return queryable.AddMutation(Mutate);
        }
    }
}