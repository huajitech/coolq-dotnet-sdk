namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 为 <see cref="INotifyAnonymousMessageReceived.AnonymousMessageReceived"/> 事件提供数据。
    /// </summary>
    public class AnonymousMessageReceivedEventArgs : RoutedEventArgs
    {
        public AnonymousMessageReceivedEventArgs(Message message, IGroup source, IAnonymousMember sender)
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
        /// 获取发送者。
        /// </summary>
        public virtual IAnonymousMember Sender { get; }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public virtual IGroup Source { get; }
    }
}