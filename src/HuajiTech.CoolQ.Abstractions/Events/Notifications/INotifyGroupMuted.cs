using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义群禁言事件。
    /// </summary>
    public interface INotifyGroupMuted
    {
        /// <summary>
        /// 在禁言时引发。
        /// </summary>
        event EventHandler<GroupMuteEventArgs> GroupMuted;
    }
}