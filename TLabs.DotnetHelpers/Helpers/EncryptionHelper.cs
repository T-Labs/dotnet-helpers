using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TLabs.DotnetHelpers.Helpers
{
    public static class EncryptionHelper
    {
        /// <summary>
        /// Sign a message with SHA256 hash
        /// </summary>
        /// <param name="message">Message to sign</param>
        /// <param name="privateKey">Private key</param>
        /// <returns>Signature in hex</returns>
        public static string SHA256Sign(string message, string privateKey)
        {
            return new HMACSHA256(Encoding.UTF8.GetBytes(privateKey))
                .ComputeHash(Encoding.UTF8.GetBytes(message))
                .Aggregate(new StringBuilder(), (sb, b) => sb.AppendFormat("{0:x2}", b), (sb) => sb.ToString()); // convert bytes to hex string
        }
    }
}
