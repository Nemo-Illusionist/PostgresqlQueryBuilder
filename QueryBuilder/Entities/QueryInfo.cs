using System.Collections.Generic;

namespace QueryBuilder.Entities
{
    public class QueryInfo
    {
        public string TableName { get; set; }
        public string TableAlias { get; set; }
        public List<ColumnInfo> Selects { get; set; }
        public List<DistinctInfo> Distincts { get; set; }
        public bool IsDistinct { get; set; }
    }
}