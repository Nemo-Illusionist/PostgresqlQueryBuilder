using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace QueryBuilder.Select
{
    public class SelectQueryBuilder
    {
        public IEnumerable<SelectElement> Gen<T, TResult>(Expression<Func<T, TResult>> expression, bool isDistinct = false)
        {
            return Parse(expression);
        }

        private static IEnumerable<SelectElement> Parse<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            switch (expression.Body)
            {
                case NewExpression newExpression:
                    return ParseNewExpression(newExpression);
                case ParameterExpression parameterExpression:
                    return ParseParameterExpression(parameterExpression);
                case UnaryExpression unaryExpression:
                    return ParseUnaryExpression(unaryExpression);
                default:
                    throw new NotImplementedException();
            }
        }

        private static IEnumerable<SelectElement> ParseUnaryExpression(UnaryExpression unaryExpression)
        {
            var selects = new[] {((MemberExpression)unaryExpression.Operand).Member.Name};
            return selects.Select(x => new SelectElement(x, x));
        }

        private static IEnumerable<SelectElement> ParseParameterExpression(ParameterExpression expression)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<SelectElement> ParseNewExpression(NewExpression expression)
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