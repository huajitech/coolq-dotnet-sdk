using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 为 <see cref="Member.Unmuted"/> 事件提供数据。
    /// </summary>
    public class MemberUnmutedEventArgs : RoutedEventArgs
    {
        public MemberUnmutedEventArgs(
            DateTime time, Group source, Member @operator, Member affectee)
        {
            Time = time;
            Source = source;
            Operator = @operator;
            Affectee = affectee;
        }

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
        /// 获取被解除禁言成员。
        /// </summary>
        public Member Affectee { get; }
    }
}