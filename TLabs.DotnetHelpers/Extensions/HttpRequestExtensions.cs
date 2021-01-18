using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using System.Text;

namespace TLabs.DotnetHelpers
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Get request body for POST and PUT requests
        /// </summary>
        public static string GetBodyString(this HttpRequest request)
        {
            string body = "";
            if (request.Method == HttpMethod.Post.Method || request.Method == HttpMethod.Put.Method)
            {
                request.EnableBuffering(); // Allows reading body several times in ASP.Net Core
                using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
                {
                    body = reader.ReadToEndAsync().Result;
                }
                request.Body.Position = 0; // Rewind, so the ASP.Net Core is not lost when it looks the body for the request
            }
            return body;
        }
    }
}
