namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 为 <see cref="INotifyGroupMessageReceived.GroupMessageReceived"/> 事件提供数据。
    /// </summary>
    public class GroupMessageReceivedEventArgs : MessageReceivedEventArgs
    {
        public GroupMessageReceivedEventArgs(Message message, IGroup source, IMember sender)
            : base(message, source, sender)
        {
            Source = source;
            Sender = sender;
        }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public new virtual IGroup Source { get; }

        /// <summary>
        /// 获取发送成员。
        /// </summary>
        public new virtual IMember Sender { get; }
    }
}