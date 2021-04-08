using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLabs.DotnetHelpers
{
    public static class EnvironmentExtensions
    {
        public static string GetEnv() => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public static bool IsDev() => GetEnv() == Environments.Development;
        public static bool IsStaging() => GetEnv() == Environments.Staging;
        public static bool IsTesting() => GetEnv() == "Testing";
        public static bool IsProd() => GetEnv() == Environments.Production;

    }
}
