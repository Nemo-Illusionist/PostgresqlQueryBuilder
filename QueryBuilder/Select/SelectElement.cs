using System;
using System.Reflection;

namespace QueryBuilder.Select
{
    public class SelectElement
    {
        public string TableHint { get; set; }

        public string FieldName { get; }

        public string AsName { get; }

        public Type Type { get; }

        public MethodInfo Method { get; set; }

        public SelectElement(string fieldName, string asName, Type type)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(fieldName));
            FieldName = fieldName;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            AsName = string.IsNullOrEmpty(asName) ? FieldName : asName;
        }
    }
}