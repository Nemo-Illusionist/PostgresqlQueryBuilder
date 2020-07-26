using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using QueryBuilder.Contract;
using QueryBuilder.Select;

namespace QueryBuilder
{
    public class PostgresqlQueryable<T> : IPostgresqlQueryable<T>
    {
        public string ToQueryString()
        {
            var (tableName, tableAlias) = TableNameAndAlias(typeof(T));

            var selectString = SelectString(tableAlias);
            var distinct = Distinct(tableAlias);
            var sql = $@"SELECT {distinct} {selectString} FROM ""{tableName}"" AS {tableAlias}";
            return RemoveMultipleSpaces(sql);
        }

        private string SelectString(string tableAlias)
        {
            var selects = ParseExpression(SelectExpression);
            var selectStatements = selects.Select(x => $@"{tableAlias}.""{x.FieldName}"" AS ""{x.AsName}""");
            var selectString = string.Join(", ", selectStatements);
            return selectString;
        }

        private string Distinct(string tableAlias)
        {
            if (!IsDistinct) return string.Empty;
            if (DistinctSelectExpression == null) return "DISTINCT";
            
            var distinctColumns = ParseExpression(DistinctSelectExpression).ToArray();

            var distinctStatements = distinctColumns.Select(x => $@"{tableAlias}.""{x.FieldName}""");
            return $"DISTINCT ON ({string.Join(", ", distinctStatements)})";
        }

        private static string RemoveMultipleSpaces(string sql)
        {
            return Regex.Replace(sql, @"\s+", " ");
        }

        private (string, string) TableNameAndAlias(Type type)
        {
            return (type.Name, type.Name.First().ToString().ToLowerInvariant());
        }

        private IEnumerable<SelectElement> ParseExpression(Expression<Func<T, object>> selectExpression)
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