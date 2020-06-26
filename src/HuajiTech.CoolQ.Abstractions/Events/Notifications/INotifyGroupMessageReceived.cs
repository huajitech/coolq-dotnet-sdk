using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义群消息接收事件。
    /// </summary>
    public interface INotifyGroupMessageReceived
    {
        /// <summary>
        /// 在收到群消息时引发。
        /// </summary>
        event EventHandler<GroupMessageReceivedEventArgs> GroupMessageReceived;
    }
}