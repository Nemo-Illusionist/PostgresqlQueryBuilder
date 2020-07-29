using QueryBuilder.Contract;

namespace QueryBuilder.Provider
{
    public class PgQueryBuilder : IPgQueryProvider
    {
        private readonly PgQueryGenerator _queryGenerator;

        public PgQueryBuilder()
        {
            _queryGenerator = new PgQueryGenerator();
        }

        public IPgQueryable<T> From<T>()
        {
            return ((IPgQueryProvider) this).CreateQuery<T>(new PgQueryNode(nameof(From)));
        }

        IPgQueryable<T> IPgQueryProvider.CreateQuery<T>(PgQueryNode node)
        {
            return new PgQueryable<T>(node, this);
        }

        string IPgQueryProvider.Execute(PgQueryNode node)
        {
            return _queryGenerator.Execute(node);
        }
    }
}