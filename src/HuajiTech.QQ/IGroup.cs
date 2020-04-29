using System.Collections.Generic;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义群。
    /// </summary>
    public interface IGroup
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
        /// 获取当前 <see cref="IGroup"/> 对象的所有成员。
        /// </summary>
        IReadOnlyCollection<Member> GetMembers();

        /// <summary>
        /// 以异步操作获取当前 <see cref="IGroup"/> 对象的所有成员。
        /// </summary>
        Task<IReadOnlyCollection<Member>> GetMembersAsync();
    }
}