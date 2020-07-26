using System.Net.NetworkInformation;
using QueryBuilder.Contract;

namespace QueryBuilder
{
    public class PostgresqlQueryBuilder
    {
        public PostgresqlQueryable<T> From<T>()
        {
            return new PostgresqlQueryable<T>();
        }
    }
}