using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QueryBuilder.Entities;

namespace QueryBuilder.Builders
{
    public class QueryInfoBuilder
    {
        private readonly ColumnElementBuilder _columnElementBuilder;

        public QueryInfoBuilder()
        {
            _columnElementBuilder = new ColumnElementBuilder();
        }

        public QueryInfo Build<T>(PostgresqlQueryableInfo<T> postgresqlQueryableInfo)
        {
            var (tableName, tableAlias) = TableNameAndAlias(typeof(T));

            return new QueryInfo
            {
                TableName = tableName,
                TableAlias = tableAlias,
                Selects = Selects(postgresqlQueryableInfo.SelectExpression).ToList(),
                Distincts = Distincts(postgresqlQueryableInfo.DistinctExpression).ToList(),
                IsDistinct = postgresqlQueryableInfo.IsDistinct
            };
        }

        private (string, string) TableNameAndAlias(Type type)
        {
            return (type.Name, type.Name.First().ToString().ToLowerInvariant());
        }

        private IEnumerable<ColumnInfo> Selects<T>(Expression<Func<T, object>> selectExpression)
        {
            if (selectExpression != null) return _columnElementBuilder.Build(selectExpression).ToList();

            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(x => x.Name)
                .Select(x => new ColumnInfo(x));
        }

        private IEnumerable<DistinctInfo> Distincts<T>(Expression<Func<T, object>> distinctExpression)
        {
            if (distinctExpression == null) return Enumerable.Empty<DistinctInfo>();

            return _columnElementBuilder.Build(distinctExpression)
                .Select(x => new DistinctInfo {ColumnName = x.ColumnName});
        }
    }
}