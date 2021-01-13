using System.Collections.Generic;
using System.Text;

namespace QueryBuilder.Entities
{
    internal class FromAndJoinGenReturn
    {
        public IReadOnlyCollection<TableAlias> TableAliases { get; }
        public StringBuilder JoinQuery { get; }

        public FromAndJoinGenReturn(IReadOnlyCollection<TableAlias> tableAliases, StringBuilder joinQuery)
        {
            TableAliases = tableAliases;
            JoinQuery = joinQuery;
        }

        public void Deconstruct(out IReadOnlyCollection<TableAlias> tableAliases, out StringBuilder joinQuery)
        {
            tableAliases = TableAliases;
            joinQuery = JoinQuery;
        }
    }
}