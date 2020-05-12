using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="IFriendEventSource.FriendAdded"/> 事件提供数据。
    /// </summary>
    public class FriendAddedEventArgs : TimedEventArgs
    {
        public FriendAddedEventArgs(DateTime time, IFriend operatee)
            : base(time)
        {
            Operatee = operatee;
        }

        /// <summary>
        /// 获取添加的好友。
        /// </summary>
        public IFriend Operatee { get; }
    }
}