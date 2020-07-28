using System;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryBuilder.Select
{
    public class SelectQueryBuilder
    {
        public Select Parse(LambdaExpression expression, bool isDistinct)
        {
            var selects = Parse(expression);
            return new Select(Array.AsReadOnly(selects.ToArray()), isDistinct);
        }

        public Select Parse(LambdaExpression expression, LambdaExpression distinctExpression)
        {
            var selects = Parse(expression);
            var distinctSelects = Parse(distinctExpression);
            return new Select(Array.AsReadOnly(selects.ToArray()), Array.AsReadOnly(distinctSelects.ToArray()));
        }

        private static Span<SelectElement> Parse(LambdaExpression expression)
        {
            switch (expression.Body)
            {
                case NewExpression newExpression:
                    return ParseNewExpression(newExpression);
                case ParameterExpression parameterExpression:
                    return ParseParameterExpression(parameterExpression);
                case MemberExpression memberExpression:
                    return ParseMemberExpression(memberExpression);
                case UnaryExpression unaryExpression:
                    return ParseUnaryExpression(unaryExpression);
                default:
                    throw new NotImplementedException();
            }
        }

        private static Span<SelectElement> ParseUnaryExpression(UnaryExpression unaryExpression)
        {
            return ParseMemberExpression(unaryExpression.Operand as MemberExpression);
        }

        private static Span<SelectElement> ParseMemberExpression(MemberExpression memberExpression)
        {
            var memberName = memberExpression.Member.Name;
            return new[] {new SelectElement(memberName, memberName, memberExpression.Type)};
        }

        private static Span<SelectElement> ParseParameterExpression(ParameterExpression expression)
        {
            throw new NotImplementedException();
        }

        private static Span<SelectElement> ParseNewExpression(NewExpression expression)
        {
            var count = expression.Arguments.Count;
            var selects = new SelectElement[count];
            for (var i = 0; i < count; i++)
            {
                selects[i] = ParseNewExpressionArgument(expression.Arguments[i], expression.Members[i]);
            }

            return selects;
        }

        private static SelectElement ParseNewExpressionArgument(Expression arg, MemberInfo member)
        {
            switch (arg)
            {
                case MemberExpression memberExpression:
                    return new SelectElement(memberExpression.Member.Name, member.Name, memberExpression.Type);
                case MethodCallExpression methodCallExpression:
                    var memExpression = (MemberExpression) methodCallExpression.Arguments[0];
                    var memberName = memExpression.Member.Name;
                    return new SelectElement(memberName, member.Name, memExpression.Type)
                        {Method = methodCallExpression.Method};
                default:
                    throw new NotImplementedException();
            }
        }
    }
}