using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="IMemberMuteEventSource.MemberMuted"/> 事件提供数据。
    /// </summary>
    public class MemberMutedEventArgs : GroupEventArgs
    {
        public MemberMutedEventArgs(
            DateTime time, Group source, Member @operator, Member operatee, TimeSpan duration)
            : base(time, source, @operator, operatee)
        {
            Duration = duration;
        }

        /// <summary>
        /// 获取禁言时长。
        /// </summary>
        public TimeSpan Duration { get; }
    }
}