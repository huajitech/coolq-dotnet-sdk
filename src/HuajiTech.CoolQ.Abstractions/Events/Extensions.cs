using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义扩展方法。
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 添加消息接收事件处理程序。
        /// 即同时添加私聊消息接收事件和群消息接收事件的事件处理程序。
        /// </summary>
        /// <param name="messageEventSource">事件源。</param>
        /// <param name="handler">事件处理程序。</param>
        public static void AddMessageReceivedEventHandler(
            this IMessageEventSource messageEventSource,
            EventHandler<MessageReceivedEventArgs> handler)
        {
            if (messageEventSource is null)
            {
                throw new ArgumentNullException(nameof(messageEventSource));
            }

            messageEventSource.UserMessageReceived += new EventHandler<UserMessageReceivedEventArgs>(handler);
            messageEventSource.GroupMessageReceived += new EventHandler<GroupMessageReceivedEventArgs>(handler);
        }

        /// <summary>
        /// 移除消息接收事件处理程序。
        /// 即同时移除私聊消息接收事件和群消息接收事件的事件处理程序。
        /// </summary>
        /// <param name="messageEventSource">事件源。</param>
        /// <param name="handler">事件处理程序。</param>
        public static void RemoveMessageReceivedEventHandler(
            this IMessageEventSource messageEventSource,
            EventHandler<MessageReceivedEventArgs> handler)
        {
            if (messageEventSource is null)
            {
                throw new ArgumentNullException(nameof(messageEventSource));
            }

            messageEventSource.UserMessageReceived -= new EventHandler<UserMessageReceivedEventArgs>(handler);
            messageEventSource.GroupMessageReceived -= new EventHandler<GroupMessageReceivedEventArgs>(handler);
        }
    }
}