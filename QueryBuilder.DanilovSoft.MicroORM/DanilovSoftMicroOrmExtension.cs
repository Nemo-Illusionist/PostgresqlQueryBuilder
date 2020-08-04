using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DanilovSoft.MicroORM;
using QueryBuilder.Contract;
using QueryBuilder.Extension;

namespace QueryBuilder.DanilovSoft.MicroORM
{
    public static class DanilovSoftMicroOrmExtension
    {
        public static async Task<List<T>> ToListAsync<T>(
            this IPgQueryable<T> queryable, 
            CancellationToken cancellationToken = default)
        {
            var queryProvider = (DanilovSoftMicroOrmPgQueryBuilder) queryable.Provider;
            return await queryProvider.SqlOrm.Sql(queryable.ToQueryString()).ToAsync().List<T>(cancellationToken);
        }

        public static async Task<T[]> ToArrayAsync<T>(
            this IPgQueryable<T> queryable, 
            CancellationToken cancellationToken = default)
        {
            var queryProvider = (DanilovSoftMicroOrmPgQueryBuilder) queryable.Provider;
            return await queryProvider.SqlOrm.Sql(queryable.ToQueryString()).ToAsync().Array<T>(cancellationToken);
        }
        
        public static async Task<T> SingleAsync<T>(
            this IPgQueryable<T> queryable,
            CancellationToken cancellationToken = default)
        {
            var queryProvider = (DanilovSoftMicroOrmPgQueryBuilder) queryable.Provider;
            return await queryProvider.SqlOrm.Sql(queryable.ToQueryString()).ToAsync().Single<T>(cancellationToken);
        }

        public static IPgQueryable<T> From<T>(this SqlORM sqlOrm)
        {
            return new DanilovSoftMicroOrmPgQueryBuilder(sqlOrm).From<T>();
        }
    }
}