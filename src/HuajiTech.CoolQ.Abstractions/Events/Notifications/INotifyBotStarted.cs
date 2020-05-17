using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义机器人启动事件。
    /// </summary>
    public interface INotifyBotStarted
    {
        /// <summary>
        /// 在机器人启动时引发。
        /// </summary>
        event EventHandler BotStarted;
    }
}