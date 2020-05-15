using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义消息接收事件。
    /// </summary>
    public interface INotifyMessageReceived
    {
        /// <summary>
        /// 在收到消息时引发。
        /// </summary>
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
    }
}