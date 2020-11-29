using System;
using System.Linq.Expressions;
using System.Text;
using QueryBuilder.Exception;
using QueryBuilder.Extension;
using QueryBuilder.Extension.Queryable;
using QueryBuilder.SelectBuilder;

namespace QueryBuilder.Provider
{
    public class PgQueryGenerator
    {
        public string Execute(PgQueryNode pgNode)
        {
            if (pgNode == null)
            {
                throw new ArgumentNullException(nameof(pgNode));
            }

            var selectQueryBuilder = new SelectQueryBuilder();
            var query = new StringBuilder();
            var node = pgNode;

            if (node.Method == nameof(PgQueryableSelectExtension.Select) ||
                node.Method == nameof(PgQueryableSelectExtension.SelectDistinct) ||
                node.Method == nameof(PgQueryableSelectExtension.SelectDistinctOn))
            {
                query.Append(selectQueryBuilder.Build(node));
                node = node.PreviousNode;
            }
            else if (node.Method == nameof(PgQueryBuilder.From))
            {
                var parameter = Expression.Parameter(node.Type, "x");
                var lambdaExpression = Expression.Lambda(parameter, parameter);
                var pgQueryNode = new PgQueryNode(nameof(PgQueryableSelectExtension.Select), node.Type, null,
                    lambdaExpression);
                query.Append(selectQueryBuilder.Build(pgQueryNode));
            }
            else
            {
                throw new OutOfSequenceException();
            }

            if (node?.Method == nameof(PgQueryBuilder.From))
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