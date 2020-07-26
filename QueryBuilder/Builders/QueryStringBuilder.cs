using System.Linq;
using System.Text.RegularExpressions;
using QueryBuilder.Entities;

namespace QueryBuilder.Builders
{
    public class QueryStringBuilder
    {
        public string Build(QueryInfo queryInfo)
        {
            var distinct = Distinct(queryInfo);
            var select = Select(queryInfo);
            var sql = $@"SELECT {distinct} {select} FROM ""{queryInfo.TableName}"" AS {queryInfo.TableAlias}";
            return RemoveMultipleSpaces(sql);
        }

        private string Select(QueryInfo queryInfo)
        {
            var selectColumns = queryInfo.Selects
                .Select(x => $@"{queryInfo.TableAlias}.""{x.ColumnName}"" AS ""{x.AsName}""");
            return string.Join(", ", selectColumns);
        }

        private string Distinct(QueryInfo queryInfo)
        {
            if (!queryInfo.IsDistinct) return string.Empty;
            if (!queryInfo.Distincts.Any()) return "DISTINCT";

            var distinctColumns = queryInfo.Distincts.Select(x => $@"{queryInfo.TableAlias}.""{x.ColumnName}""");
            return $"DISTINCT ON ({string.Join(", ", distinctColumns)})";
        }

        private static string RemoveMultipleSpaces(string sql)
        {
            return Regex.Replace(sql, @"\s+", " ");
        }
    }
}