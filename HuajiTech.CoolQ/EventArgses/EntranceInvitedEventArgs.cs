using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 为 <see cref="CurrentUser.EntranceInvited"/> 事件提供数据。
    /// </summary>
    public class EntranceInvitedEventArgs : RoutedEventArgs
    {
        public EntranceInvitedEventArgs(
            DateTime time, Group target, User inviter, EntranceInvitation invitation)
        {
            Time = time;
            Target = target;
            Inviter = inviter;
            Invitation = invitation;
        }

        /// <summary>
        /// 获取时间。
        /// </summary>
        public DateTime Time { get; }

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
        public EntranceInvitation Invitation { get; }
    }
}