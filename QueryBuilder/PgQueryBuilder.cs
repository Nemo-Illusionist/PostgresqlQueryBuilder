using QueryBuilder.Contract;

namespace QueryBuilder
{
    public class PgQueryBuilder
    {
        public virtual IPgQueryable<T> From<T>()
        {
            var provider = new PgQueryProvider();
            return provider.CreateQuery<T>(new PgQueryNode(nameof(From)));
        }
    }
}