using QueryBuilder.Contract;

namespace QueryBuilder
{
    public class PgQueryProvider : IPgQueryProvider
    {
        public IPgQueryable<T> CreateQuery<T>(PgQueryNode node)
        {
            return new PgQueryable<T>(node, this);
        }
    }
}