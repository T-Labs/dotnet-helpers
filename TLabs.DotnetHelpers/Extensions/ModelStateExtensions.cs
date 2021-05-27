using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace TLabs.DotnetHelpers
{
    public static class ModelStateExtensions
    {
        /// <summary>Get all errors separated by ;</summary>
        public static string ErrorsToString(this ModelStateDictionary state) =>
            string.Join("; ", state.Values.SelectMany(v => v.Errors).Select(_ => _.ErrorMessage));
    }
}
