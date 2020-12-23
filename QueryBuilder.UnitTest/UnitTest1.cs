using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using QueryBuilder.Extension.Queryable;
using QueryBuilder.Provider;
using QueryBuilder.SelectBuilder;


namespace QueryBuilder.UnitTest
{
    public class Tests
    {
        private SelectQueryParser _selectQueryParser;

        [SetUp]
        public void Setup()
        {
            _selectQueryParser = new SelectQueryParser();
        }

        [Test]
        public void Test1()
        {
            // var i = default(QueryBuilder.Contract.IPgJoin<int, int, int>);
            // var e = Expression.Lambda(Expression.Constant(10));
            // new PgQueryBuilder().From<TestClass>()
            // .CrossJoin<TestClass, TestClass1>()
            // .CrossJoin<TestClass, TestClass1, TestClass1>();
            // .Join((x, y) => true, PgHelper.Generic.Empty<TestClass1>())
            // .LeftJoin((x, y) => true, PgHelper.Generic.Empty<TestClass1>())
            // .RightJoin((x, y) => true, PgHelper.Generic.Empty<TestClass1>())
            // .FullJoin((x, y) => true, PgHelper.Generic.Empty<TestClass1>())
            // .Select(x => new {Id = x.From.Id, Name1 = x.Join1.Name});
            // .CrossJoin<TestClass, TestClass, TestClass, TestClass>()
            // .Join<TestClass, TestClass, TestClass, TestClass, TestClass>((t1, t2, t3, t4, t5) => true)
            // ;
            // var genClass = new PgQueryableJoinExtensionGenerator().Gen();
            // _selectQueryBuilder.Pars((TestClass x) => new {id = x.Id, x.Name, count =x.SubIds.Count()}, false);
            // Expression<Func<TestClass, TestClass>> expression = x => x;
            // _selectQueryBuilder.Parse(expression, false);
            Expression<Func<TestClass, object>> expression = x => new {id = x.Id, x.Name, count = x.SubIds.Count()};
            Expression<Func<TestClass, object>> e1 = x => new {x.Id, x.Name};
            _selectQueryParser.Parse(expression, e1);
            // Array.Empty<int>().AsQueryable().OrderBy(x=>x).ThenBy(x=>x).Select(x=>x+2).Take()
            Assert.Pass();
        }

        private class TestClass1 : TestClass
        {
        }

        private class TestClass2 : TestClass
        {
        }

        private class TestClass
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public Guid[] SubIds { get; set; }
        }
    }
}