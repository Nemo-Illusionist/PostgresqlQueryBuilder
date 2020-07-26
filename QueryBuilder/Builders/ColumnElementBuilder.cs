using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using QueryBuilder.Entities;

namespace QueryBuilder.Builders
{
    public class ColumnElementBuilder
    {
        public IEnumerable<ColumnInfo> Build<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            return Parse(expression);
        }

        private static IEnumerable<ColumnInfo> Parse<T, TResult>(Expression<Func<T, TResult>> expression)
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

        private static IEnumerable<ColumnInfo> ParseUnaryExpression(UnaryExpression unaryExpression)
        {
            return ParseMemberExpression(unaryExpression.Operand as MemberExpression);
        }

        private static IEnumerable<ColumnInfo> ParseMemberExpression(MemberExpression memberExpression)
        {
            var selects = new[] {memberExpression.Member.Name};
            return selects.Select(x => new ColumnInfo(x, x));
        }

        private static IEnumerable<ColumnInfo> ParseParameterExpression(ParameterExpression expression)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<ColumnInfo> ParseNewExpression(NewExpression expression)
        {
            var count = expression.Arguments.Count;
            var selects = new ColumnInfo[count];
            for (var i = 0; i < count; i++) selects[i] = ParseNewExpressionArgument(expression.Arguments[i], expression.Members[i]);

            return selects;
        }

        private static ColumnInfo ParseNewExpressionArgument(Expression arg, MemberInfo member)
        {
            return arg switch
            {
                MemberExpression memberExpression => new ColumnInfo(memberExpression.Member.Name, member.Name),
                _ => throw new NotImplementedException()
            };
        }
    }
}