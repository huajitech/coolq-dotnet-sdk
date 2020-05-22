using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义成员。
    /// </summary>
    public interface IMember : IUser, IAliased, ITimedMuteable, IEquatable<IMember?>
    {
        /// <summary>
        /// 获取当前 <see cref="IMember"/> 实例的年龄。
        /// </summary>
        int Age { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="IMember"/> 实例是否可以编辑 <see cref="IAliased.Alias"/>。
        /// </summary>
        bool CanEditAlias { get; }

        /// <summary>
        /// 获取当前 <see cref="IMember"/> 实例的自定义头衔。
        /// </summary>
        CustomTitle? CustomTitle { get; }

        /// <summary>
        /// 获取当前 <see cref="IMember"/> 实例加入 <see cref="IGroup"/> 的时间。
        /// </summary>
        DateTime TimeEntered { get; }

        /// <summary>
        /// 获取当前 <see cref="IMember"/> 实例的性别。
        /// </summary>
        Gender Gender { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="IMember"/> 实例是否有不良记录。
        /// </summary>
        bool HasBadRecord { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="IMember"/> 实例是否为管理员或群主。
        /// </summary>
        bool IsAdministrator { get; }

        /// <summary>
        /// 获取当前 <see cref="IMember"/> 实例的最后发言时间。
        /// </summary>
        DateTime LastSpeakTime { get; }

        /// <summary>
        /// 获取当前 <see cref="IMember"/> 实例的等级。
        /// </summary>
        string? Level { get; }

        /// <summary>
        /// 获取当前 <see cref="IMember"/> 实例的位置。
        /// </summary>
        string? Location { get; }

        /// <summary>
        /// 获取当前 <see cref="IMember"/> 实例的角色。
        /// </summary>
        MemberRole Role { get; }

        /// <summary>
        /// 获取当前 <see cref="IMember"/> 实例的所属 <see cref="IGroup"/> 实例。
        /// </summary>
        IGroup Group { get; }

        /// <summary>
        /// 将当前 <see cref="IMember"/> 实例踢出所在群。
        /// </summary>
        /// <param name="disallowRejoin">如果不再接收当前 <see cref="IMember"/> 实例的 <see cref="IEntranceRequest"/>，则为 <c>true</c>；否则为 <c>false</c>。</param>
        void Kick(bool disallowRejoin = false);

        /// <summary>
        /// 设置当前 <see cref="IMember"/> 实例的 <see cref="IAliased.Alias"/>。
        /// </summary>
        /// <param name="alias">要设置的 <see cref="IAliased.Alias"/> 的值。</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Naming", "CA1716:标识符不应与关键字匹配", Justification = "<挂起>")]
        void SetAlias(string alias);

        /// <summary>
        /// 将当前 <see cref="IMember"/> 实例设为 <see cref="Group"/> 的管理员。
        /// </summary>
        void SetAsAdministrator();

        /// <summary>
        /// 设置当前 <see cref="IMember"/> 实例的 <see cref="CustomTitle"/>。
        /// </summary>
        /// <param name="title">要设置的 <see cref="CustomTitle"/> 的值。</param>
        void SetCustomTitle(CustomTitle title);

        /// <summary>
        /// 解除当前 <see cref="IMember"/> 实例在 <see cref="Group"/> 的管理员身份。
        /// </summary>
        void UnsetAsAdministrator();
    }
}