using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QueryBuilder.Contract;
using QueryBuilder.Select;

namespace QueryBuilder
{
    public class PostgresqlQueryable<T> : IPostgresqlQueryable<T>
    {
        public string ToQueryString()
        {
            var selects = Selects(SelectExpression);
            var (tableName, tableAlias) = TableNameAndAlias(typeof(T));

            var selectStatements = selects.Select(x => $@"{tableAlias}.""{x.FieldName}"" AS ""{x.AsName}""");
            var selectString = string.Join(" ", selectStatements);

            return $@"SELECT {selectString} FROM ""{tableName}"" AS {tableAlias}";
        }

        private (string, string) TableNameAndAlias(Type type)
        {
            return (type.Name, type.Name.First().ToString().ToLowerInvariant());
        }

        private IEnumerable<SelectElement> Selects(Expression<Func<T, object>> selectExpression)
        {
            if (selectExpression == null)
            {
                return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Select(x => x.Name)
                    .Select(x => new SelectElement(x, x));
            }

            var selectQueryBuilder = new SelectQueryBuilder();
            var selects = selectQueryBuilder.Gen(selectExpression);
            return selects.ToArray();
        }

        public List<Expression> WhereExpressions { get; set; }
        public Expression<Func<T, object>> SelectExpression { get; set; }
        public Expression<Func<T, object>> DistinctSelectExpression { get; set; }
        public bool IsDistinct { get; set; }
    }
}