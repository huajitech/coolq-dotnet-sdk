using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义入群邀请事件。
    /// </summary>
    public interface INotifyMembershipInvited
    {
        /// <summary>
        /// 在被邀请加群时引发。
        /// </summary>
        event EventHandler<MembershipInvitedEventArgs> MembershipInvited;
    }
}