using System;
using System.Globalization;

namespace TLabs.DotnetHelpers
{
    public enum RoundingType { Nearest, Up, Down };

    public static class DecimalExtensions
    {
        /// <summary>Add spaces between thousands</summary>
        public static string Readable(this decimal value, bool useRound = false)
        {
            if (useRound)
                value = value > 1_000 ? value.Round(2) : value.Round(8);
            return value.ToString("n", new NumberFormatInfo { NumberGroupSeparator = " " }); // add space between 1000's
        }

        /// <summary>Remove trailing zeroes</summary>
        public static decimal Normalize(this decimal value) => value / 1.000000000000000000000000000000000m;

        public static decimal Round(this decimal value, int places, RoundingType roundType = RoundingType.Nearest)
        {
            if (roundType == RoundingType.Nearest)
                return decimal.Round(value, places);

            decimal multiplier = (decimal)Math.Pow(10, places);
            decimal result = roundType == RoundingType.Down
                ? Math.Floor(value * multiplier) / multiplier
                : Math.Ceiling(value * multiplier) / multiplier;
            return decimal.Round(result, places);
        }

        public static decimal RoundUp(this decimal value, int places) => value.Round(places, RoundingType.Up);

        public static decimal RoundDown(this decimal value, int places) => value.Round(places, RoundingType.Down);

        /// <summary>
        /// Returns original number if places is null
        /// </summary>
        public static decimal TryRound(this decimal value, int? places, RoundingType roundType = RoundingType.Nearest)
        {
            if (places == null)
                return value;
            return value.Round(places.Value, roundType);
        }
    }
}
