namespace QueryBuilder.Contract
{
    public interface IPgJoin<out T1, out T2>
    {
        T1 From { get; }
        T2 Join1 { get; }
    }
}