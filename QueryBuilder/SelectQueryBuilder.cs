using System;
using System.Linq;
using System.Linq.Expressions;

namespace QueryBuilder
{
    public class SelectQueryBuilder
    {
        public string Gen<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            var parameterExpression = (ParameterExpression) expression.Parameters[0];
            var newExpression = (NewExpression) expression.Body;

            for (int i = 0; i < newExpression.Arguments.Count; i++)
            {
                switch (newExpression.Arguments[i])
                {
                    case MemberExpression memberExpression:
                        var arg = memberExpression;
                        var mem = newExpression.Members[i];
                        Console.WriteLine("{0} as {1}", arg.Member.Name, mem.Name);
                        break;
                    case MethodCallExpression methodCallExpression:
                        var arg1 = methodCallExpression;
                        var mem1 = newExpression.Members[i];
                        var argName = ((MemberExpression) arg1.Arguments[0]).Member.Name;
                        Console.WriteLine("{0}({1}) as {2}", arg1.Method.Name, argName, mem1.Name);
                        break;
                }

                // var arg = (MemberExpression) newExpression.Arguments[i];
                // var mem = newExpression.Members[i];
                // Console.WriteLine("{0} as {1}", arg.Member.Name, mem.Name);
            }

            throw new NotImplementedException();
        }
    }
}