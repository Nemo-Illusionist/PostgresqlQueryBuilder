using System;
using System.Linq;
using NUnit.Framework;

namespace QueryBuilder.UnitTest
{
    public class Tests
    {
        private SelectQueryBuilder _selectQueryBuilder;

        [SetUp]
        public void Setup()
        {
            _selectQueryBuilder = new SelectQueryBuilder();
        }

        [Test]
        public void Test1()
        {
            _selectQueryBuilder.Gen((TestClass x) => new {id = x.Id, x.Name, count =x.SubIds.Count()});
            Assert.Pass();
        }

        private class TestClass
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public Guid[] SubIds { get; set; }
        }
    }
}