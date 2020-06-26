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
            Source = source;
            Operator = @operator;
        }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public virtual IGroup Source { get; }

        /// <summary>
        /// 获取操作者。
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Naming", "CA1716:标识符不应与关键字匹配", Justification = "<挂起>")]
        public virtual IMember Operator { get; }
    }
}