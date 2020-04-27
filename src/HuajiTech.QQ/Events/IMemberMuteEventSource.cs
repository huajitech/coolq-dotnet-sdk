using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义成员禁言事件。
    /// </summary>
    public interface IMemberMuteEventSource
    {
        /// <summary>
        /// 在禁言时引发。
        /// </summary>
        event EventHandler<MemberMutedEventArgs> MemberMuted;

        /// <summary>
        /// 在解除禁言时引发。
        /// </summary>
        event EventHandler<GroupEventArgs> MemberUnmuted;
    }
}