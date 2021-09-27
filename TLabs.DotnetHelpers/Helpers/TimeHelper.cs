using System;

namespace TLabs.DotnetHelpers.Helpers
{
    public static class TimeHelper
    {
        public static long GetUnixLongTimestampNow() => GetUnixLongTimestamp(DateTimeOffset.UtcNow);

        /// <summary>Timestamp with milliseconds. Example: 163212555831</summary>
        public static long GetUnixLongTimestamp(DateTimeOffset date)
        {
            TimeSpan t = date.UtcDateTime - new DateTime(1970, 1, 1, 0, 0, 0);
            return (long)t.TotalMilliseconds;
        }

        public static int GetUnixTimestampNow() => GetUnixTimestamp(DateTimeOffset.UtcNow);

        /// <summary>Timestamp with seconds. Example: 1632125555</summary>
        public static int GetUnixTimestamp(DateTimeOffset date)
        {
            TimeSpan t = date.UtcDateTime - new DateTime(1970, 1, 1, 0, 0, 0);
            return (int)t.TotalSeconds;
        }

        /// <param name="timestamp">Timestamp with milliseconds. Example: 163212555831</param>
        public static DateTimeOffset LongTimestampToDate(long timestamp)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(timestamp);
            return new DateTimeOffset(date, TimeSpan.Zero);
        }

        public static DateTimeOffset TimestampToDate(long timestamp) =>
            LongTimestampToDate(timestamp * 1000L);

        /// <summary>Get delay time for next request attempt (logarithmic increase)</summary>
        /// <param name="countAttempts">How many attempts were done. 0 - no delay</param>
        /// <param name="maxDelay">Default is 4 hours</param>
        public static TimeSpan GetDelay(int countAttempts, TimeSpan? maxDelay = null)
        {
            if (countAttempts == 0)
                return TimeSpan.Zero;

            if (maxDelay == null)
                maxDelay = TimeSpan.FromHours(4);

            var delay = TimeSpan.FromSeconds(10 + ((int)Math.Pow(2, countAttempts) - 1) / 2);

            if (delay > maxDelay.Value)
                delay = maxDelay.Value;
            return delay;
        }
    }
}
