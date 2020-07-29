using NUnit.Framework;
using QueryBuilder.Extension;
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
            var queryString = _queryBuilder.From<Person>().Where(x=>x.Name == "ivan").ToQueryString();
            const string queryResult = @"SELECT p.""Id"" AS ""Id"", p.""Name"" AS ""Name"" FROM ""Person"" AS p WHERE p.""Name"" = 'ivan'";

            Assert.AreEqual(queryResult, queryString);
        }
    }
}