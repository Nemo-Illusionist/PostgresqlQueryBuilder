using System.Collections.Generic;
using QueryBuilder.Builders;
using QueryBuilder.Entities;

namespace QueryBuilder
{
    public delegate void MutateInfo<T>(ref PostgresqlQueryableInfo<T> info);

    public class PostgresqlQueryable<T>
    {
        public PostgresqlQueryable(MutateInfo<T> mutate, PostgresqlQueryable<T> prev = null)
        {
            Mutate = mutate;
            Prev = prev;
        }

        private PostgresqlQueryable<T> Prev { get; }
        private MutateInfo<T> Mutate { get; }

        public PostgresqlQueryable<T> AddMutation(MutateInfo<T> mutation)
        {
            return new PostgresqlQueryable<T>(mutation, this);
        }

        public string ToQueryString()
        {
            var postgresqlQueryInfo = BuildPostgresqlQueryableInfo();
            var queryInfo = new QueryInfoBuilder().Build(postgresqlQueryInfo);
            var query = new QueryStringBuilder().Build(queryInfo);

            return query;
        }

        private PostgresqlQueryableInfo<T> BuildPostgresqlQueryableInfo()
        {
            var mutationsStack = new Stack<MutateInfo<T>>();
            var current = this;
            do
            {
                mutationsStack.Push(current.Mutate);
                current = current.Prev;
            } while (current != null);

            PostgresqlQueryableInfo<T> info = null;
            while (mutationsStack.TryPop(out var mutate)) mutate(ref info);

            return info;
        }
    }
}