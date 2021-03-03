using System;

namespace TLabs.DotnetHelpers.Helpers
{
    public static class TimeHelper
    {
        public static int GetUnixTimestampNow() => GetUnixTimestamp(DateTimeOffset.UtcNow);

        public static int GetUnixTimestamp(DateTimeOffset date)
        {
            TimeSpan t = date.UtcDateTime - new DateTime(1970, 1, 1, 0, 0, 0);
            return (int)t.TotalSeconds;
        }

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
