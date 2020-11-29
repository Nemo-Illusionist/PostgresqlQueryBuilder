using System;
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

        public IPgFromQueryable<T> From<T>()
        {
            var queryable = ((IPgQueryProvider) this).CreateQuery<T>(new PgQueryNode(nameof(From), typeof(T)));
            return (IPgFromQueryable<T>) queryable;
        }

        IPgQueryable<T> IPgQueryProvider.CreateQuery<T>(PgQueryNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return new PgQueryable<T>(node, this);
        }

        string IPgQueryProvider.Execute(PgQueryNode node)
        {
            return _queryGenerator.Execute(node);
        }
    }
}