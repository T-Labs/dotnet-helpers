using Newtonsoft.Json;
using System;
using System.Linq;

namespace TLabs.DotnetHelpers
{
    public static class CodesGenerator
    {
        /// <summary>Create a string with random digits and/or letters</summary>
        public static string Create(int length, bool useDigits = false,
            bool useUpperLetters = false, bool useLowerLetters = false)
        {
            if (length <= 0)
                throw new ArgumentException("length must be greater than 0");
            string chars = "";
            if (useDigits)
                chars += "23456789"; // 0 and 1 are not used because they are similar to O and l
            if (useUpperLetters)
                chars += "ABCDEFGHIJKLMNPQRSTUVWXYZ";
            if (useLowerLetters)
                chars += "abcdefghijkmnpqrstuvwxyz";
            if (chars.NotHasValue())
                throw new ArgumentException("At least one of useDigits, useUpperLetters, useLowerLetters must be true");

            var rand = new Random();
            var result = string.Join("", Enumerable.Range(0, length).Select(i => chars[rand.Next(chars.Length)]));
            return result;
        }
    }
}
