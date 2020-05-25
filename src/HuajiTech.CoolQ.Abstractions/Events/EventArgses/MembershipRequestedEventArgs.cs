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
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Requester = requester ?? throw new ArgumentNullException(nameof(requester));
            Request = request ?? throw new ArgumentNullException(nameof(request));
        }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public IGroup Source { get; }

        /// <summary>
        /// 获取请求用户。
        /// </summary>
        public IUser Requester { get; }

        /// <summary>
        /// 获取请求。
        /// </summary>
        public IMembershipRequest Request { get; }
    }
}