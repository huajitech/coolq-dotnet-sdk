using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义成员。
    /// </summary>
    public interface IMember : IUser, IAliased, ITimedMuteable, IEquatable<IMember?>
    {
        /// <summary>
        /// 获取一个值，指示当前 <see cref="IMember"/> 实例是否可以编辑 <see cref="IAliased.Alias"/>。
        /// </summary>
        bool CanEditAlias { get; }

        /// <summary>
        /// 获取当前 <see cref="IMember"/> 实例的自定义头衔。
        /// 若当前 <see cref="IMember"/> 实例没有自定义头衔，则为 <see langword="null"/>。
        /// </summary>
        CustomTitle? CustomTitle { get; }

        /// <summary>
        /// 获取当前 <see cref="IMember"/> 实例加入 <see cref="IGroup"/> 的时间。
        /// </summary>
        DateTime TimeJoined { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="IMember"/> 实例是否有不良记录。
        /// </summary>
        bool HasBadRecord { get; }

        /// <summary>
        /// 获取或设置一个值，指示当前 <see cref="IMember"/> 实例是否为管理员。
        /// 若当前 <see cref="IMember"/> 实例是群主，则该属性也返回 <see langword="true"/>。
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
        /// 设置当前 <see cref="IMember"/> 实例的别名，即 <see cref="IAliased.Alias"/> 属性的值。
        /// 如果 <paramref name="alias"/> 为 <see langword="null"/>，则移除当前 <see cref="IMember"/> 实例的别名。
        /// </summary>
        /// <param name="alias">要设置的别名。</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Naming", "CA1716:标识符不应与关键字匹配", Justification = "<挂起>")]
        void SetAlias(string? alias);

        /// <summary>
        /// 将当前 <see cref="IMember"/> 实例踢出所在群。
        /// </summary>
        /// <param name="ignoreFurtherRequests">
        /// 如果不再接收当前 <see cref="IMember"/> 实例的 <see cref="IMembershipRequest"/>，则为 <see langword="true"/>；否则为 <see langword="false"/>。
        /// </param>
        void Kick(bool ignoreFurtherRequests = false);

        /// <summary>
        /// 使当前 <see cref="IMember"/> 实例成为管理员。
        /// </summary>
        void MakeAdministrator();

        /// <summary>
        /// 使当前 <see cref="IMember"/> 实例不再是管理员。
        /// </summary>
        void UnmakeAdministrator();

        /// <summary>
        /// 设置当前 <see cref="IMember"/> 实例的自定义头衔，即 <see cref="CustomTitle"/> 属性的值。
        /// 如果 <paramref name="title"/> 为 <see langword="null"/>，则移除当前 <see cref="IMember"/> 实例的自定义头衔。
        /// </summary>
        /// <param name="title">要设置的头衔。</param>
        void SetCustomTitle(CustomTitle? title);
    }
}