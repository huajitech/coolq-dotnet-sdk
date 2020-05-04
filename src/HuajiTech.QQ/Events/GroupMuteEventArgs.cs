using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="IGroupMuteEventSource.GroupMuted"/> 和 <see cref="IGroupMuteEventSource.GroupUnmuted"/> 事件提供数据。
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
        public IGroup Source { get; }

        /// <summary>
        /// 获取操作者。
        /// </summary>
        public IMember Operator { get; }
    }
}