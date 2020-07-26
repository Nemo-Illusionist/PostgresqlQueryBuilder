using NUnit.Framework;
using QueryBuilder.Extension;
using QueryBuilder.UnitTest.Entities;

namespace QueryBuilder.UnitTest
{
    public class ExtensionTest
    {
        private PostgresqlQueryBuilder _queryBuilder;

        [SetUp]
        public void Setup()
        {
            _queryBuilder = new PostgresqlQueryBuilder();
        }

        [Test]
        public void NotEqualTest()
        {
            var q1 = _queryBuilder.From<Person>();
            var q2 = q1.Select(x => x.Id);
            var qs1 = q1.ToQueryString();
            var qs2 = q2.ToQueryString();
            Assert.AreNotEqual(qs1, qs2);
        }
        
        [Test]
        public void EqualTest()
        {
            var q1 = _queryBuilder.From<Person>();
            var q2 = q1.Select(x => x.Id);
            var qs1 = q1.Select(x => x.Id).ToQueryString();
            var qs2 = q2.ToQueryString();
            Assert.AreEqual(qs1, qs2);
        }
    }
}