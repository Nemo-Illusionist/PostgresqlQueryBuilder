using System;
using System.Linq;
using System.Linq.Expressions;
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
            // Expression<Func<TestClass, TestClass>> expression = x => x;
            // _selectQueryBuilder.Parse(expression, false);
            Expression<Func<TestClass, object>> expression = x => new {id = x.Id, x.Name, count = x.SubIds.Count()};
            Expression<Func<TestClass, object>> e1 = x => new {x.Name};
            _selectQueryBuilder.Parse(expression, e1);
            // Array.Empty<int>().AsQueryable().OrderBy(x=>x).ThenBy(x=>x).Select(x=>x+2)
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