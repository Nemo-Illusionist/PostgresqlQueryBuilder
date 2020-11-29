using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace QueryBuilder
{
    public class PgQueryNode
    {
        public PgQueryNode PreviousNode { get; }

        public Type Type { get; }

        public string Method { get; }

        public ReadOnlyCollection<LambdaExpression> Expressions { get; }

        public PgQueryNode(
            string method,
            Type type,
            PgQueryNode previousNode = null,
            params LambdaExpression[] expressions)
        {
            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(method));
            }

            Method = method;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Expressions = new ReadOnlyCollection<LambdaExpression>(expressions);
            PreviousNode = previousNode;
        }
    }
}