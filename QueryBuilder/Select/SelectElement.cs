using System;
using System.Reflection;

namespace QueryBuilder.Select
{
    public class SelectElement
    {
        public string TableHint { get; }

        public string FieldName { get; }

        public string AsName { get; }

        public Type Type { get; }

        public MethodInfo Method { get; }

        public SelectElement(string table, string fieldName, Type type, string asName = null, MethodInfo method = null)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(fieldName));
            if (string.IsNullOrWhiteSpace(table))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(table));

            TableHint = table;
            FieldName = fieldName;
            Method = method;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            AsName = string.IsNullOrEmpty(asName) ? FieldName : asName;
        }
    }
}