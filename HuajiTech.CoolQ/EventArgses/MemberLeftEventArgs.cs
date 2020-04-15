using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 为 <see cref="Group.MemberLeft"/> 事件提供数据。
    /// </summary>
    public class MemberLeftEventArgs : RoutedEventArgs
    {
        public MemberLeftEventArgs(
            bool isKicked, DateTime time, Group source, Member @operator, User affectee)
        {
            IsPassive = isKicked;
            Time = time;
            Source = source;
            Operator = @operator;
            Affectee = affectee;
        }

        /// <summary>
        /// 获取一个值，指示成员是否被动退群。
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
        /// 获取退群用户。
        /// </summary>
        public User Affectee { get; }
    }
}