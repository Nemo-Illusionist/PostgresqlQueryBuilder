using System.Collections.Generic;
using System.Threading.Tasks;
using DanilovSoft.MicroORM;
using QueryBuilder.Contract;
using QueryBuilder.Extension;

namespace QueryBuilder.DanilovSoft.MicroORM
{
    public static class DanilovSoftMicroOrmExtension
    {
        public static async Task<List<T>> ToList<T>(this IPgQueryable<T> queryable)
        {
            var queryProvider = (DanilovSoftMicroOrmPgQueryBuilder) queryable.Provider;
            return await queryProvider.SqlOrm.Sql(queryable.ToQueryString()).ToAsync().List<T>();
        }

        public static async Task<T[]> ToArray<T>(this IPgQueryable<T> queryable)
        {
            var queryProvider = (DanilovSoftMicroOrmPgQueryBuilder) queryable.Provider;
            return await queryProvider.SqlOrm.Sql(queryable.ToQueryString()).ToAsync().Array<T>();
        }

        public static IPgQueryable<T> From<T>(this SqlORM sqlOrm)
        {
            return new DanilovSoftMicroOrmPgQueryBuilder(sqlOrm).From<T>();
        }
    }
}