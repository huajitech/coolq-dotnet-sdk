namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 为 <see cref="INotifyUserMessageReceived.UserMessageReceived"/> 事件提供数据。
    /// </summary>
    public class UserMessageReceivedEventArgs : MessageReceivedEventArgs
    {
        public UserMessageReceivedEventArgs(Message message, IUser source, IUser sender)
            : base(message, source, sender)
        {
            Source = source;
        }

        /// <summary>
        /// 获取来源用户。
        /// </summary>
        public new virtual IUser Source { get; }
    }
}