namespace QueryBuilder.Contract
{
    public interface IPostgresqlQueryable<out T>
    {
        string ToQueryString();
    }
}