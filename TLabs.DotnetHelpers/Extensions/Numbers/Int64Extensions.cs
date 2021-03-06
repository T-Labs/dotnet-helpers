using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLabs.DotnetHelpers
{
    public static class Int64Extensions
    {
        /// <summary>Add spaces between thousands</summary>
        public static string Readable(this long value)
        {
            return value.ToString("N0", new NumberFormatInfo { NumberGroupSeparator = " " }); // add space between 1000's
        }
    }
}
