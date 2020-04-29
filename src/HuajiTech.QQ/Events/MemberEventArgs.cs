using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="IGroupMemberEventSource.MemberJoined"/> 和 <see cref="IGroupMemberEventSource.MemberLeft"/> 事件提供数据。
    /// </summary>
    public class MemberEventArgs : GroupEventArgs
    {
        public MemberEventArgs(
            bool isPassive, DateTime time, Group source, Member @operator, Member operatee)
            : base(time, source, @operator, operatee)
        {
            IsPassive = isPassive;
        }

        /// <summary>
        /// 获取一个值，指示成员是否被动入群。
        /// </summary>
        public bool IsPassive { get; }
    }
}