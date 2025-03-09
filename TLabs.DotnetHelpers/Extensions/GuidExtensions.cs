using System;
using System.Linq;

namespace TLabs.DotnetHelpers
{
    public static class GuidExtensions
    {

        static int[] byteOrder = { 15, 14, 13, 12, 11, 10, 9, 8, 6, 7, 4, 5, 0, 1, 2, 3 };

        /// <summary>Increase guid value by 1</summary>
        public static Guid Increment(this Guid guid)
        {
            var bytes = guid.ToByteArray();
            var canIncrement = byteOrder.Any(i => ++bytes[i] != 0);
            return new Guid(canIncrement ? bytes : new byte[16]);
        }

        /// <summary>Create new guid from 2 existing guids</summary>
        public static Guid Combine(this Guid guid1, Guid guid2)
        {
            if (guid1 == Guid.Empty || guid2 == Guid.Empty)
            {
                throw new ArgumentException($"guid1:{guid1} or guid2:{guid2} is empty!");
            }
            byte[] guid1Bytes = guid1.ToByteArray();
            byte[] guid2Bytes = guid2.ToByteArray();
            byte[] resultBytes = new byte[guid1Bytes.Length];

            for (int i = 0; i < guid1Bytes.Length; i++)
            {
                resultBytes[i] = (byte)(guid1Bytes[i] ^ guid2Bytes[i]);
            }
            return new Guid(resultBytes);
        }
    }
}
