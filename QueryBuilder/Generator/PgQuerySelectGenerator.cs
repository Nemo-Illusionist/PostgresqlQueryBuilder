using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using QueryBuilder.Contract;
using QueryBuilder.Entities;
using QueryBuilder.Extension.Queryable;

namespace QueryBuilder.Generator
{
    internal class PgQuerySelectGenerator
    {
        private readonly PgQueryNode _node;
        private readonly IPgQueryNameProvider _nameProvider;

        private record SelectElement(string Sql, string As);

        public PgQuerySelectGenerator(PgQueryNode node, IPgQueryNameProvider nameProvider)
        {
            _node = node ?? throw new ArgumentNullException(nameof(node));
            _nameProvider = nameProvider ?? throw new ArgumentNullException(nameof(nameProvider));
        }

        public StringBuilder Execute()
        {
            var query = new StringBuilder("SELECT ");

            var isDistinct = _node.Method == nameof(PgQueryableSelectExtension.SelectDistinct)
                             || _node.Method == nameof(PgQueryableSelectExtension.SelectDistinctOn);
            if (isDistinct)
            {
                query.Append("DISTINCT ");
            }

            if (_node.Expressions.Count == 2)
            {
                var distinctOnElements = Parse(_node.Expressions[1]);
                query.Append($"ON ({string.Join(", ", distinctOnElements.Select(x => x.Sql))}) ");
            }

            var elements = Parse(_node.Expressions[0]);
            query.Append(string.Join(", ", elements.Select(x => $"{x.Sql} AS \"{x.As}\"")));
            query.Append(' ');
            return query;
        }

        private SelectElement[] Parse(LambdaExpression expression)
        {
            return expression.Body switch
            {
                NewExpression newExpression => ParseNewExpression(newExpression),
                ParameterExpression parameterExpression => ParseParameterExpression(parameterExpression),
                MemberExpression memberExpression => ParseMemberExpression(memberExpression),
                UnaryExpression unaryExpression => ParseUnaryExpression(unaryExpression),
                _ => throw new NotImplementedException()
            };
        }

        private SelectElement[] ParseUnaryExpression(UnaryExpression unaryExpression)
        {
            return ParseMemberExpression((unaryExpression.Operand as MemberExpression)!);
        }

        private SelectElement[] ParseMemberExpression(MemberExpression memberExpression)
        {
            var memberName = memberExpression.Member.Name;
            var tableName = _nameProvider.GetTableName(memberExpression);
            return new[] {new SelectElement($"{tableName}.\"{memberName}\"", memberName)};
        }

        private SelectElement[] ParseParameterExpression(ParameterExpression expression)
        {
            SelectElement[] selectElements;
            var propertyInfos = expression.Type.GetProperties();
            if (expression.Type.IsGenericType)
            {
                selectElements = propertyInfos
                    .Select(x => new {x.Name, Type = x.PropertyType})
                    .SelectMany(x => x.Type.GetProperties(), 
                        (x, pi) => new
                        {
                            TableName = _nameProvider.GetTableName($"{expression}.{x.Name}"), 
                            PropertyName = pi.Name
                        })
                    .Select(x => new SelectElement($"{x.TableName}.\"{x.PropertyName}\"", x.PropertyName))
                    .ToArray();
            }
            else
            {
                var tableName = _nameProvider.GetTableName(expression);
                selectElements = propertyInfos
                    .Select(x => new SelectElement($"{tableName}.\"{x.Name}\"", x.Name))
                    .ToArray();
            }

            return selectElements;
        }

        private SelectElement[] ParseNewExpression(NewExpression expression)
        {
            var count = expression.Arguments.Count;
            var selects = new SelectElement[count];
            for (var i = 0; i < count; i++)
            {
                selects[i] = ParseNewExpressionArgument(expression.Arguments[i], expression.Members[i]);
            }

            return selects;
        }

        private SelectElement ParseNewExpressionArgument(Expression arg, MemberInfo member)
        {
            switch (arg)
            {
                case MemberExpression memberExpression:
                    var tableName = _nameProvider.GetTableName(memberExpression);
                    return new SelectElement($"{tableName}.\"{memberExpression.Member.Name}\"", member.Name);
                case MethodCallExpression methodCallExpression:
                    var memExpression = (MemberExpression) methodCallExpression.Arguments[0];
                    var memberName = memExpression.Member.Name;
                    return new SelectElement(memberName, member.Name);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}