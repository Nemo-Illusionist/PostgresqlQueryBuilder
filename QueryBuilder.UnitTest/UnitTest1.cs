using System;
using System.Linq;
using NUnit.Framework;
using QueryBuilder.Select;

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
            // _selectQueryBuilder.Pars((TestClass x) => new {id = x.Id, x.Name, count =x.SubIds.Count()}, false);
            _selectQueryBuilder.Parse(
                (TestClass x) => new {id = x.Id, x.Name, count = x.SubIds.Count()},
                x => new {x.Name});
            // Array.Empty<int>().AsQueryable().OrderBy().ThenBy()
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