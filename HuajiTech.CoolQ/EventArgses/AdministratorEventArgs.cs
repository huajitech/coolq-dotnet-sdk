using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 为 <see cref="Group.AdministratorAdded"/> 和 <see cref="Group.AdministratorRemoved"/> 事件提供数据。
    /// </summary>
    public class AdministratorEventArgs : RoutedEventArgs
    {
        public AdministratorEventArgs(
            DateTime time, Group source, Member affectee)
        {
            Time = time;
            Source = source;
            Affectee = affectee;
        }

        /// <summary>
        /// 获取被操作的成员。
        /// </summary>
        public Member Affectee { get; }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public Group Source { get; }

        /// <summary>
        /// 获取时间。
        /// </summary>
        public DateTime Time { get; }
    }
}