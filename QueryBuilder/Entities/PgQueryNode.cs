using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace QueryBuilder.Entities
{
    public class PgQueryNode : IEnumerable<PgQueryNode>
    {
        public PgQueryNode PreviousNode { get; }

        public Type Type { get; }

        public string Method { get; }

        public ReadOnlyCollection<LambdaExpression> Expressions { get; }

        public PgQueryNode(string method,
            Type type,
            PgQueryNode previousNode = null,
            params LambdaExpression[] expressions)
        {
            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(method));
            }

            Type = type ?? throw new ArgumentNullException(nameof(type));
            Method = method;
            Expressions = new ReadOnlyCollection<LambdaExpression>(expressions);
            PreviousNode = previousNode;
        }

        public IEnumerator<PgQueryNode> GetEnumerator()
        {
            return new PgQueryNodeEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class PgQueryNodeEnumerator : IEnumerator<PgQueryNode>
        {
            public PgQueryNode Current { get; private set; }
            object IEnumerator.Current => Current;

            private readonly PgQueryNode _node;

            public PgQueryNodeEnumerator(PgQueryNode node)
            {
                _node = node;
                Current = null;
            }

            public bool MoveNext()
            {
                var localNode = Current;
                if (localNode == null)
                {
                    Current = _node;
                    return true;
                }
                else if (localNode.PreviousNode != null)
                {
                    Current = localNode.PreviousNode;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
                Current = null;
            }

            public void Dispose()
            {
            }
        }
    }
}