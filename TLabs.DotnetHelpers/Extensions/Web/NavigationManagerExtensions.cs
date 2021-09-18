using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Linq;

namespace TLabs.DotnetHelpers.Extensions
{
    public static class NavigationManagerExtensions
    {
        /// <summary>Use to get queryString value in Blazor</summary>
        public static QueryResult<T> TryGetQueryString<T>(this NavigationManager navManager, string key)
        {
            var uri = navManager.ToAbsoluteUri(navManager.Uri);

            if (!QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var queryValue))
                return QueryResult<T>.CreateFailedLogic($"NotFound");
            try
            {
                T result = ParseHelper.Parse<T>(queryValue.ToString());
                return QueryResult<T>.CreateSucceeded(result);
            }
            catch (Exception e)
            {
                return QueryResult<T>.CreateFailed($"{e.Message}. Key:{key} Value:{queryValue}");
            }
        }
    }
}
