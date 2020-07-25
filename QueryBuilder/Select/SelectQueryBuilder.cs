using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace QueryBuilder.Select
{
    public class SelectQueryBuilder
    {
        public string Gen<T, TResult>(Expression<Func<T, TResult>> expression, bool isDistinct = false)
        {
            var selects = Pars(expression);
            return Convert(selects);
        }

        private static Span<SelectElement> Pars<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            switch (expression.Body)
            {
                case NewExpression newExpression:
                    return ParsNewExpression(newExpression);
                case ParameterExpression parameterExpression:
                    return ParsParameterExpression(parameterExpression);
                default:
                    throw new NotImplementedException();
            }
        }

        private static Span<SelectElement> ParsParameterExpression(ParameterExpression expression)
        {
            throw new NotImplementedException();
        }

        private static Span<SelectElement> ParsNewExpression(NewExpression expression)
        {
            var count = expression.Arguments.Count;
            var selects = new SelectElement[count];
            for (var i = 0; i < count; i++)
            {
                selects[i] = ParsNewExpressionArgument(expression.Arguments[i], expression.Members[i]);
            }

            return selects.AsSpan();
        }

        private static SelectElement ParsNewExpressionArgument(Expression arg, MemberInfo member)
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

        private static string Convert(Span<SelectElement> selects)
        {
            var builder = new StringBuilder("SELECT ");
            for (int i = 0; i < selects.Length; i++)
            {
                throw new NotImplementedException();
                // builder.Append(s)
            }

            return builder.ToString();
        }
    }
}