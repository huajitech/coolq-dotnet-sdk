using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义机器人停止事件。
    /// </summary>
    public interface INotifyBotStopping
    {
        /// <summary>
        /// 在机器人停止时引发。
        /// </summary>
        event EventHandler BotStopping;
    }
}