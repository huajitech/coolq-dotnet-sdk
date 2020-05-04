using System;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义成员信息。
    /// </summary>
    public interface IMemberInfo : IUserInfo, IAliased
    {
        /// <summary>
        /// 获取当前 <see cref="IMemberInfo"/> 对象的年龄。
        /// </summary>
        int Age { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="IMemberInfo"/> 对象是否可以编辑 <see cref="IAliased.Alias"/>。
        /// </summary>
        bool CanEditAlias { get; }

        /// <summary>
        /// 获取当前 <see cref="IMemberInfo"/> 对象的自定义头衔。
        /// </summary>
        CustomTitle CustomTitle { get; }

        /// <summary>
        /// 获取当前 <see cref="IMemberInfo"/> 对象加入 <see cref="IGroup"/> 的时间。
        /// </summary>
        DateTime TimeEntered { get; }

        /// <summary>
        /// 获取当前 <see cref="IMemberInfo"/> 对象的性别。
        /// </summary>
        Gender Gender { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="IMemberInfo"/> 对象是否有不良记录。
        /// </summary>
        bool HasBadRecord { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="IMemberInfo"/> 对象是否为管理员或群主。
        /// </summary>
        bool IsAdministrator { get; }

        /// <summary>
        /// 获取当前 <see cref="IMemberInfo"/> 对象的最后发言时间。
        /// </summary>
        DateTime LastSpeakTime { get; }

        /// <summary>
        /// 获取当前 <see cref="IMemberInfo"/> 对象的等级。
        /// </summary>
        string Level { get; }

        /// <summary>
        /// 获取当前 <see cref="IMemberInfo"/> 对象的位置。
        /// </summary>
        string Location { get; }

        /// <summary>
        /// 获取当前 <see cref="IMemberInfo"/> 对象的角色。
        /// </summary>
        MemberRole Role { get; }
    }
}