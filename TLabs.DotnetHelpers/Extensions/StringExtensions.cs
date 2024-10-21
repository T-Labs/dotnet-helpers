using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace TLabs.DotnetHelpers
{
    public enum LetterCapitalization { Any, Upper, Lower };

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

        /// <summary>Use instead of str.Substring, return empty string in case of ArgumentOutOfRange</summary>
        public static string SubstringSafe(this string value, int startIndex, int length)
        {
            if (value.NotHasValue() || value.Length <= startIndex)
                return "";
            if (startIndex + length > value.Length)
                length = value.Length - startIndex;
            return value.Substring(startIndex, length);
        }

        public static bool OnlyHasEngLetters(this string value, LetterCapitalization capit = LetterCapitalization.Any,
            bool allowDigits = false)
        {
            string regexParams = capit switch
            {
                LetterCapitalization.Any => "A-Za-z",
                LetterCapitalization.Upper => "A-Z",
                LetterCapitalization.Lower => "a-z",
                _ => throw new ArgumentException($"Unknown LetterCapitalization '{capit}'"),
            };
            if (allowDigits)
                regexParams += "0-9";
            return Regex.IsMatch(value, @$"^[{regexParams}]+$");
        }

        public static decimal? DecimalTryParse(this string str)
        {
            if (decimal.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal result))
                return result;
            return null;
        }

        /// <summary>Correctly combine parts of url</summary>
        public static string UrlCombine(this string url1, string url2)
        {
            // https://stackoverflow.com/a/2806717
            if (url1.Length == 0)
                return url2;
            if (url2.Length == 0)
                return url1;

            url1 = url1.TrimEnd('/', '\\');
            url2 = url2.TrimStart('/', '\\');
            return string.Format("{0}/{1}", url1, url2);
        }

        public static bool IsIP(this string url) =>
            new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b").IsMatch(url);
    }
}
