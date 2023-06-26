using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace TLabs.DotnetHelpers
{
    public static class HttpRequestExtensions
    {
        public static string GetBaseUrl(this HttpRequest req)
        {
            // https://blog.elmah.io/how-to-get-base-url-in-asp-net-core/
            if (req == null)
                return null;
            var uriBuilder = new UriBuilder(req.Scheme, req.Host.Host, req.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
                uriBuilder.Port = -1;

            return uriBuilder.Uri.AbsoluteUri;
        }

        /// <summary>Get request body for POST and PUT requests</summary>
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
