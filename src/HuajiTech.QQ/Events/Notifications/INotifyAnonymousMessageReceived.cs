using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义匿名消息接收事件。
    /// </summary>
    public interface INotifyAnonymousMessageReceived
    {
        /// <summary>
        /// 在收到匿名消息时引发。
        /// </summary>
        event EventHandler<AnonymousMessageReceivedEventArgs> AnonymousMessageReceived;
    }
}