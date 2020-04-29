using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="IEntranceInvitationEventSource.EntranceInvited"/> 事件提供数据。
    /// </summary>
    public class EntranceInvitedEventArgs : TimedEventArgs
    {
        public EntranceInvitedEventArgs(
            DateTime time, Group target, User inviter, Request invitation)
            : base(time)
        {
            Target = target;
            Inviter = inviter;
            Invitation = invitation;
        }

        /// <summary>
        /// 获取目标群。
        /// </summary>
        public Group Target { get; }

        /// <summary>
        /// 获取邀请用户。
        /// </summary>
        public User Inviter { get; }

        /// <summary>
        /// 获取邀请。
        /// </summary>
        public Request Invitation { get; }
    }
}