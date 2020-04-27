using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义群成员事件。
    /// </summary>
    public interface IGroupMemberEventSource
    {
        /// <summary>
        /// 在成员加入时引发。
        /// </summary>
        event EventHandler<MemberEventArgs> MemberJoined;

        /// <summary>
        /// 在成员离开时引发。
        /// </summary>
        event EventHandler<MemberEventArgs> MemberLeft;
    }
}