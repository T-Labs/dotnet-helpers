using System;
using System.Diagnostics;

namespace TLabs.DotnetHelpers
{
    public class StopwatchOperation : IDisposable
    {
        protected Stopwatch InnerStopwatch { get; set; }
        public string Name { get; set; }
        public Action<string> LogAction { get; set; }

        public StopwatchOperation(string name, Action<string> logAction)
        {
            InnerStopwatch = new Stopwatch();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            LogAction = logAction;

            InnerStopwatch.Start();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (InnerStopwatch == null) return;

            InnerStopwatch.Stop();
            LogAction($"Duration of '{Name}' is {InnerStopwatch.ElapsedMilliseconds} milliseconds.");
            InnerStopwatch = null;
        }
    }
}
