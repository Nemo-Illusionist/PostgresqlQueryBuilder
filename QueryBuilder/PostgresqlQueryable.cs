using System;
using System.Linq.Expressions;
using QueryBuilder.Builders;

namespace QueryBuilder
{
    public class PostgresqlQueryable<T>
    {
        private readonly QueryInfoBuilder _queryInfoBuilder;
        private readonly QueryStringBuilder _queryStringBuilder;

        public PostgresqlQueryable()
        {
            _queryInfoBuilder = new QueryInfoBuilder();
            _queryStringBuilder = new QueryStringBuilder();
        }

        public Expression<Func<T, object>> SelectExpression { get; set; }
        public Expression<Func<T, object>> DistinctExpression { get; set; }
        public bool IsDistinct { get; set; }


        public string ToQueryString()
        {
            var queryInfo = _queryInfoBuilder.Build(this);
            var query = _queryStringBuilder.Build(queryInfo);
            return query;
        }
    }
}