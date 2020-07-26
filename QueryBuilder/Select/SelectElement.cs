using System;

namespace QueryBuilder.Select
{
    public class SelectElement
    {
        public string TableHint { get; set; }
        
        public string FuncTemplate { get; set; }
        
        public string FieldName { get; }
        
        public string AsName { get; }
        
        public bool IsDistinct { get; }

        public SelectElement(string fieldName, string asName, bool isDistinct = false)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(fieldName));
            FieldName = fieldName;
            AsName = string.IsNullOrEmpty(asName) ? FieldName : asName;
            IsDistinct = isDistinct;
        }
    }
}