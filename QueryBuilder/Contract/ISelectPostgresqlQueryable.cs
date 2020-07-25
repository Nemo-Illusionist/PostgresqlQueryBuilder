namespace QueryBuilder.Contract
{
    public interface ISelectPostgresqlQueryable<out T> : IPostgresqlQueryable<T>
    {
    }
}