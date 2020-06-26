using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 为 <see cref="INotifyMembershipInvited.MembershipInvited"/> 事件提供数据。
    /// </summary>
    public class MembershipInvitedEventArgs : TimedEventArgs
    {
        public MembershipInvitedEventArgs(
            DateTime time, IGroup target, IUser inviter, IRequest invitation)
            : base(time)
        {
            Target = target;
            Inviter = inviter;
            Invitation = invitation;
        }

        /// <summary>
        /// 获取目标群。
        /// </summary>
        public virtual IGroup Target { get; }

        /// <summary>
        /// 获取邀请用户。
        /// </summary>
        public virtual IUser Inviter { get; }

        /// <summary>
        /// 获取邀请。
        /// </summary>
        public virtual IRequest Invitation { get; }
    }
}