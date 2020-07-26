namespace QueryBuilder.Builders
{
    public class PostgresqlQueryBuilder
    {
        public PostgresqlQueryable<T> From<T>()
        {
            return new PostgresqlQueryable<T>();
        }
    }
}