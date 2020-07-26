using System.Collections.Generic;
using QueryBuilder.Builders;
using QueryBuilder.Entities;

namespace QueryBuilder
{
    public delegate PostgresqlQueryableInfo<T> DiffFunc<T>(PostgresqlQueryableInfo<T> info);

    public class PostgresqlQueryable<T>
    {
        public PostgresqlQueryable(DiffFunc<T> diffFunc, PostgresqlQueryable<T> prev = null)
        {
            Diff = diffFunc;
            Prev = prev;
        }

        private PostgresqlQueryable<T> Prev { get; }
        private DiffFunc<T> Diff { get; }

        public PostgresqlQueryable<T> AddDiff(DiffFunc<T> diff)
        {
            return new PostgresqlQueryable<T>(diff, this);
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
            var diffStack = new Stack<PostgresqlQueryable<T>>();
            var diff = this;
            do
            {
                diffStack.Push(diff);
                diff = diff.Prev;
            } while (diff != null);

            PostgresqlQueryableInfo<T> info = null;
            while (diffStack.TryPop(out var currentDiff)) info = currentDiff.Diff(info);

            return info;
        }
    }
}