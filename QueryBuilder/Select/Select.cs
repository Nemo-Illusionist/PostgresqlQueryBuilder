using System;
using System.Collections.Generic;

namespace QueryBuilder.Select
{
    internal class Select
    {
        public bool IsDistinct { get; }

        public IReadOnlyCollection<SelectElement> Elements { get; }

        public Select(IReadOnlyCollection<SelectElement> elements, bool isDistinct = false)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
            IsDistinct = isDistinct;
        }
    }
}