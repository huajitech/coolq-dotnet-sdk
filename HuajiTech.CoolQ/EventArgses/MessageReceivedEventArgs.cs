namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 为 <see cref="CurrentUser.MessageReceived"/> 事件提供数据。
    /// </summary>
    public class MessageReceivedEventArgs : RoutedEventArgs
    {
        public MessageReceivedEventArgs(Message message, Chat source, User sender)
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
        public User Sender { get; }

        /// <summary>
        /// 获取来源聊天。
        /// </summary>
        public Chat Source { get; }
    }
}