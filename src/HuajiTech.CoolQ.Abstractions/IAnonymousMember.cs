using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义匿名成员。
    /// </summary>
    public interface IAnonymousMember : INamed, IMuteable, IEquatable<IAnonymousMember?>
    {
        /// <summary>
        /// 获取当前 <see cref="IAnonymousMember"/> 实例的标识符。
        /// </summary>
        long Id { get; }

        /// <summary>
        /// 获取当前 <see cref="IAnonymousMember"/> 实例的所属群。
        /// </summary>
        public IGroup Group { get; }
    }
}