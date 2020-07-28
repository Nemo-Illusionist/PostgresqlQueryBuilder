using System;
using DanilovSoft.MicroORM;
using QueryBuilder.Contract;

namespace QueryBuilder.DanilovSoft.MicroORM
{
    public class DanilovSoftMicroOrmPgQueryBuilder : PgQueryBuilder
    {
        private readonly SqlORM _sqlOrm;

        public DanilovSoftMicroOrmPgQueryBuilder(SqlORM sqlOrm)
        {
            _sqlOrm = sqlOrm ?? throw new ArgumentNullException(nameof(sqlOrm));
        }

        public override IPgQueryable<T> From<T>()
        {
            var provider = new DanilovSoftMicroOrmPgQueryProvider(_sqlOrm);
            return provider.CreateQuery<T>(new PgQueryNode(nameof(From)));
        }
    }
}