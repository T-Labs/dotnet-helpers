using System;

namespace TLabs.DotnetHelpers
{
    public enum RoundType { Nearest, Up, Down };

    public static class DecimalExtensions
    {
        /// <summary>
        /// Remove trailing zeroes
        /// </summary>
        public static decimal Normalize(this decimal value) => value / 1.000000000000000000000000000000000m;

        public static decimal Round(this decimal value, int places, RoundType roundType = RoundType.Nearest)
        {
            if (roundType == RoundType.Nearest)
                return decimal.Round(value, places);

            decimal multiplier = (decimal)Math.Pow(10, places);
            decimal result = roundType == RoundType.Down
                ? Math.Floor(value * multiplier) / multiplier
                : Math.Ceiling(value * multiplier) / multiplier;
            return decimal.Round(result, places);
        }

        public static decimal RoundUp(this decimal value, int places) => value.Round(places, RoundType.Up);

        public static decimal RoundDown(this decimal value, int places) => value.Round(places, RoundType.Down);

        /// <summary>
        /// Returns original number if places is null
        /// </summary>
        public static decimal TryRound(this decimal value, int? places, RoundType roundType = RoundType.Nearest)
        {
            if (places == null)
                return value;
            return value.Round(places.Value, roundType);
        }
    }
}
