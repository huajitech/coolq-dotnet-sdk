namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 指定日志级别。
    /// </summary>
    internal enum LogLevel
    {
        /// <summary>
        /// 调试。
        /// </summary>
        Debug = 0,

        /// <summary>
        /// 信息。
        /// </summary>
        Info = 10,

        /// <summary>
        /// 成功。
        /// </summary>
        Success = 11,

        /// <summary>
        /// 接收。
        /// </summary>
        Receiving = 12,

        /// <summary>
        /// 发送。
        /// </summary>
        Sending = 13,

        /// <summary>
        /// 警告。
        /// </summary>
        Warning = 20,

        /// <summary>
        /// 错误。
        /// </summary>
        Error = 30,

        /// <summary>
        /// 致命错误。
        /// </summary>
        Fatal = 40
    }
}