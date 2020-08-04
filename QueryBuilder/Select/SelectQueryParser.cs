using System;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryBuilder.Select
{
    public class SelectQueryParser
    {
        private const string TableHint = "_t1";

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
            return expression.Body switch
            {
                NewExpression newExpression => ParseNewExpression(newExpression),
                ParameterExpression parameterExpression => ParseParameterExpression(parameterExpression),
                MemberExpression memberExpression => ParseMemberExpression(memberExpression),
                UnaryExpression unaryExpression => ParseUnaryExpression(unaryExpression),
                _ => throw new NotImplementedException()
            };
        }

        private static Span<SelectElement> ParseUnaryExpression(UnaryExpression unaryExpression)
        {
            return ParseMemberExpression(unaryExpression.Operand as MemberExpression);
        }

        private static Span<SelectElement> ParseMemberExpression(MemberExpression memberExpression)
        {
            var memberName = memberExpression.Member.Name;
            return new[] {new SelectElement(TableHint, memberName, memberExpression.Type)};
        }

        private static Span<SelectElement> ParseParameterExpression(ParameterExpression expression)
        {
            var propertyInfos = expression.Type.GetProperties();
            var length = propertyInfos.Length;
            var selectElements = new SelectElement[length];
            for (var i = 0; i < length; i++)
            {
                var propertyInfo = propertyInfos[i];
                selectElements[i] = new SelectElement(TableHint, propertyInfo.Name, propertyInfo.PropertyType);
            }

            return selectElements;
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
                    return new SelectElement(TableHint, memberExpression.Member.Name,
                        memberExpression.Type, member.Name);
                case MethodCallExpression methodCallExpression:
                    var memExpression = (MemberExpression) methodCallExpression.Arguments[0];
                    var memberName = memExpression.Member.Name;
                    return new SelectElement(TableHint, memberName, memExpression.Type, member.Name,
                        methodCallExpression.Method);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}