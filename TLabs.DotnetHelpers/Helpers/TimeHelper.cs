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
    }
}
