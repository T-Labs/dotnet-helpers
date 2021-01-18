using System;

namespace TLabs.DotnetHelpers
{
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Get date with 00:00 UTC +0
        /// </summary>
        public static DateTimeOffset RemoveTimePart(this DateTimeOffset date)
            => new DateTimeOffset(date.Date, TimeSpan.Zero);

        /// <summary>
        /// Get string that will be parsed to DateTimeOffset correctly
        /// </summary>
        public static string ToUrlValue(this DateTimeOffset date) => date.ToString("o");

        /// <summary>
        /// Get UNIX timestamp
        /// </summary>
        public static int GetUnixTime(this DateTimeOffset date)
        {
            TimeSpan t = date.UtcDateTime - new DateTime(1970, 1, 1, 0, 0, 0);
            return (int)t.TotalSeconds;
        }
    }
}
