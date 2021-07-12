using Newtonsoft.Json;
using System;
using System.Linq;

namespace TLabs.DotnetHelpers
{
    public static class ParseHelper
    {
        /// <summary>Parse to object/string/bool/int/long/decimal</summary>
        public static T Parse<T>(string value)
        {
            if (typeof(T) == typeof(string))
                return (T)(object)value;
            else if (typeof(T) == typeof(bool))
                return (T)(object)bool.Parse(value);
            else if (typeof(T) == typeof(int))
                return (T)(object)int.Parse(value);
            else if (typeof(T) == typeof(long))
                return (T)(object)long.Parse(value);
            else if (typeof(T) == typeof(decimal))
                return (T)(object)decimal.Parse(value);
            else if (typeof(T).IsClass) // not value type
                return JsonConvert.DeserializeObject<T>(value);
            else
                throw new ArgumentException($"ParseHelper unsupported type: {typeof(T).Name}");
        }
    }
}
