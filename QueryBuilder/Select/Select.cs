using System;
using System.Collections.Generic;

namespace QueryBuilder.Select
{
    public class Select
    {
        public bool IsDistinct { get; }

        public IReadOnlyCollection<SelectElement> Elements { get; }
        
        public IReadOnlyCollection<SelectElement> DistinctElements { get; }

        public Select(IReadOnlyCollection<SelectElement> elements, bool isDistinct = false)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
            DistinctElements = null;
            IsDistinct = isDistinct;
        }        
        
        public Select(IReadOnlyCollection<SelectElement> elements, IReadOnlyCollection<SelectElement> distinctElements)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
            DistinctElements = distinctElements;
            IsDistinct = false;
        }
    }
}