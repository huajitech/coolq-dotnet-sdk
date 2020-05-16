namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="INotifyMessageReceived.MessageReceived"/> 事件提供数据。
    /// </summary>
    public class MessageReceivedEventArgs : RoutedEventArgs
    {
        public MessageReceivedEventArgs(IContentfulMessage message, IChattable source, IUser sender)
        {
            Message = message ?? throw new System.ArgumentNullException(nameof(message));
            Source = source ?? throw new System.ArgumentNullException(nameof(source));
            Sender = sender ?? throw new System.ArgumentNullException(nameof(sender));
        }

        /// <summary>
        /// 获取消息。
        /// </summary>
        public IContentfulMessage Message { get; }

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