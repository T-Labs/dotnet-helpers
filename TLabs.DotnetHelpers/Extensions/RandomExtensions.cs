using System;

namespace TLabs.DotnetHelpers
{
    public static class RandomExtensions
    {
        public static decimal NextDecimal(this Random rnd, decimal from, decimal to)
        {
            if (from == to)
            {
                return from; // fix DivideBy0 error
            }
            if (to < from)
            {
                return rnd.NextDecimal(to, from);
            }

            byte fromScale = new System.Data.SqlTypes.SqlDecimal(from).Scale;
            byte toScale = new System.Data.SqlTypes.SqlDecimal(to).Scale;

            byte scale = (byte)(fromScale + toScale);
            if (scale > 28)
            {
                scale = 28;
            }

            decimal r = new decimal(rnd.Next(), rnd.Next(), rnd.Next(), false, scale);
            if (Math.Sign(from) == Math.Sign(to) || from == 0 || to == 0)
            {
                return decimal.Remainder(r, to - from) + from;
            }

            bool getFromNegativeRange = (double)from + rnd.NextDouble() * ((double)to - (double)from) < 0;
            return getFromNegativeRange ? decimal.Remainder(r, -from) + from : decimal.Remainder(r, to);
        }
    }
}
