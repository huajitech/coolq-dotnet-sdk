using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义成员解除禁言事件。
    /// </summary>
    public interface INotifyMemberUnmuted
    {
        /// <summary>
        /// 在解除禁言时引发。
        /// </summary>
        event EventHandler<GroupEventArgs> MemberUnmuted;
    }
}