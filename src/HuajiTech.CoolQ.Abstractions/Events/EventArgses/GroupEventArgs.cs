using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 为群事件提供数据。
    /// </summary>
    public class GroupEventArgs : TimedEventArgs
    {
        public GroupEventArgs(DateTime time, IGroup source, IMember @operator, IMember operatee)
            : base(time)
        {
            Source = source;
            Operator = @operator;
            Operatee = operatee;
        }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public virtual IGroup Source { get; }

        /// <summary>
        /// 获取操作人。
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Naming", "CA1716:标识符不应与关键字匹配", Justification = "<挂起>")]
        public virtual IMember Operator { get; }

        /// <summary>
        /// 获取被操作人。
        /// </summary>
        public virtual IMember Operatee { get; }
    }
}