using System;

namespace QueryBuilder.Select
{
    internal class Select
    {
        public string TableHint { get; set; }

        public string FieldName { get; }

        public string FuncTemplate { get; set; }

        public string AsName { get; }

        public Select(string fieldName, string asName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(fieldName));
            FieldName = fieldName;
            AsName = string.IsNullOrEmpty(asName) ? FieldName : asName;
        }
    }
}