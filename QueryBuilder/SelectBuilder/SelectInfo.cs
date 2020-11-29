using System;
using System.Collections.Generic;

namespace QueryBuilder.SelectBuilder
{
    public class SelectInfo
    {
        public bool IsDistinct { get; }

        public IReadOnlyCollection<SelectElement> Elements { get; }
        
        public IReadOnlyCollection<SelectElement> DistinctElements { get; }

        public SelectInfo(IReadOnlyCollection<SelectElement> elements, bool isDistinct = false)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
            DistinctElements = null;
            IsDistinct = isDistinct;
        }        
        
        public SelectInfo(IReadOnlyCollection<SelectElement> elements, IReadOnlyCollection<SelectElement> distinctElements)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
            DistinctElements = distinctElements;
            IsDistinct = false;
        }
    }
}