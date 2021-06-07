using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TLabs.DotnetHelpers
{
    public static class FlurlExtensions
    {
        private static string _gatewayUrl;
        private static ILogger _logger;

        public static void InitFlurl(this IServiceCollection services, string gatewayUrl)
        {
            _gatewayUrl = gatewayUrl;
            _logger = LoggerFactory.Create(builder => { builder.AddConsole(); })
                .CreateLogger<FlurlCall>();

            FlurlHttp.Configure(settings =>
            {
                settings.JsonSerializer = new NewtonsoftJsonSerializer(new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });

                // log info of error request
                settings.OnErrorAsync = async (FlurlCall call) =>
                {
                    const int maxLength = 500;
                    string body = call.RequestBody?.Substring(0, Math.Min(call.RequestBody?.Length ?? 0, maxLength));
                    string responseContent = null;
                    if (call.HttpResponseMessage?.Content != null)
                    {
                        responseContent = await call.HttpResponseMessage.Content.ReadAsStringAsync() ?? "";
                        responseContent = responseContent.Substring(0, Math.Min(responseContent.Length, maxLength));
                    }

                    _logger.LogError($"Error: {call.Exception.Message}, " +
                        $"{(call.Duration.HasValue ? $"duration:{call.Duration}" : "")}" +
                        $"{(string.IsNullOrEmpty(body) ? "" : $"\nbody: {body}")}" +
                        $"{(string.IsNullOrEmpty(responseContent) ? "" : $"\nresponseContent: {responseContent}\n")}");
                };
            });
        }

        // Extend POST and PUT requests to specify response type in a single call

        public static async Task<T> PostJsonAsync<T>(this IFlurlRequest request, object data,
            CancellationToken cancellationToken = default,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
            if (typeof(T) == typeof(string)) // if string then parse response as string and convert as T
                return (T)(object)await request
                    .PostJsonAsync(data, cancellationToken, completionOption).ReceiveString();
            else
                return await request.PostJsonAsync(data, cancellationToken, completionOption).ReceiveJson<T>();
        }

        public static async Task<T> PutJsonAsync<T>(this IFlurlRequest request, object data,
            CancellationToken cancellationToken = default,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
            if (typeof(T) == typeof(string)) // if string then parse response as string and convert as T
                return (T)(object)await request
                    .PutJsonAsync(data, cancellationToken, completionOption).ReceiveString();
            else
                return await request.PutJsonAsync(data, cancellationToken, completionOption).ReceiveJson<T>();
        }

        // Add Delete requests with body

        public static async Task<IFlurlResponse> DeleteJsonAsync(this IFlurlRequest request, object data,
            CancellationToken cancellationToken = default,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
            return await request.SendJsonAsync(HttpMethod.Delete, data, cancellationToken, completionOption);
        }

        public static async Task<T> DeleteJsonAsync<T>(this IFlurlRequest request, object data,
            CancellationToken cancellationToken = default,
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
            if (typeof(T) == typeof(string)) // if string then parse response as string and convert as T
                return (T)(object)await request
                    .SendJsonAsync(HttpMethod.Delete, data, cancellationToken, completionOption).ReceiveString();
            else
                return await request
                    .SendJsonAsync(HttpMethod.Delete, data, cancellationToken, completionOption).ReceiveJson<T>();
        }

        // Choose to use full url or send to gateway

        public static IFlurlRequest ExternalApi(this Url url) => new FlurlRequest(url);

        public static IFlurlRequest ExternalApi(this string url) => ExternalApi(new Url(url));

        public static IFlurlRequest InternalApi(this Url url) => new FlurlRequest(Url.Combine(_gatewayUrl, url));

        public static IFlurlRequest InternalApi(this string url) => InternalApi(new Url(url));

        // Wrap response in QueryResult

        public static async Task<QueryResult> GetQueryResult(this Task<IFlurlResponse> request)
        {
            try
            {
                await request;
                return QueryResult.CreateSucceeded();
            }
            catch (FlurlHttpException e)
            {
                string responseString = await e.GetResponseStringAsync();
                return QueryResult.CreateFailedLogic(responseString, $"{e.Message}");
            }
            catch (Exception e)
            {
                return QueryResult.CreateFailed(e.Message);
            }
        }

        public static async Task<QueryResult<T>> GetQueryResult<T>(this Task<T> request)
        {
            try
            {
                return QueryResult<T>.CreateSucceeded(await request);
            }
            catch (FlurlHttpException e)
            {
                string responseString = await e.GetResponseStringAsync();
                return QueryResult<T>.CreateFailedLogic(responseString, $"{e.Message}");
            }
            catch (Exception e)
            {
                return QueryResult<T>.CreateFailed(e.Message);
            }
        }
    }
}
