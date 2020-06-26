using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 提供记录日志的方法。
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 记录一条等级为调试的日志。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void LogDebug(string type, string message);

        /// <summary>
        /// 记录一条等级为错误的日志。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void LogError(string type, string message);

        /// <summary>
        /// 记录一条等级为致命的日志。
        /// </summary>
        /// <remarks>
        /// 该方法弹出一个错误提示窗口，该窗口不包括调用堆栈和重载应用选项。
        /// 如非必要，请改为使用 <see cref="RaiseFatal(string)"/>。
        /// </remarks>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        [Obsolete("在适用处使用 RaiseFatal。有关详细信息，请参阅备注部分。")]
        void LogFatal(string type, string message);

        /// <summary>
        /// 记录一条日志。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void Log(string type, string message);

        /// <summary>
        /// 记录一条等级为接收的日志。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void LogReceiving(string type, string message);

        /// <summary>
        /// 记录一条等级为发送的日志。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void LogSending(string type, string message);

        /// <summary>
        /// 记录一条等级为成功的日志。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void LogSuccess(string type, string message);

        /// <summary>
        /// 记录一条等级为警告的日志。
        /// </summary>
        /// <param name="type">日志的类型。</param>
        /// <param name="message">日志的消息。</param>
        void LogWarning(string type, string message);

        /// <summary>
        /// 引发一个致命错误。
        /// </summary>
        /// <param name="message">致命错误的消息。</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design", "CA1030:在适用处使用事件", Justification = "<挂起>")]
        void RaiseFatal(string message);
    }
}