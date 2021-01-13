using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using QueryBuilder.Entities;
using QueryBuilder.Extension;
using QueryBuilder.Extension.Queryable;
using QueryBuilder.Helpers;
using QueryBuilder.Provider;

namespace QueryBuilder.Generator
{
    internal class PgQueryFromAndJoinGenerator
    {
        private static Dictionary<string, string> _joinNameMapper = new()
        {
            {nameof(PgQueryBuilder.From), "FROM"},
            {nameof(PgQueryableJoinExtension.Join), "JOIN"},
            {nameof(PgQueryableJoinExtension.CrossJoin), "CROSS JOIN"},
            {nameof(PgQueryableJoinExtension.FullJoin), "FULL JOIN"},
            {nameof(PgQueryableJoinExtension.LeftJoin), "LEFT JOIN"},
            {nameof(PgQueryableJoinExtension.RightJoin), "RIGHT JOIN"},
        };
        
        public FromAndJoinGenReturn Execute(IEnumerable<PgQueryNode> nodes)
        {
            var joinQuery = new StringBuilder();
            var paramsList = new List<TableAlias>();
            foreach (var current in nodes)
            {
                AddFromAndJoin(joinQuery, current, paramsList);
            }

            return new FromAndJoinGenReturn(paramsList, joinQuery);
        }
        
        private static void AddFromAndJoin(StringBuilder query, PgQueryNode node, ICollection<TableAlias> paramsList)
        {
            var names = GetJoinTableNameAndAlias(node.Type);
            paramsList.Add(names);
            query.Append(_joinNameMapper[node.Method]);
            query.Append(" \"");
            query.Append(names.TableName);
            query.Append("\" AS ");
            query.Append(names.Alias);

            if (node.Expressions.Count == 1)
            {
                query.Append(" ON ");
                query.Append(GetJoinWhere(node.Expressions[0], paramsList));
            }

            query.AddSpace();
        }
        
        private static TableAlias GetJoinTableNameAndAlias(Type type)
        {
            string tableName;
            string alias;
            if (type.IsGenericType)
            {
                var arguments = type.GetGenericArguments();
                tableName = arguments.Last().Name;
                alias = $"_t{arguments.Length - 1}";
            }
            else
            {
                tableName = type.Name;
                alias = "_t0";
            }

            return new TableAlias(tableName, alias);
        }

        private static StringBuilder GetJoinWhere(LambdaExpression expression, IEnumerable<TableAlias> paramsList)
        {
            var paramMap = expression.Parameters.Zip(paramsList)
                .DistinctBy(x=>x.First.Name)
                .ToDictionary(x => x.First.Name, x => x.Second.Alias);
            var query = new StringBuilder();
            GetJoinWhere_Rec(expression.Body, paramMap, query);
            query.TrimEnd();
            return query;
        }

        private static void GetJoinWhere_Rec(
            Expression expression,
            IReadOnlyDictionary<string, string> paramMap,
            StringBuilder query)
        {
            switch (expression)
            {
                case BinaryExpression ex:
                    GetJoinWhere_Rec(ex.Left, paramMap, query);
                    query.AddSpace();
                    query.Append(BinaryOperatorMapper.Map(ex.NodeType));
                    query.AddSpace();
                    GetJoinWhere_Rec(ex.Right, paramMap, query);
                    break;
                case MethodCallExpression ex:
                    break;
                case MemberExpression ex:
                    switch (ex.Expression)
                    {
                        case ParameterExpression pex:
                            query.Append(paramMap[pex.Name]);
                            query.Append('.');
                            break;
                    }

                    query.AddInQuotes(ex.Member.Name);
                    break;
            }
        }
    }
}