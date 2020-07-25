using NUnit.Framework;
using QueryBuilder.Extension;
using QueryBuilder.UnitTest.Entities;

namespace QueryBuilder.UnitTest
{
    public class SelectTest
    {
        private PostgresqlQueryBuilder _queryBuilder;

        [SetUp]
        public void Setup()
        {
            _queryBuilder = new PostgresqlQueryBuilder();
        }

        [Test]
        public void FromSelect()
        {
            var queryString = _queryBuilder.From<Person>().ToQueryString();
            const string queryResult = @"SELEC p.""Id"" AS ""Id"", p.""Name"" AS ""Name"" FROM ""Persone"" AS p";

            Assert.AreEqual(queryResult, queryString);
        }

        [Test]
        public void Select()
        {
            var queryString = _queryBuilder.From<Person>()
                .Select(x => new {id = x.Id, name = x.Name})
                .ToQueryString();
            const string queryResult = @"SELEC p.""Id"" AS ""id"", p.""Name"" AS ""name"" FROM ""Persone"" AS p";

            Assert.AreEqual(queryResult, queryString);
        }

        [Test]
        public void SelectField()
        {
            var queryString = _queryBuilder.From<Person>()
                .Select(x => x.Id)
                .ToQueryString();
            const string queryResult = @"SELEC p.""Id"" AS ""Id"" FROM ""Persone"" AS p";

            Assert.AreEqual(queryResult, queryString);
        }

        [Test]
        public void SelectDistinct()
        {
            var queryString = _queryBuilder.From<Person>()
                .SelectDistinct(x => new {name = x.Name})
                .ToQueryString();
            const string queryResult = @"SELEC DISTINCT p.""Name"" AS ""name"" FROM ""Persone"" AS p";

            Assert.AreEqual(queryResult, queryString);
        }

        [Test]
        public void SelectDistinctOn()
        {
            var queryString = _queryBuilder.From<Person>()
                .SelectDistinct(x => new {id = x.Id, name = x.Name}, x => x.name)
                .ToQueryString();
            const string queryResult = @"SELEC DISTINCT ON (p.""Name"") p.""Id"" AS ""Id"", p.""Name"" AS ""Name"" FROM ""Persone"" AS p";

            Assert.AreEqual(queryResult, queryString);
        }
    }
}