using System;

namespace QueryBuilder.Entities
{
    public class ColumnInfo
    {
        public ColumnInfo(string columnName, string asName = null)
        {
            if (string.IsNullOrWhiteSpace(columnName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(columnName));

            ColumnName = columnName;
            AsName = string.IsNullOrEmpty(asName) ? ColumnName : asName;
        }

        public string ColumnName { get; }

        public string AsName { get; }
    }
}