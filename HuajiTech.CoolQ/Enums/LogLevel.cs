namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 指定日志等级。
    /// </summary>
    public enum LogLevel
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