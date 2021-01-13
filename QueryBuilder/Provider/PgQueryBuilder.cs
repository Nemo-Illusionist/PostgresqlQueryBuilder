using System;
using QueryBuilder.Contract;
using QueryBuilder.Entities;
using QueryBuilder.Generator;

namespace QueryBuilder.Provider
{
    public class PgQueryBuilder : IPgQueryProvider
    {
        private readonly IPgQueryGenerator _queryGenerator;

        public PgQueryBuilder()
        {
            _queryGenerator = new PgQueryGenerator();
        }

        public IPgFromQueryable<T> From<T>()
        {
            var pgQueryNode = new PgQueryNode(nameof(From), typeof(T));
            var queryable = ((IPgQueryProvider) this).CreateQuery<T>(pgQueryNode);
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