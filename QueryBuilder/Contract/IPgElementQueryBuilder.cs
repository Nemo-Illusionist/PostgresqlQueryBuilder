using System.Text;

namespace QueryBuilder.Contract
{
    public interface IPgElementQueryBuilder
    {
        StringBuilder Build();
        StringBuilder Build(PgQueryNode node);
    }
}