using QueryBuilder.Entities;
using QueryBuilder.Extension.Queryable;
using QueryBuilder.Provider;

namespace QueryBuilder.Extension
{
    internal static class PgQueryNodeExtension
    {
        public static bool IsSelect(this PgQueryNode node)
        {
            if (node == null)
            {
                return false;
            }

            return node.Method == nameof(PgQueryableSelectExtension.Select)
                   || node.Method == nameof(PgQueryableSelectExtension.SelectDistinct)
                   || node.Method == nameof(PgQueryableSelectExtension.SelectDistinctOn);
        }

        public static bool IsJoin(this PgQueryNode node)
        {
            if (node == null)
            {
                return false;
            }

            return node.Method == nameof(PgQueryableJoinExtension.Join)
                   || node.Method == nameof(PgQueryableJoinExtension.CrossJoin)
                   || node.Method == nameof(PgQueryableJoinExtension.FullJoin)
                   || node.Method == nameof(PgQueryableJoinExtension.LeftJoin)
                   || node.Method == nameof(PgQueryableJoinExtension.RightJoin);
        }

        public static bool IsFrom(this PgQueryNode node)
        {
            if (node == null)
            {
                return false;
            }

            return node.Method == nameof(PgQueryBuilder.From);
        }
    }
}