using DanilovSoft.MicroORM;

namespace QueryBuilder.DanilovSoft.MicroORM
{
    public class DanilovSoftMicroOrmPgQueryProvider : PgQueryProvider
    {
        public SqlORM SqlOrm { get; }

        public DanilovSoftMicroOrmPgQueryProvider(SqlORM sqlOrm)
        {
            SqlOrm = sqlOrm;
        }
    }
}