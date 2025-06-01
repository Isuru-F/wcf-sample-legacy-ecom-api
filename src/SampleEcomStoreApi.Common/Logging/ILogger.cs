using System;

namespace SampleEcomStoreApi.Common.Logging
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogError(string message, Exception exception);
        void LogDebug(string message);
    }
}
