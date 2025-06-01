using System;
using System.Diagnostics;

namespace SampleEcomStoreApi.Common.Logging
{
    public class EnterpriseLibraryLogger : ILogger
    {
        public void LogInfo(string message)
        {
            Trace.WriteLine($"[INFO] {DateTime.Now}: {message}");
        }

        public void LogWarning(string message)
        {
            Trace.WriteLine($"[WARNING] {DateTime.Now}: {message}");
        }

        public void LogError(string message)
        {
            Trace.WriteLine($"[ERROR] {DateTime.Now}: {message}");
        }

        public void LogError(string message, Exception exception)
        {
            Trace.WriteLine($"[ERROR] {DateTime.Now}: {message}\r\nException: {exception.Message}\r\nStackTrace: {exception.StackTrace}");
        }

        public void LogDebug(string message)
        {
            Trace.WriteLine($"[DEBUG] {DateTime.Now}: {message}");
        }
    }
}
