using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义成员离开事件。
    /// </summary>
    public interface INotifyMemberLeft
    {
        /// <summary>
        /// 在成员离开时引发。
        /// </summary>
        event EventHandler<GroupEventArgs> MemberLeft;
    }
}