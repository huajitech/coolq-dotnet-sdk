using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 为 <see cref="Member.Muted"/> 事件提供数据。
    /// </summary>
    public class MemberMutedEventArgs : RoutedEventArgs
    {
        public MemberMutedEventArgs(
            DateTime time, Group source, Member @operator, Member affectee, TimeSpan duration)
        {
            Time = time;
            Source = source;
            Operator = @operator;
            Affectee = affectee;
            Duration = duration;
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
        /// 获取被禁言成员。
        /// </summary>
        public Member Affectee { get; }

        /// <summary>
        /// 获取禁言时长。
        /// </summary>
        public TimeSpan Duration { get; }
    }
}