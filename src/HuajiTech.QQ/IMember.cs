using System;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义成员。
    /// </summary>
    public interface IMember : IUser, IMemberInfo, ITimedMuteable, IEquatable<IMember?>
    {
        /// <summary>
        /// 获取当前 <see cref="IMember"/> 对象的所属 <see cref="IGroup"/> 对象。
        /// </summary>
        IGroup Group { get; }

        /// <summary>
        /// 将当前 <see cref="IMember"/> 对象踢出所在群。
        /// </summary>
        /// <param name="disallowRejoin">如果不再接收当前 <see cref="IMember"/> 对象的 <see cref="IEntranceRequest"/>，则为 <c>true</c>；否则为 <c>false</c>。</param>
        void Kick(bool disallowRejoin = false);

        /// <summary>
        /// 设置当前 <see cref="IMember"/> 对象的 <see cref="IAliased.Alias"/>。
        /// </summary>
        /// <param name="alias">要设置的 <see cref="IAliased.Alias"/> 的值。</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Naming", "CA1716:标识符不应与关键字匹配", Justification = "<挂起>")]
        void SetAlias(string alias);

        /// <summary>
        /// 将当前 <see cref="IMember"/> 对象设为 <see cref="Group"/> 的管理员。
        /// </summary>
        void SetAsAdministrator();

        /// <summary>
        /// 设置当前 <see cref="IMember"/> 对象的 <see cref="CustomTitle"/>。
        /// </summary>
        /// <param name="title">要设置的 <see cref="CustomTitle"/> 的值。</param>
        void SetCustomTitle(CustomTitle title);

        /// <summary>
        /// 解除当前 <see cref="IMember"/> 对象在 <see cref="Group"/> 的管理员身份。
        /// </summary>
        void UnsetAsAdministrator();
    }
}