using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 为 <see cref="INotifyMemberMuted.MemberMuted"/> 事件提供数据。
    /// </summary>
    public class MemberMutedEventArgs : GroupEventArgs
    {
        public MemberMutedEventArgs(
            DateTime time, IGroup source, IMember @operator, IMember operatee, TimeSpan duration)
            : base(time, source, @operator, operatee)
        {
            Duration = duration;
        }

        /// <summary>
        /// 获取禁言时长。
        /// </summary>
        public virtual TimeSpan Duration { get; }
    }
}