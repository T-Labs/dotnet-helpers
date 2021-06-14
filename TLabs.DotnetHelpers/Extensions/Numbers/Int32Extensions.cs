using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLabs.DotnetHelpers.Extensions
{
    public static class Int32Extensions
    {
        /// <summary>Add spaces between thousands</summary>
        public static string Readable(this int value)
        {
            return value.ToString("n", new NumberFormatInfo { NumberGroupSeparator = " " }); // add space between 1000's
        }
    }
}
