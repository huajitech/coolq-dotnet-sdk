using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为群事件提供数据。
    /// </summary>
    public class GroupEventArgs : TimedEventArgs
    {
        public GroupEventArgs(DateTime time, IGroup source, IMember @operator, IMember operatee)
            : base(time)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Operator = @operator ?? throw new ArgumentNullException(nameof(@operator));
            Operatee = operatee ?? throw new ArgumentNullException(nameof(operatee));
        }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public IGroup Source { get; }

        /// <summary>
        /// 获取操作人。
        /// </summary>
        public IMember Operator { get; }

        /// <summary>
        /// 获取被操作人。
        /// </summary>
        public IMember Operatee { get; }
    }
}