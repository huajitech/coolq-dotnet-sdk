using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="INotifyFriended.Friended"/> 事件提供数据。
    /// </summary>
    public class FriendedEventArgs : TimedEventArgs
    {
        public FriendedEventArgs(DateTime time, IFriend operatee)
            : base(time)
        {
            Operatee = operatee ?? throw new ArgumentNullException(nameof(operatee));
        }

        /// <summary>
        /// 获取添加的好友。
        /// </summary>
        public IFriend Operatee { get; }
    }
}