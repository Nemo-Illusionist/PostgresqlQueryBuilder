using System;
using System.Linq.Expressions;
using NUnit.Framework;
using QueryBuilder.Extension.Queryable;
using QueryBuilder.Helpers;
using QueryBuilder.Provider;
using QueryBuilder.UnitTest.Entities;


namespace QueryBuilder.UnitTest
{
    public class DevTests
    {

        [Test]
        public void Test1()
        {
            var parameter = Expression.Parameter(typeof(int), "x");
            var lambda = Expression.Lambda(parameter,parameter);
            
            // var i = default(QueryBuilder.Contract.IPgJoin<int, int, int>);
            // var e = Expression.Lambda(Expression.Constant(10));
            var queryString = new PgQueryBuilder().From<User>()
                .Join((u, ud) => u.Id == ud.UserId && u.Login == ud.Name, PgHelper.Generic.Empty<UserData>())
                .LeftJoin((_, uds, tc) => tc.Name == uds.Name, PgHelper.Generic.Empty<TestClass>())
                .Select(x => new {x.From.Id, x.Join1.UserId, testClass = x.Join2.Name})
                .ToQueryString();
            Console.WriteLine(queryString);
                // .CrossJoin(PgHelper.Generic.Empty<TestClass1>())
                // .CrossJoin(PgHelper.Generic.Empty<TestClass1>())
                // .Join((t1, t2, t3, t4) => true, PgHelper.Generic.Empty<TestClass1>())
                // .LeftJoin((t1, t2, t3, t4, t5) => true, PgHelper.Generic.Empty<TestClass1>())
                // .RightJoin((t1, t2, t3, t4, t5, t6) => true, PgHelper.Generic.Empty<TestClass1>())
                // .FullJoin((t1, t2, t3, t4, t5, t6, t7) => true, PgHelper.Generic.Empty<TestClass1>())
                // .Select(x => new {x.From.Id, Name1 = x.Join1.Name})
                // ;
            // var genClass = new PgQueryableJoinExtensionGenerator().Gen();
            // _selectQueryBuilder.Pars((TestClass x) => new {id = x.Id, x.Name, count =x.SubIds.Count()}, false);
            // Expression<Func<TestClass, TestClass>> expression = x => x;
            // _selectQueryBuilder.Parse(expression, false);
            // Expression<Func<TestClass, object>> expression = x => new {id = x.Id, x.Name, count = x.SubIds.Count()};
            // Expression<Func<TestClass, object>> e1 = x => new {x.Id, x.Name};
            // Array.Empty<int>().AsQueryable().OrderBy(x=>x).ThenBy(x=>x).Select(x=>x+2).Take()
            // Assert.Pass();
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