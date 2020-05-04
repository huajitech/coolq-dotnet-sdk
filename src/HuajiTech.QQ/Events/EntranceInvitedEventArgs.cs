using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="IEntranceInviteEventSource.EntranceInvited"/> 事件提供数据。
    /// </summary>
    public class EntranceInvitedEventArgs : TimedEventArgs
    {
        public EntranceInvitedEventArgs(
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
        public IGroup Target { get; }

        /// <summary>
        /// 获取邀请用户。
        /// </summary>
        public IUser Inviter { get; }

        /// <summary>
        /// 获取邀请。
        /// </summary>
        public IRequest Invitation { get; }
    }
}