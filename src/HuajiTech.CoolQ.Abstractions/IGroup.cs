using System.Collections.Generic;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义群。
    /// </summary>
    public interface IGroup : IChattable, IMuteable, IRequestable, IRefreshable
    {
        /// <summary>
        /// 获取当前 <see cref="IGroup"/> 对象的成员容量。
        /// </summary>
        int MemberCapacity { get; }

        /// <summary>
        /// 获取当前 <see cref="IGroup"/> 对象的成员数。
        /// </summary>
        int MemberCount { get; }

        /// <summary>
        /// 禁用当前 <see cref="IGroup"/> 对象的匿名功能。
        /// </summary>
        void DisableAnonymous();

        /// <summary>
        /// 解散当前 <see cref="IGroup"/> 对象。
        /// </summary>
        void Disband();

        /// <summary>
        /// 启用当前 <see cref="IGroup"/> 对象的匿名功能。
        /// </summary>
        void EnableAnonymous();

        /// <summary>
        /// 获取当前 <see cref="IGroup"/> 对象的所有成员。
        /// </summary>
        IReadOnlyCollection<IMember> GetMembers();

        /// <summary>
        /// 离开当前 <see cref="IGroup"/> 对象。
        /// </summary>
        void Leave();
    }
}