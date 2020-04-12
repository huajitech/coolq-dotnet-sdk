using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 为 <see cref="Group.Muted"/> 和 <see cref="Group.Unmuted"/> 事件提供数据。
    /// </summary>
    public class GroupMuteEventArgs : RoutedEventArgs
    {
        public GroupMuteEventArgs(DateTime time, Group source, Member @operator)
        {
            Time = time;
            Source = source;
            Operator = @operator;
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
        /// 获取操作者。
        /// </summary>
        public Member Operator { get; }
    }
}