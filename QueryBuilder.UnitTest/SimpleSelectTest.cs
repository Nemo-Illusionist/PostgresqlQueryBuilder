using NUnit.Framework;
using QueryBuilder.Extension.Queryable;
using QueryBuilder.Provider;
using QueryBuilder.UnitTest.Entities;

namespace QueryBuilder.UnitTest
{
    public class SimpleSelectTest
    {
        private PgQueryBuilder _queryBuilder;

        [SetUp]
        public void Setup()
        {
            _queryBuilder = new PgQueryBuilder();
        }

        [Test]
        public void FromSelect()
        {
            var queryString = _queryBuilder.From<User>().ToQueryString();
            var query1String = _queryBuilder.From<User>().Select(x => x).ToQueryString();
            var query2String = _queryBuilder.From<User>().Select(x => new {x.Id, x.Login}).ToQueryString();
            const string queryResult = @"SELECT _t0.""Id"" AS ""Id"", _t0.""Login"" AS ""Login"" FROM ""User"" AS _t0";

            Assert.AreEqual(queryResult, queryString);
            Assert.AreEqual(queryResult, query1String);
            Assert.AreEqual(queryResult, query2String);
        }

        [Test]
        public void Select()
        {
            var queryString = _queryBuilder.From<User>()
                .Select(x => new {id = x.Id, name = x.Login})
                .ToQueryString();
            const string queryResult = @"SELECT _t0.""Id"" AS ""id"", _t0.""Login"" AS ""name"" FROM ""User"" AS _t0";

            Assert.AreEqual(queryResult, queryString);
        }

        [Test]
        public void SelectField()
        {
            var queryString = _queryBuilder.From<User>()
                .Select(x => x.Id)
                .ToQueryString();
            const string queryResult = @"SELECT _t0.""Id"" AS ""Id"" FROM ""User"" AS _t0";

            Assert.AreEqual(queryResult, queryString);
        }

        [Test]
        public void SelectDistinct()
        {
            var queryString = _queryBuilder.From<User>()
                .SelectDistinct(x => new {name = x.Login})
                .ToQueryString();
            const string queryResult = @"SELECT DISTINCT _t0.""Login"" AS ""name"" FROM ""User"" AS _t0";

            Assert.AreEqual(queryResult, queryString);
        }

        [Test]
        public void SelectAllFieldsDistinct()
        {
            var queryString = _queryBuilder.From<User>()
                .SelectDistinct()
                .ToQueryString();
            const string queryResult =
                @"SELECT DISTINCT _t0.""Id"" AS ""Id"", _t0.""Login"" AS ""Login"" FROM ""User"" AS _t0";

            Assert.AreEqual(queryResult, queryString);
        }

        [Test]
        public void SelectDistinctOn()
        {
            var queryString = _queryBuilder.From<User>()
                .SelectDistinctOn(x => new {id = x.Id, name = x.Login}, x => x.Login)
                .ToQueryString();
            const string queryResult =
                @"SELECT DISTINCT ON (_t0.""Login"") _t0.""Id"" AS ""id"", _t0.""Login"" AS ""name"" FROM ""User"" AS _t0";

            Assert.AreEqual(queryResult, queryString);
        }

        [Test]
        public void SelectAllFieldsDistinctOn()
        {
            var queryString = _queryBuilder.From<User>()
                .SelectDistinctOn(x => x.Login)
                .ToQueryString();
            const string queryResult =
                @"SELECT DISTINCT ON (_t0.""Login"") _t0.""Id"" AS ""Id"", _t0.""Login"" AS ""Login"" FROM ""User"" AS _t0";

            Assert.AreEqual(queryResult, queryString);
        }
        
        [Test]
        public void SelectAllFieldsDistinctOnAll()
        {
            var queryString = _queryBuilder.From<User>()
                .SelectDistinctOn(x => x)
                .ToQueryString();
            const string queryResult =
                @"SELECT DISTINCT ON (_t0.""Id"", _t0.""Login"") _t0.""Id"" AS ""Id"", _t0.""Login"" AS ""Login"" FROM ""User"" AS _t0";

            Assert.AreEqual(queryResult, queryString);
        }
    }
}