using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetHelpers
{
    public class QueryResult
    {
        [JsonProperty] // required for deserialization without public set
        public bool Succeeded { get; protected set; }

        [JsonProperty]
        public List<string> Errors { get; protected set; } = new List<string>();

        public string ErrorsString =>
            Errors.Count == 0 ? null : string.Join(";; ", Errors);

        public void EnsureSuccess()
        {
            if (!Succeeded)
                throw new Exception(ErrorsString);
        }

        public static QueryResult CreateSucceeded()
        {
            return new QueryResult { Succeeded = true };
        }

        public static QueryResult CreateFailed(params string[] errors)
        {
            if (errors.Length == 0)
                throw new ArgumentException($"CreateFailed() 0 errors");
            var result = new QueryResult { Succeeded = false };
            result.Errors.AddRange(errors);
            return result;
        }

        public static QueryResult CreateFailed(QueryResult otherResult) =>
            CreateFailed(otherResult.Errors.ToArray());
    }

    public class QueryResult<T> : QueryResult
    {
        [JsonProperty]
        public T Data { get; protected set; }

        public static QueryResult<T> CreateSucceeded(T data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            return new QueryResult<T>
            {
                Succeeded = true,
                Data = data
            };
        }

        public static new QueryResult<T> CreateFailed(params string[] errors)
        {
            if (errors.Length == 0)
                throw new ArgumentException($"CreateFailed() 0 errors");
            var result = new QueryResult<T> { Succeeded = false };
            result.Errors.AddRange(errors);
            return result;
        }

        public static new QueryResult<T> CreateFailed(QueryResult otherResult) =>
            CreateFailed(otherResult.Errors.ToArray());
    }
}
