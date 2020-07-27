using System;

namespace QueryBuilder.Select
{
    public class SelectElement : IEquatable<SelectElement>
    {
        public string TableHint { get; set; }

        public string FuncTemplate { get; set; }

        public bool IsDistinct { get; set; }

        public string FieldName { get; }

        public string AsName { get; }

        public SelectElement(string fieldName, string asName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(fieldName));
            FieldName = fieldName;
            AsName = string.IsNullOrEmpty(asName) ? FieldName : asName;
        }

        public bool Equals(SelectElement other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FieldsEquals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return FieldsEquals((SelectElement) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TableHint, FuncTemplate, IsDistinct, FieldName, AsName);
        }

        private bool FieldsEquals(SelectElement other)
        {
            return TableHint == other.TableHint &&
                   FuncTemplate == other.FuncTemplate &&
                   IsDistinct == other.IsDistinct &&
                   FieldName == other.FieldName &&
                   AsName == other.AsName;
        }
    }
}