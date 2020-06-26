namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 为消息接收事件数据提供基类。
    /// </summary>
    public class MessageReceivedEventArgs : RoutedEventArgs
    {
        public MessageReceivedEventArgs(Message message, IChattable source, IUser sender)
        {
            Message = message;
            Source = source;
            Sender = sender;
        }

        /// <summary>
        /// 获取消息。
        /// </summary>
        public virtual Message Message { get; }

        /// <summary>
        /// 获取来源聊天。
        /// </summary>
        public virtual IChattable Source { get; }

        /// <summary>
        /// 获取发送者。
        /// </summary>
        public virtual IUser Sender { get; }
    }
}