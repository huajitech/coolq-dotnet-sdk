using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义群解除禁言事件。
    /// </summary>
    public interface INotifyGroupUnmuted
    {
        /// <summary>
        /// 在解除禁言时引发。
        /// </summary>
        event EventHandler<GroupMuteEventArgs> GroupUnmuted;
    }
}