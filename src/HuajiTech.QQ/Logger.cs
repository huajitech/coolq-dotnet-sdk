using System;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 提供记录日志的方法。
    /// </summary>
    public abstract class Logger : ILogger
    {
        public abstract void Log(LogLevel level, string? type, string? message);

        public virtual void LogDebug(string? type, string? message) => Log(LogLevel.Debug, type, message);

        public virtual void LogDebug(string? message) => LogDebug(Resources.Debug, message);

        public virtual void LogInfo(string? type, string? message) => Log(LogLevel.Info, type, message);

        public virtual void LogInfo(string? message) => LogInfo(Resources.Info, message);

        public virtual void LogSuccess(string? type, string? message) => Log(LogLevel.Success, type, message);

        public virtual void LogSuccess(string? message) => LogSuccess(Resources.Success, message);

        public virtual void LogReceiving(string? type, string? message) => Log(LogLevel.Receiving, type, message);

        public virtual void LogReceiving(string? message) => LogReceiving(Resources.Receiving, message);

        public virtual void LogSending(string? type, string? message) => Log(LogLevel.Sending, type, message);

        public virtual void LogSending(string? message) => LogSending(Resources.Sending, message);

        public virtual void LogWarning(string? type, string? message) => Log(LogLevel.Warning, type, message);

        public virtual void LogWarning(string? message) => LogWarning(Resources.Warning, message);

        public virtual void LogWarning(Exception? exception) => LogWarning(Resources.Exception, exception?.ToString());

        public virtual void LogError(string? type, string? message) => Log(LogLevel.Error, type, message);

        public virtual void LogError(string? message) => LogError(Resources.Error, message);

        public virtual void LogError(Exception? exception) => LogError(Resources.Exception, exception?.ToString());

        public abstract void LogFatal(string? message);
    }
}