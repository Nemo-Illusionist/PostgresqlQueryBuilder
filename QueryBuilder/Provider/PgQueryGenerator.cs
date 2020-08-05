using System;
using System.Linq.Expressions;
using System.Text;
using QueryBuilder.Exception;
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
            else if (node.Method == nameof(PgQueryBuilder.From))
            {
                var parameter = Expression.Parameter(node.Type, "x");
                var lambdaExpression = Expression.Lambda(parameter, parameter);
                var pgQueryNode = new PgQueryNode(nameof(PgQueryableExtension.Select), node.Type, null, lambdaExpression);
                query.Append(selectQueryBuilder.Build(pgQueryNode));
            }
            else
            {
                throw new OutOfSequenceException();
            }

            if (node.Method == nameof(PgQueryBuilder.From))
            {
                query.Append($@"FROM ""{node.Type.Name}"" AS _t1 ");
            }
            else
            {
                throw new OutOfSequenceException();
            }

            return query.TrimEnd().ToString();
        }
    }
}