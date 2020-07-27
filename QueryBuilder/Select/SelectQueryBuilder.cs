using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryBuilder.Select
{
    public class SelectQueryBuilder
    {
        public Select Parse<T, TResult>(Expression<Func<T, TResult>> expression, bool isDistinct)
        {
            var selects = Parse(expression);
            return new Select(Array.AsReadOnly(selects.ToArray()), isDistinct);
        }

        public Select Parse<T, TResult, TDistinct>(
            Expression<Func<T, TResult>> expression,
            Expression<Func<TResult, TDistinct>> distinctExpression)
        {
            var selects = Parse(expression);
            var selectsDistinct = Parse(distinctExpression);
            var selectElements = selects.ToArray()
                .GroupJoin(selectsDistinct.ToArray(), x => x, x => x,
                    (s, d) =>
                    {
                        s.IsDistinct = d.Any();
                        return s;
                    }
                );
            return new Select(selectElements.ToList().AsReadOnly());
        }

        private static Span<SelectElement> Parse<T, TResult>(Expression<Func<T, TResult>> expression)
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
            return new[] {new SelectElement(memberName, memberName)};
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
                    return new SelectElement(memberExpression.Member.Name, member.Name);
                case MethodCallExpression methodCallExpression:
                    var memberName = ((MemberExpression) methodCallExpression.Arguments[0]).Member.Name;
                    return new SelectElement(memberName, member.Name) {FuncTemplate = methodCallExpression.Method.Name};
                default:
                    throw new NotImplementedException();
            }
        }
    }
}