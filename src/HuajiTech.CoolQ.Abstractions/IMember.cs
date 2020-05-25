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
        /// 获取或设置当前 <see cref="IMember"/> 实例的别名。
        /// 若当前 <see cref="IMember"/> 实例没有别名，则为 <see langword="null"/>。
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Naming", "CA1716:标识符不应与关键字匹配", Justification = "<挂起>")]
        new string? Alias { get; set; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="IMember"/> 实例是否可以编辑 <see cref="IAliased.Alias"/>。
        /// </summary>
        bool CanEditAlias { get; }

        /// <summary>
        /// 获取当前 <see cref="IMember"/> 实例的自定义头衔。
        /// 若当前 <see cref="IMember"/> 实例没有自定义头衔，则为 <see langword="null"/>。
        /// </summary>
        CustomTitle? CustomTitle { get; set; }

        /// <summary>
        /// 获取当前 <see cref="IMember"/> 实例加入 <see cref="IGroup"/> 的时间。
        /// </summary>
        DateTime TimeJoined { get; }

        /// <summary>
        /// 获取当前 <see cref="IMember"/> 实例的性别。
        /// </summary>
        Gender Gender { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="IMember"/> 实例是否有不良记录。
        /// </summary>
        bool HasBadRecord { get; }

        /// <summary>
        /// 获取或设置一个值，指示当前 <see cref="IMember"/> 实例是否为管理员。
        /// 若当前 <see cref="IMember"/> 对象是群主，则该属性也返回 <see langword="true"/>。
        /// </summary>
        bool IsAdministrator { get; set; }

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
        /// <param name="ignoreFurtherRequests">
        /// 如果不再接收当前 <see cref="IMember"/> 实例的 <see cref="IMembershipRequest"/>，则为 <see langword="true"/>；否则为 <see langword="false"/>。
        /// </param>
        void Kick(bool ignoreFurtherRequests = false);
    }
}