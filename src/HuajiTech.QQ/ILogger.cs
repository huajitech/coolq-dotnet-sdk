using System;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义记录日志的方法。
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 记录一条日志。
        /// </summary>
        /// <param name="level">日志的等级。</param>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void Log(LogLevel level, string type, string message);

        /// <summary>
        /// 记录一条调试信息。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void LogDebug(string type, string message);

        /// <summary>
        /// 记录一条调试信息。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        void LogDebug(string message);

        /// <summary>
        /// 记录一个错误。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void LogError(string type, string message);

        /// <summary>
        /// 记录一个错误。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        void LogError(string message);

        /// <summary>
        /// 将异常记录为一个错误。
        /// </summary>
        /// <param name="exception">要记录的异常。</param>
        void LogError(Exception exception);

        /// <summary>
        /// 记录一个致命错误。
        /// </summary>
        /// <param name="message">致命错误的消息。</param>
        void LogFatal(string message);

        /// <summary>
        /// 记录一条信息。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void LogInfo(string type, string message);

        /// <summary>
        /// 记录一条信息。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        void LogInfo(string message);

        /// <summary>
        /// 记录一个接收。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void LogReceiving(string type, string message);

        /// <summary>
        /// 记录一个接收。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        void LogReceiving(string message);

        /// <summary>
        /// 记录一个发送。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void LogSending(string type, string message);

        /// <summary>
        /// 记录一个发送。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        void LogSending(string message);

        /// <summary>
        /// 记录一个成功。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void LogSuccess(string type, string message);

        /// <summary>
        /// 记录一个成功。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        void LogSuccess(string message);

        /// <summary>
        /// 记录一个警告。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void LogWarning(string type, string message);

        /// <summary>
        /// 记录一个警告。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        void LogWarning(string message);

        /// <summary>
        /// 将异常记录为一个警告。
        /// </summary>
        /// <param name="exception">要记录的异常。</param>
        void LogWarning(Exception exception);
    }
}