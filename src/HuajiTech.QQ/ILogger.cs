namespace HuajiTech.QQ
{
    public interface ILogger
    {
        void Log(LogLevel level, string type, string message);

        void LogDebug(string message);

        void LogDebug(string type, string message);

        void LogError(string message);

        void LogError(string type, string message);

        void LogFatal(string message);

        void LogInfo(string message);

        void LogInfo(string type, string message);

        void LogReceiving(string message);

        void LogReceiving(string type, string message);

        void LogSending(string message);

        void LogSending(string type, string message);

        void LogSuccess(string message);

        void LogSuccess(string type, string message);

        void LogWarning(string message);

        void LogWarning(string type, string message);
    }
}