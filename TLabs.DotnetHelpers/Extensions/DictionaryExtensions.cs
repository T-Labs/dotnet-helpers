using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLabs.DotnetHelpers
{
    public static class DictionaryExtensions
    {
        /// <summary>Return value from dictionary or add if not exists and return it</summary>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey key, Func<TValue> valueCreator)
        {
            TValue value;
            if (dictionary.TryGetValue(key, out value)) return value;

            value = valueCreator();
            dictionary.Add(key, value);
            return value;
        }

        /// <summary>Return value from dictionary or add if not exists and return it</summary>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey key, TValue defaultValue) =>
            dictionary.GetOrAdd(key, () => defaultValue);
    }
}
