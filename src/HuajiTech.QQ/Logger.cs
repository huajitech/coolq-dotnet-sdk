using System;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 提供更友好的记录日志的方法。
    /// 此类为抽象类。
    /// </summary>
    public abstract class Logger
    {
        /// <summary>
        /// 记录一条日志。
        /// </summary>
        /// <param name="level">日志的等级。</param>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public abstract void Log(LogLevel level, string type, string message);

        /// <summary>
        /// 记录一条调试信息。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public virtual void LogDebug(string type, string message) => Log(LogLevel.Debug, type, message);

        /// <summary>
        /// 记录一条调试信息。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        public virtual void LogDebug(string message) => LogDebug(Resources.Debug, message);

        /// <summary>
        /// 记录一条信息。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public virtual void LogInfo(string type, string message) => Log(LogLevel.Info, type, message);

        /// <summary>
        /// 记录一条信息。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        public virtual void LogInfo(string message) => LogInfo(Resources.Info, message);

        /// <summary>
        /// 记录一个成功。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public virtual void LogSuccess(string type, string message) => Log(LogLevel.Success, type, message);

        /// <summary>
        /// 记录一个成功。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        public virtual void LogSuccess(string message) => LogSuccess(Resources.Success, message);

        /// <summary>
        /// 记录一个接收。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public virtual void LogReceiving(string type, string message) => Log(LogLevel.Receiving, type, message);

        /// <summary>
        /// 记录一个接收。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        public virtual void LogReceiving(string message) => LogReceiving(Resources.Receiving, message);

        /// <summary>
        /// 记录一个发送。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public virtual void LogSending(string type, string message) => Log(LogLevel.Sending, type, message);

        /// <summary>
        /// 记录一个发送。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        public virtual void LogSending(string message) => LogSending(Resources.Sending, message);

        /// <summary>
        /// 记录一个警告。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public virtual void LogWarning(string type, string message) => Log(LogLevel.Warning, type, message);

        /// <summary>
        /// 记录一个警告。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        public virtual void LogWarning(string message) => LogWarning(Resources.Warning, message);

        /// <summary>
        /// 将异常记录为一个警告。
        /// </summary>
        /// <param name="exception">要记录的异常。</param>
        public virtual void LogWarning(Exception exception) => LogWarning(Resources.Exception, exception?.ToString());

        /// <summary>
        /// 记录一个错误。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public virtual void LogError(string type, string message) => Log(LogLevel.Error, type, message);

        /// <summary>
        /// 记录一个错误。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        public virtual void LogError(string message) => LogError(Resources.Error, message);

        /// <summary>
        /// 将异常记录为一个错误。
        /// </summary>
        /// <param name="exception">要记录的异常。</param>
        public virtual void LogError(Exception exception) => LogError(Resources.Exception, exception?.ToString());

        /// <summary>
        /// 记录一个致命错误。
        /// </summary>
        /// <param name="message">致命错误的消息。</param>
        public abstract void LogFatal(string message);
    }
}