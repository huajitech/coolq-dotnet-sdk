using System;

namespace HuajiTech.CoolQ
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Performance", "CA1806:不要忽略方法结果", Justification = "<挂起>")]
    internal class Logger : ILogger
    {
        internal const LogLevel UnhandledExceptionLogLevel = LogLevel.Error;

        public static void Log(LogLevel level, string? type, string? message)
            => NativeMethods.Bot_Log(Bot.Instance.AuthCode, level, type, message);

        internal static void LogUnhandledException(Exception ex)
            => Log(UnhandledExceptionLogLevel, CoreResources.UnhandledException, ex.ToString());

        public void RaiseFatal(string? message)
            => NativeMethods.Bot_LogFatal(Bot.Instance.AuthCode, message);

        public void LogDebug(string? type, string? message) => Log(LogLevel.Debug, type, message);

        public void Log(string? type, string? message) => Log(LogLevel.Info, type, message);

        public void LogSuccess(string? type, string? message) => Log(LogLevel.Success, type, message);

        public void LogReceiving(string? type, string? message) => Log(LogLevel.Receiving, type, message);

        public void LogSending(string? type, string? message) => Log(LogLevel.Sending, type, message);

        public void LogWarning(string? type, string? message) => Log(LogLevel.Warning, type, message);

        public void LogError(string? type, string? message) => Log(LogLevel.Error, type, message);

        public void LogFatal(string type, string message) => Log(LogLevel.Fatal, type, message);
    }
}