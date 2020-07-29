using System;
using DanilovSoft.MicroORM;
using QueryBuilder.Contract;
using QueryBuilder.Provider;

namespace QueryBuilder.DanilovSoft.MicroORM
{
    public class DanilovSoftMicroOrmPgQueryBuilder : PgQueryBuilder
    {
        public SqlORM SqlOrm { get; }

        public DanilovSoftMicroOrmPgQueryBuilder(SqlORM sqlOrm)
        {
            SqlOrm = sqlOrm ?? throw new ArgumentNullException(nameof(sqlOrm));
        }
    }
}