using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义成员加入事件。
    /// </summary>
    public interface INotifyMemberJoined
    {
        /// <summary>
        /// 在成员加入时引发。
        /// </summary>
        event EventHandler<GroupEventArgs> MemberJoined;
    }
}