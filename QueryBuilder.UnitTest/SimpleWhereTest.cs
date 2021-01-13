using NUnit.Framework;
using QueryBuilder.Extension.Queryable;
using QueryBuilder.Provider;
using QueryBuilder.UnitTest.Entities;

namespace QueryBuilder.UnitTest
{
    public class SimpleWhereTest
    {
        private PgQueryBuilder _queryBuilder;

        [SetUp]
        public void Setup()
        {
            _queryBuilder = new PgQueryBuilder();
        }
        
        [Test]
        public void FromWhereSelect()
        {
            var queryString = _queryBuilder.From<User>().Where(x=>x.Login == "name").ToQueryString();
            const string queryResult = @"SELECT p.""Id"" AS ""Id"", p.""Name"" AS ""Name"" FROM ""Person"" AS p WHERE p.""Name"" = 'ivan'";

            Assert.AreEqual(queryResult, queryString);
        }
    }
}