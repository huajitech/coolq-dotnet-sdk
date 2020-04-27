using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义消息事件。
    /// </summary>
    public interface IMessageEventSource
    {
        /// <summary>
        /// 在收到消息时引发。
        /// </summary>
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        /// 在收到匿名消息时引发。
        /// </summary>
        event EventHandler<AnonymousMessageReceivedEventArgs> AnonymousMessageReceived;
    }
}