namespace QueryBuilder.Contract
{
    public interface IPostgresqlQueryable<out T>
    {
        IPostgresqlQueryProvider Provider { get; }
        
        string ToQueryString();
    }
}