using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace QueryBuilder.Select
{
    public class SelectQueryBuilder
    {
        public string Gen<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            var selects = Pars(expression);
            return Convert(selects);
        }

        private static Span<Select> Pars<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            switch (expression.Body)
            {
                case NewExpression newExpression:
                    return ParsNewExpression(newExpression);
                default:
                    throw new NotImplementedException();
            }
        }

        private static Span<Select> ParsNewExpression(NewExpression expression)
        {
            var count = expression.Arguments.Count;
            var selects = new Select[count];
            for (var i = 0; i < count; i++)
            {
                selects[i] = ParsNewExpressionArgument(expression.Arguments[i], expression.Members[i]);
            }

            return selects.AsSpan();
        }

        private static Select ParsNewExpressionArgument(Expression arg, MemberInfo member)
        {
            switch (arg)
            {
                case MemberExpression memberExpression:
                    return new Select(memberExpression.Member.Name, member.Name);
                case MethodCallExpression methodCallExpression:
                    var memberName = ((MemberExpression) methodCallExpression.Arguments[0]).Member.Name;
                    return new Select(memberName, member.Name) {FuncTemplate = methodCallExpression.Method.Name};
                default:
                    throw new NotImplementedException();
            }
        }

        private static string Convert(Span<Select> selects)
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