using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using QueryBuilder.Contract;
using QueryBuilder.Entities;
using QueryBuilder.Extension;
using QueryBuilder.Extension.Queryable;
using QueryBuilder.Provider;

namespace QueryBuilder.Generator
{
    internal class PgQueryGenerator : IPgQueryGenerator
    {
        public string Execute(PgQueryNode pgNode)
        {
            if (pgNode == null)
            {
                throw new ArgumentNullException(nameof(pgNode));
            }

            var query = new StringBuilder();

            PgQueryNode selectNode = null;
            var fromOrJoinNods = new Stack<PgQueryNode>();

            var node = pgNode;
            do
            {
                if (node.IsJoin() || node.IsFrom())
                {
                    fromOrJoinNods.Push(node);
                }
                else if (node.IsSelect())
                {
                    selectNode = node;
                }

                node = node.PreviousNode;
            } while (node != null);

            var fromAndJoinGenerator = new PgQueryFromAndJoinGenerator();
            var (tableAliases, joinQuery) = fromAndJoinGenerator.Execute(fromOrJoinNods);

            if (selectNode == null)
            {
                var type = fromOrJoinNods.Last().Type;
                var parameter = Expression.Parameter(type, "x");
                var lambda = Expression.Lambda(parameter, parameter);
                selectNode = new PgQueryNode(nameof(PgQueryableSelectExtension.Select), type, null, lambda);
            }

            var nameProvider = new PgQueryNameProvider(tableAliases, selectNode);
            var selectGenerator = new PgQuerySelectGenerator(selectNode, nameProvider);

            query.Append(selectGenerator.Execute());
            query.Append(joinQuery);

            query.TrimEnd();
            return query.ToString();
        }
    }
}