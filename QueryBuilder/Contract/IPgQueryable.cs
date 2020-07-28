using System;

namespace QueryBuilder.Contract
{
    public interface IPgQueryable<out T>
    {
        PgQueryNode Node { get; }

        Type ElementType { get; }

        IPgQueryProvider Provider { get; }
    }
}