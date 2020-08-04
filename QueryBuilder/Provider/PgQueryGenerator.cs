using System;
using System.Text;
using QueryBuilder.Extension;
using QueryBuilder.Select;

namespace QueryBuilder.Provider
{
    public class PgQueryGenerator
    {
        public string Execute(PgQueryNode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            var selectQueryBuilder = new SelectQueryBuilder();
            var query = new StringBuilder();

            if (node.Method == nameof(PgQueryableExtension.Select) ||
                node.Method == nameof(PgQueryableExtension.SelectDistinct) ||
                node.Method == nameof(PgQueryableExtension.SelectDistinctOn))
            {
                query.Append(selectQueryBuilder.Build(node));
                node = node.PreviousNode;
            }
            else
            {
                query.Append(selectQueryBuilder.Build());
            }

            if (node.Method == nameof(PgQueryBuilder.From))
            {
                query.Append($@"FROM ""{node.Type.Name}"" AS _t1 ");
            }
            else
            {
                throw new Exception("Out of sequence");
            }

            return query.TrimEnd().ToString();
        }
    }
}