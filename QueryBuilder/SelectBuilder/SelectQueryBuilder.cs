using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QueryBuilder.Contract;
using QueryBuilder.Exception;
using QueryBuilder.Extension.Queryable;

namespace QueryBuilder.SelectBuilder
{
    internal class SelectQueryBuilder : IPgElementQueryBuilder
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
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var select = node.Method switch
            {
                nameof(PgQueryableSelectExtension.Select) => _selectQueryParser.Parse(node.Expressions[0], false),
                nameof(PgQueryableSelectExtension.SelectDistinct) => _selectQueryParser.Parse(node.Expressions[0], true),
                nameof(PgQueryableSelectExtension.SelectDistinctOn) => _selectQueryParser.Parse(node.Expressions[0], node.Expressions[1]),
                _ => throw new OutOfSequenceException()
            };

            return ToSql(select);
        }

        private static StringBuilder ToSql(SelectInfo selectInfo)
        {
            if (selectInfo.IsDistinct && selectInfo.DistinctElements != null)
            {
                throw new System.Exception("select invalid");
            }

            var query = new StringBuilder("SELECT ");
            if (selectInfo.IsDistinct)
            {
                query.Append("DISTINCT ");
            }
            else if (selectInfo.DistinctElements != null)
            {
                query.Append($"DISTINCT ON ({GenDistinctOn(selectInfo.DistinctElements)}) ");
            }

            query.Append(GenSelectFields(selectInfo.Elements));
            query.Append(' ');
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