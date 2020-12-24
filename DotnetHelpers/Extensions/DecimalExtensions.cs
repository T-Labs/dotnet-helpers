using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetHelpers.Extensions
{
    public enum RoundType { Nearest, Up, Down };

    public static class DecimalExtensions
    {
        public static decimal Round(this decimal value, int places, 
            RoundType roundType = RoundType.Nearest)
        {
            if (roundType == RoundType.Nearest)
                return decimal.Round(value, places);

            decimal multiplier = (decimal)Math.Pow(10, places);
            decimal result = roundType == RoundType.Down
                ? Math.Floor(value * multiplier) / multiplier
                : Math.Ceiling(value * multiplier) / multiplier;
            return decimal.Round(result, places);
        }
    }
}
