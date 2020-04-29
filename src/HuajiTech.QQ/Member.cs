using System;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示成员。
    /// 此类为抽象类。
    /// </summary>
    public abstract class Member : User, IMember, IAliased
    {
        /// <summary>
        /// 以指定的号码和群初始化一个 <see cref="Member"/> 类的新实例。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="group"/> 为 <c>null</c>。</exception>
        protected Member(long number, Group group)
            : base(number)
        {
            Group = group ?? throw new ArgumentNullException(nameof(group));
        }

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的年龄。
        /// </summary>
        public abstract int Age { get; }

        public abstract string Alias { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="Member"/> 对象是否可以编辑 <see cref="Alias"/>。
        /// </summary>
        public abstract bool CanEditAlias { get; }

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的自定义头衔。
        /// </summary>
        public abstract CustomTitle CustomTitle { get; }

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的显示名称。
        /// </summary>
        public override string DisplayName => Alias ?? Nickname;

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象加入 <see cref="Group"/> 的时间。
        /// </summary>
        public abstract DateTime TimeEntered { get; }

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的性别。
        /// </summary>
        public abstract Gender Gender { get; }

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的所属 <see cref="QQ.Group"/> 对象。
        /// </summary>
        public Group Group { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="Member"/> 对象是否有不良记录。
        /// </summary>
        public abstract bool HasBadRecord { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="Member"/> 对象是否为管理员或群主。
        /// </summary>
        public virtual bool IsAdministrator => Role == MemberRole.Administrator || Role == MemberRole.Owner;

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的最后发言时间。
        /// </summary>
        public abstract DateTime LastSpeakTime { get; }

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的等级。
        /// </summary>
        public abstract string Level { get; }

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的位置。
        /// </summary>
        public abstract string Location { get; }

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的角色。
        /// </summary>
        public abstract MemberRole Role { get; }

        public virtual bool Equals(IMember other) => base.Equals(other) && other.Group.Equals(Group);

        public override bool Equals(object obj) => Equals(obj as IMember);

        public override int GetHashCode() => base.GetHashCode() ^ Group.GetHashCode();

        /// <summary>
        /// 将当前 <see cref="Member"/> 对象踢出所在群。
        /// </summary>
        /// <param name="disallowRejoin">如果不再接收当前 <see cref="Member"/> 对象的 <see cref="EntranceRequest"/>，则为 <c>true</c>；否则为 <c>false</c>。</param>
        public abstract void Kick(bool disallowRejoin = false);

        /// <summary>
        /// 以异步操作将当前 <see cref="Member"/> 对象踢出所在群。
        /// </summary>
        public virtual Task KickAsync(bool disallowRejoin = false) => Task.Run(() => Kick(disallowRejoin));

        public abstract void Mute(TimeSpan duration);

        public abstract void Mute();

        public virtual Task MuteAsync(TimeSpan duration) => Task.Run(() => Mute(duration));

        public virtual Task MuteAsync() => Task.Run(Mute);

        /// <summary>
        /// 设置当前 <see cref="Member"/> 对象的 <see cref="Alias"/>。
        /// </summary>
        /// <param name="alias">要设置的 <see cref="Alias"/> 的值。</param>
        public abstract void SetAlias(string alias);

        /// <summary>
        /// 以异步操作设置当前 <see cref="Member"/> 对象的 <see cref="Alias"/>。
        /// </summary>
        /// <param name="alias">要设置的 <see cref="Alias"/> 的值。</param>
        public virtual Task SetAliasAsync(string alias) => Task.Run(() => SetAlias(alias));

        /// <summary>
        /// 将当前 <see cref="Member"/> 对象设为 <see cref="Group"/> 的管理员。
        /// </summary>
        public abstract void SetAsAdministrator();

        /// <summary>
        /// 以异步操作将当前 <see cref="Member"/> 对象设为 <see cref="Group"/> 的管理员。
        /// </summary>
        public virtual Task SetAsAdministratorAsync() => Task.Run(SetAsAdministrator);

        /// <summary>
        /// 设置当前 <see cref="Member"/> 对象的 <see cref="CustomTitle"/>。
        /// </summary>
        /// <param name="title">要设置的 <see cref="CustomTitle"/> 的值。</param>
        public abstract void SetCustomTitle(CustomTitle title);

        /// <summary>
        /// 以异步操作设置当前 <see cref="Member"/> 对象的 <see cref="CustomTitle"/>。
        /// </summary>
        /// <param name="title">要设置的 <see cref="CustomTitle"/> 的值。</param>
        public virtual Task SetCustomTitleAsync(CustomTitle title) => Task.Run(() => SetCustomTitle(title));

        public abstract void Unmute();

        public virtual Task UnmuteAsync() => Task.Run(Unmute);

        /// <summary>
        /// 解除当前 <see cref="Member"/> 对象在 <see cref="Group"/> 的管理员身份。
        /// </summary>
        public abstract void UnsetAsAdministrator();

        /// <summary>
        /// 以异步操作解除当前 <see cref="Member"/> 对象在 <see cref="Group"/> 的管理员身份。
        /// </summary>
        public virtual Task UnsetAsAdministratorAsync() => Task.Run(UnsetAsAdministrator);
    }
}