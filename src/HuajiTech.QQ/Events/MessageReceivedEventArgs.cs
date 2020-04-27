namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="IMessageEventSource.MessageReceived"/> 事件提供数据。
    /// </summary>
    public class MessageReceivedEventArgs : RoutedEventArgs
    {
        public MessageReceivedEventArgs(IMessage message, IChattable source, IUser sender)
        {
            Message = message;
            Source = source;
            Sender = sender;
        }

        /// <summary>
        /// 获取消息。
        /// </summary>
        public IMessage Message { get; }

        /// <summary>
        /// 获取发送者。
        /// </summary>
        public IUser Sender { get; }

        /// <summary>
        /// 获取来源聊天。
        /// </summary>
        public IChattable Source { get; }
    }
}