using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="IFriendEventSource.FriendRequested"/> 事件提供数据。
    /// </summary>
    public class FriendshipRequestedEventArgs : TimedEventArgs
    {
        public FriendshipRequestedEventArgs(
            DateTime time, IUser requester, IFriendshipRequest request)
            : base(time)
        {
            Requester = requester;
            Request = request;
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