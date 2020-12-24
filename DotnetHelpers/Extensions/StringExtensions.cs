namespace DotnetHelpers
{
    public static class StringExtensions
    {
        public static bool HasValue(this string value) => !string.IsNullOrWhiteSpace(value);

        public static bool NotHasValue(this string value) => !HasValue(value);

        public static string Cut(this string value, int maxLength = 50)
        {
            if (value.NotHasValue())
                return "";
            return value.Length <= maxLength ? value : $"{value.Substring(0, maxLength)}...";
        }

        public static string Cut(this System.Guid guid, int maxLength = 8)
            => guid.ToString().Cut(maxLength);
    }
}
