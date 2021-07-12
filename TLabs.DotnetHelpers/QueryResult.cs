using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TLabs.DotnetHelpers
{
    public class QueryResult
    {
        public bool Succeeded { get; protected set; }

        public List<string> Errors { get; protected set; } = new List<string>();

        public string LogicError { get; protected set; }

        public string ErrorsString =>
            $"{(LogicError.HasValue() ? $"{LogicError}. " : "")} " +
            $"{(Errors.Count == 0 ? "" : string.Join(";; ", Errors))}";

        public override string ToString() => Succeeded ? $"QueryResult.Success" : $"QueryResult.Error: {ErrorsString}";

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
            return CreateFailedLogic(null, errors);
        }

        public static QueryResult CreateFailed(QueryResult otherResult) =>
            CreateFailedLogic(otherResult.LogicError, otherResult.Errors.ToArray());

        public static QueryResult CreateFailedLogic(string logicError, params string[] errors)
        {
            var result = new QueryResult { Succeeded = false };
            result.LogicError = logicError;
            result.Errors.AddRange(errors);
            return result;
        }
    }

    public class QueryResult<T> : QueryResult
    {
        public T Data { get; protected set; }

        public override string ToString() => Succeeded ? $"QueryResult.Data: {Data}" : $"QueryResult.Error: {ErrorsString}";

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
        public static QueryResult<T> CreateFailed(params string[] errors)
        {
            if (errors.Length == 0)
                throw new ArgumentException($"CreateFailed() 0 errors");
            return CreateFailedLogic(null, errors);
        }

        public static new QueryResult<T> CreateFailed(QueryResult otherResult) =>
            CreateFailedLogic(otherResult.LogicError, otherResult.Errors.ToArray());

        public static new QueryResult<T> CreateFailedLogic(string logicError, params string[] errors)
        {
            var result = new QueryResult<T> { Succeeded = false };
            result.LogicError = logicError;
            result.Errors.AddRange(errors);
            return result;
        }
    }
}
