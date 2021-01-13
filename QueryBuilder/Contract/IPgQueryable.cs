using System;
using QueryBuilder.Entities;

namespace QueryBuilder.Contract
{
    // ReSharper disable once UnusedTypeParameter
    public interface IPgQueryable<out T>
    {
        PgQueryNode Node { get; }

        Type ElementType { get; }

        IPgQueryProvider Provider { get; }
    }

    public interface IPgFromQueryable<out T> : IPgQueryable<T>
    {
    }

    public interface IPgGroupQueryable<out T> : IPgQueryable<T>
    {
    }
}