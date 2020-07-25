using System.Net.NetworkInformation;
using QueryBuilder.Contract;

namespace QueryBuilder
{
    public class PostgresqlQueryBuilder
    {
        public IPostgresqlQueryable<T> From<T>()
        {
            throw new NetworkInformationException();
        }
    }
}