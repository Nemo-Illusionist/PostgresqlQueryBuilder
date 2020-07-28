namespace QueryBuilder.Contract
{
    public interface IPgQueryProvider
    {
        IPgQueryable<TElement> CreateQuery<TElement>(PgQueryNode node);
    }
}