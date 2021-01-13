using QueryBuilder.Entities;

namespace QueryBuilder.Contract
{
    public interface IPgQueryProvider
    {
        IPgQueryable<TElement> CreateQuery<TElement>(PgQueryNode node);
        
        string Execute(PgQueryNode node);
    }
}