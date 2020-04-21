using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 提供用于记录日志的方法的静态类。
    /// 即使记录失败，此类的方法也不会引发任何异常。
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Performance", "CA1806:不要忽略方法结果", Justification = "<挂起>")]
    public static class Logger
    {
        /// <summary>
        /// 记录一条日志。
        /// </summary>
        /// <param name="level">日志的等级。</param>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public static void Log(LogLevel level, string type, string message)
        {
            NativeMethods.Log(Bot.AuthCode, level, type, message);
        }

        /// <summary>
        /// 记录一条调试信息。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public static void LogDebug(string type, string message)
        {
            Log(LogLevel.Debug, type, message);
        }

        /// <summary>
        /// 记录一条调试信息。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        public static void LogDebug(string message)
        {
            LogDebug(Resources.Debug, message);
        }

        /// <summary>
        /// 记录一条信息。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public static void LogInfo(string type, string message)
        {
            Log(LogLevel.Info, type, message);
        }

        /// <summary>
        /// 记录一条信息。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        public static void LogInfo(string message)
        {
            LogInfo(Resources.Info, message);
        }

        /// <summary>
        /// 记录一个成功。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public static void LogSuccess(string type, string message)
        {
            Log(LogLevel.Success, type, message);
        }

        /// <summary>
        /// 记录一个成功。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        public static void LogSuccess(string message)
        {
            LogSuccess(Resources.Success, message);
        }

        /// <summary>
        /// 记录一个接收。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public static void LogReceiving(string type, string message)
        {
            Log(LogLevel.Receiving, type, message);
        }

        /// <summary>
        /// 记录一个接收。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        public static void LogReceiving(string message)
        {
            LogReceiving(Resources.Receiving, message);
        }

        /// <summary>
        /// 记录一个发送。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public static void LogSending(string type, string message)
        {
            Log(LogLevel.Sending, type, message);
        }

        /// <summary>
        /// 记录一个发送。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        public static void LogSending(string message)
        {
            LogSending(Resources.Sending, message);
        }

        /// <summary>
        /// 记录一个警告。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public static void LogWarning(string type, string message)
        {
            Log(LogLevel.Warning, type, message);
        }

        /// <summary>
        /// 记录一个警告。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        public static void LogWarning(string message)
        {
            LogWarning(Resources.Warning, message);
        }

        /// <summary>
        /// 将异常记录为一个警告。
        /// </summary>
        /// <param name="exception">要记录的异常。</param>
        public static void LogWarning(this Exception exception)
        {
            if (exception is null)
            {
                return;
            }

            LogWarning(Resources.Exception, exception.ToString());
        }

        /// <summary>
        /// 记录一个错误。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        public static void LogError(string type, string message)
        {
            Log(LogLevel.Error, type, message);
        }

        /// <summary>
        /// 记录一个错误。
        /// </summary>
        /// <param name="message">日志的消息。</param>
        public static void LogError(string message)
        {
            LogError(Resources.Error, message);
        }

        /// <summary>
        /// 将异常记录为一个错误。
        /// </summary>
        /// <param name="exception">要记录的异常。</param>
        public static void LogError(this Exception exception)
        {
            if (exception is null)
            {
                return;
            }

            LogError(Resources.Exception, exception.ToString());
        }

        /// <summary>
        /// 记录一个致命错误。
        /// </summary>
        /// <param name="message">致命错误的消息。</param>
        public static void LogFatal(string message)
        {
            NativeMethods.LogFatal(Bot.AuthCode, message);
        }
    }
}