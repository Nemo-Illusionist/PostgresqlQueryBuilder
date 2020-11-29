using System;
using System.Text;

namespace QueryBuilder.Extension
{
    internal static class StringBuilderExtension
    {
        public static StringBuilder TrimEnd(this StringBuilder sb)
        {
              
            if (sb == null) throw new ArgumentNullException(nameof(sb));
            if (sb.Length == 0) return sb;

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

            return sb;
        }
    }
}