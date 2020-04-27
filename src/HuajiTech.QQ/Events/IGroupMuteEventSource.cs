using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义群禁言事件。
    /// </summary>
    public interface IGroupMuteEventSource
    {
        /// <summary>
        /// 在禁言时引发。
        /// </summary>
        event EventHandler<GroupMuteEventArgs> GroupMuted;

        /// <summary>
        /// 在解除禁言时引发。
        /// </summary>
        event EventHandler<GroupMuteEventArgs> GroupUnmuted;
    }
}