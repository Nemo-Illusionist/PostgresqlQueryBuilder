using System;

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


    public interface IPgJoin<out T1, out T2>
    {
        T1 From { get; }
        T2 Join1 { get; }
    }
}