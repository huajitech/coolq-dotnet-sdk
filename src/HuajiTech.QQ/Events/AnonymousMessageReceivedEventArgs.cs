namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="IMessageEventSource.AnonymousMessageReceived"/> 事件提供数据。
    /// </summary>
    public class AnonymousMessageReceivedEventArgs : RoutedEventArgs
    {
        public AnonymousMessageReceivedEventArgs(
            Message message, Group source, AnonymousMember sender)
        {
            Message = message;
            Source = source;
            Sender = sender;
        }

        /// <summary>
        /// 获取消息。
        /// </summary>
        public Message Message { get; }

        /// <summary>
        /// 获取发送者。
        /// </summary>
        public AnonymousMember Sender { get; }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public Group Source { get; }
    }
}