using System;
using System.Linq.Expressions;

namespace QueryBuilder.Entities
{
    public class PostgresqlQueryableInfo<T>
    {
        public Expression<Func<T, object>> SelectExpression { get; set; }
        public Expression<Func<T, object>> DistinctExpression { get; set; }
        public bool IsDistinct { get; set; }
    }
}