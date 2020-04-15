using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 为 <see cref="Group.MemberJoined"/> 事件提供数据。
    /// </summary>
    public class MemberJoinedEventArgs : RoutedEventArgs
    {
        public MemberJoinedEventArgs(
            bool isInvited, DateTime time, Group source, Member @operator, Member affectee)
        {
            IsPassive = isInvited;
            Time = time;
            Source = source;
            Operator = @operator;
            Affectee = affectee;
        }

        /// <summary>
        /// 获取一个值，指示成员是否被动入群。
        /// </summary>
        public bool IsPassive { get; }

        /// <summary>
        /// 获取时间。
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public Group Source { get; }

        /// <summary>
        /// 获取操作人。
        /// </summary>
        public Member Operator { get; }

        /// <summary>
        /// 获取入群成员。
        /// </summary>
        public Member Affectee { get; }
    }
}