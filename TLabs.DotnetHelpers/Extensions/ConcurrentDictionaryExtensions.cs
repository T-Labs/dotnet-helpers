using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLabs.DotnetHelpers
{
    public static class ConcurrentDictionaryExtensions
    {
        public static ConcurrentDictionary<TKey, TSource> ToConcurrentDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return ToConcurrentDictionary<TSource, TKey, TSource>(source, keySelector, IdentityFunction<TSource>.Instance, null);
        }

        public static ConcurrentDictionary<TKey, TSource> ToConcurrentDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            return ToConcurrentDictionary<TSource, TKey, TSource>(source, keySelector, IdentityFunction<TSource>.Instance, comparer);
        }

        public static ConcurrentDictionary<TKey, TElement> ToConcurrentDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            //return ToConcurrentDictionary<TSource, TKey, TElement>(source, keySelector, elementSelector, null);
            if (source == null) throw new Exception("Source is null");
            if (keySelector == null) throw new Exception("Key is null");
            if (elementSelector == null) throw new Exception("Selector is null");

            ConcurrentDictionary<TKey, TElement> d = new ConcurrentDictionary<TKey, TElement>();
            foreach (TSource element in source) d.TryAdd(keySelector(element), elementSelector(element));
            return d;
        }

        public static ConcurrentDictionary<TKey, TElement> ToConcurrentDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) throw new Exception("Source is null");
            if (keySelector == null) throw new Exception("Key is null");
            if (elementSelector == null) throw new Exception("Selector is null");

            ConcurrentDictionary<TKey, TElement> d = new ConcurrentDictionary<TKey, TElement>(comparer);
            foreach (TSource element in source) d.TryAdd(keySelector(element), elementSelector(element));
            return d;
        }

        public static ConcurrentBag<TSource> ToConcurrentBag<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new Exception("Source is null");
            return new ConcurrentBag<TSource>(source);
        }

        internal class IdentityFunction<TElement>
        {
            public static Func<TElement, TElement> Instance
            {
                get { return x => x; }
            }
        }
    }
}
