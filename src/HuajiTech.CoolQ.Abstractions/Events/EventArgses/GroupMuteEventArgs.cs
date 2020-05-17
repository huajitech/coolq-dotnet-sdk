using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 为 <see cref="INotifyGroupMuted.GroupMuted"/> 和 <see cref="INotifyGroupUnmuted.GroupUnmuted"/> 事件提供数据。
    /// </summary>
    public class GroupMuteEventArgs : TimedEventArgs
    {
        public GroupMuteEventArgs(DateTime time, IGroup source, IMember @operator)
            : base(time)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Operator = @operator ?? throw new ArgumentNullException(nameof(@operator));
        }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public IGroup Source { get; }

        /// <summary>
        /// 获取操作者。
        /// </summary>
        public IMember Operator { get; }
    }
}