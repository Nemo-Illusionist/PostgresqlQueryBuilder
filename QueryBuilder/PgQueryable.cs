using System;
using QueryBuilder.Contract;

namespace QueryBuilder
{
    public class PgQueryable<T> : IPgFromQueryable<T>, IPgGroupQueryable<T>
    {
        public PgQueryNode Node { get; }

        public Type ElementType { get; }

        public IPgQueryProvider Provider { get; }

        public PgQueryable(PgQueryNode node, IPgQueryProvider provider)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            ElementType = typeof(T);
        }
    }
}