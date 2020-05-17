using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 为 <see cref="INotifyFriendAdding.FriendAdding"/> 事件提供数据。
    /// </summary>
    public class FriendAddingEventArgs : TimedEventArgs
    {
        public FriendAddingEventArgs(
            DateTime time, IUser requester, IFriendshipRequest request)
            : base(time)
        {
            Requester = requester ?? throw new ArgumentNullException(nameof(requester));
            Request = request ?? throw new ArgumentNullException(nameof(request));
        }

        /// <summary>
        /// 获取请求用户。
        /// </summary>
        public IUser Requester { get; }

        /// <summary>
        /// 获取请求。
        /// </summary>
        public IFriendshipRequest Request { get; }
    }
}