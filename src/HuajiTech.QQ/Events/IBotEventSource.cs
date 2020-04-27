using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义机器人事件。
    /// </summary>
    public interface IBotEventSource
    {
        /// <summary>
        /// 在应用被禁用时引发。
        /// </summary>
        event EventHandler AppDisabling;

        /// <summary>
        /// 在应用被启用时引发。
        /// </summary>
        event EventHandler AppEnabled;

        /// <summary>
        /// 在机器人启动时引发。
        /// </summary>
        event EventHandler BotStarted;

        /// <summary>
        /// 在机器人停止时引发。
        /// </summary>
        event EventHandler BotStopping;
    }
}