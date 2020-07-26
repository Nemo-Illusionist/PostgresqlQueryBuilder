using QueryBuilder.Entities;

namespace QueryBuilder.Builders
{
    public class PostgresqlQueryBuilder
    {
        public PostgresqlQueryable<T> From<T>()
        {
            static void Mutate(ref PostgresqlQueryableInfo<T> info)
            {
                info ??= new PostgresqlQueryableInfo<T>();
            }

            return new PostgresqlQueryable<T>(Mutate);
        }
    }
}