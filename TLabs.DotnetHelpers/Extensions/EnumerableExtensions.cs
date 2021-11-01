using System;
using System.Collections.Generic;
using System.Linq;

namespace TLabs.DotnetHelpers
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> UnionSafe<T>(this IEnumerable<T> source1, IEnumerable<T> source2)
        {
            return source1 != null ?
                (source2 != null ? source1.Union(source2) : source1) : source2;
        }

        public static string ToStrings<T>(this IEnumerable<T> source, string separator = "\n ")
        {
            return string.Join(separator, source.Select(_ => _.ToString());
        }
    }
}
