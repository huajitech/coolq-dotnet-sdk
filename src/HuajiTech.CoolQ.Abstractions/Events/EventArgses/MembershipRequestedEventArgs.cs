using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 为 <see cref="INotifyMembershipRequested.MembershipRequested"/> 事件提供数据。
    /// </summary>
    public class MembershipRequestedEventArgs : TimedEventArgs
    {
        public MembershipRequestedEventArgs(
            DateTime time, IGroup source, IUser requester, IMembershipRequest request)
            : base(time)
        {
            Source = source;
            Requester = requester;
            Request = request;
        }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public virtual IGroup Source { get; }

        /// <summary>
        /// 获取请求用户。
        /// </summary>
        public virtual IUser Requester { get; }

        /// <summary>
        /// 获取请求。
        /// </summary>
        public virtual IMembershipRequest Request { get; }
    }
}