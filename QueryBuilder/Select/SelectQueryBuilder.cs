using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QueryBuilder.Contract;
using QueryBuilder.Exception;
using QueryBuilder.Extension;

namespace QueryBuilder.Select
{
    public class SelectQueryBuilder : IPgElementQueryBuilder
    {
        private readonly SelectQueryParser _selectQueryParser;

        public SelectQueryBuilder()
        {
            _selectQueryParser = new SelectQueryParser();
        }

        public StringBuilder Build()
        {
            return new StringBuilder("SELECT * ");
        }

        public StringBuilder Build(PgQueryNode node)
        {
            var select = node.Method switch
            {
                nameof(PgQueryableExtension.Select) => _selectQueryParser.Parse(node.Expressions[0], false),
                nameof(PgQueryableExtension.SelectDistinct) => _selectQueryParser.Parse(node.Expressions[0], true),
                nameof(PgQueryableExtension.SelectDistinctOn) => _selectQueryParser.Parse(node.Expressions[0],
                    node.Expressions[1]),
                _ => throw new OutOfSequenceException()
            };

            return ToSql(select);
        }

        private static StringBuilder ToSql(Select select)
        {
            if (select == null) throw new ArgumentNullException(nameof(select));
            if (select.IsDistinct && select.DistinctElements != null) throw new System.Exception("select invalid");
            var query = new StringBuilder("SELECT ");
            if (select.IsDistinct)
            {
                query.Append("DISTINCT ");
            }
            else if (select.DistinctElements != null)
            {
                query.Append($"DISTINCT ON ({GenDistinctOn(select.DistinctElements)}) ");
            }

            query.Append(GenSelectFields(select.Elements));
            query.Append(" ");
            return query;
        }

        private static string GenDistinctOn(IEnumerable<SelectElement> elements)
        {
            return string.Join(", ", elements.Select(x => $"{x.TableHint}.\"{x.FieldName}\""));
        }

        private static string GenSelectFields(IEnumerable<SelectElement> elements)
        {
            return string.Join(", ", elements.Select(x => $"{x.TableHint}.\"{x.FieldName}\" AS \"{x.AsName}\""));
        }
    }
}