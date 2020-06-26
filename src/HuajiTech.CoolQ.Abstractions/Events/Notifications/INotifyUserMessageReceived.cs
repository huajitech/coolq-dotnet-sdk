using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义私聊消息接收事件。
    /// </summary>
    public interface INotifyUserMessageReceived
    {
        /// <summary>
        /// 在收到私聊消息时引发。
        /// </summary>
        event EventHandler<UserMessageReceivedEventArgs> UserMessageReceived;
    }
}