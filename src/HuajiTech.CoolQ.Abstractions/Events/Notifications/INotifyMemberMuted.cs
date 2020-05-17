using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义成员禁言事件。
    /// </summary>
    public interface INotifyMemberMuted
    {
        /// <summary>
        /// 在禁言时引发。
        /// </summary>
        event EventHandler<MemberMutedEventArgs> MemberMuted;
    }
}