namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="INotifyAnonymousMessageReceived.AnonymousMessageReceived"/> 事件提供数据。
    /// </summary>
    public class AnonymousMessageReceivedEventArgs : RoutedEventArgs
    {
        public AnonymousMessageReceivedEventArgs(
            IMessage message, IGroup source, IAnonymousMember sender)
        {
            Message = message ?? throw new System.ArgumentNullException(nameof(message));
            Source = source ?? throw new System.ArgumentNullException(nameof(source));
            Sender = sender ?? throw new System.ArgumentNullException(nameof(sender));
        }

        /// <summary>
        /// 获取消息。
        /// </summary>
        public IMessage Message { get; }

        /// <summary>
        /// 获取发送者。
        /// </summary>
        public IAnonymousMember Sender { get; }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public IGroup Source { get; }
    }
}