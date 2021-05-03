using System.Linq;

namespace TLabs.DotnetHelpers
{
    public static class StringExtensions
    {
        public static bool HasValue(this string value) => !string.IsNullOrWhiteSpace(value);

        public static bool NotHasValue(this string value) => !HasValue(value);

        /// <summary>Return null if string is empty or whitespace</summary>
        public static string NullIfEmpty(this string value) => value.HasValue() ? value : null;

        public static string RemoveWhitespaces(this string value) =>
            value == null ? null : new string(value.Where(c => !char.IsWhiteSpace(c)).ToArray());

        public static string Cut(this string value, int maxLength = 50)
        {
            if (value.NotHasValue())
                return "";
            return value.Length <= maxLength ? value : $"{value.Substring(0, maxLength)}...";
        }

        public static string Cut(this System.Guid guid, int maxLength = 8)
            => guid.ToString().Cut(maxLength);

        /// <summary>return empty string in case of ArgumentOutOfRange</summary>
        public static string SubstringSafe(this string value, int startIndex, int length)
        {
            if (value.NotHasValue() || value.Length <= startIndex)
                return "";
            if (startIndex + length > value.Length)
                length = value.Length - startIndex;
            return value.Substring(startIndex, length);
        }
    }
}
