using System;
using System.Text;

namespace QueryBuilder.Extension
{
    internal static class StringBuilderExtension
    {
        public static void AddSpace(this StringBuilder sb)
        {
            sb.Append(' ');
        }

        public static void AddInQuotes(this StringBuilder sb, string value)
        {
            sb.Append('"');
            sb.Append(value);
            sb.Append('"');
        }

        public static void TrimEnd(this StringBuilder sb)
        {
            if (sb == null)
            {
                throw new ArgumentNullException(nameof(sb));
            }

            int i = sb.Length - 1;
            for (; i >= 0; i--)
            {
                if (!char.IsWhiteSpace(sb[i]))
                {
                    break;
                }
            }

            if (i < sb.Length - 1)
            {
                sb.Length = i + 1;
            }
        }
    }
}