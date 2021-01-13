using QueryBuilder.Entities;

namespace QueryBuilder.Contract
{
    public interface IPgQueryGenerator
    {
        string Execute(PgQueryNode pgNode);
    }
}