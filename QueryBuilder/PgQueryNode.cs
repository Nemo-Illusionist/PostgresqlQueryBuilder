using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QueryBuilder
{
    public class PgQueryNode
    {
        public PgQueryNode PreviousNode { get; }

        public string Method { get; }

        public IReadOnlyCollection<LambdaExpression> Expressions { get; }

        public PgQueryNode(
            string method,
            PgQueryNode previousNode = null,
            params LambdaExpression[] expressions)
        {
            if (string.IsNullOrEmpty(method))
                throw new ArgumentException("Value cannot be null or empty.", nameof(method));
            Method = method;

            Expressions = expressions;
            PreviousNode = previousNode;
        }
    }
}