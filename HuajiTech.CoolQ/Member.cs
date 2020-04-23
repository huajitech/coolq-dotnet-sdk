using HuajiTech.CoolQ.DataExchange;
using System;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示成员。
    /// </summary>
    public partial class Member : User
    {
        private MemberInfo _info;

        /// <summary>
        /// 以指定的号码和群初始化一个 <see cref="Member"/> 类的新实例。
        /// </summary>
        /// <param name="number">号码。</param>
        /// <param name="group">所在群聊。</param>
        /// <exception cref="ArgumentNullException"><paramref name="group"/> 为 <c>null</c>。</exception>
        public Member(long number, Group group)
            : base(number)
        {
            Group = group ?? throw new ArgumentNullException(nameof(group));
        }

        internal Member(MemberInfo info)
            : this(info.Number, info.Group)
        {
            _info = info;
        }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="Member"/> 对象是否含有信息。
        /// </summary>
        public override bool HasInfo => !(_info is null);

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的年龄。
        /// </summary>
        public int Age => GetInfo().Age;

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的群名片。
        /// </summary>
        public string Alias => GetInfo().Alias;

        /// <summary>
        /// 获取一个值，指示当前 <see cref="Member"/> 对象是否可以编辑 <see cref="Alias"/>。
        /// </summary>
        public bool CanEditAlias => GetInfo().CanEditAlias;

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的自定义头衔。
        /// </summary>
        public CustomTitle CustomTitle => GetInfo().CustomTitle;

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的显示名称。
        /// 对于 <see cref="Member"/> 对象，在 <see cref="Alias"/> 不为 <c>null</c> 时为 <see cref="Alias"/>；否则为 <see cref="Nickname"/>。
        /// </summary>
        public override string DisplayName => Alias ?? Nickname;

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象加入 <see cref="Group"/> 的时间。
        /// </summary>
        public DateTime EntranceTime => GetInfo().EntranceTime;

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的性别。
        /// </summary>
        public Gender Gender => GetInfo().Gender;

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的所属 <see cref="CoolQ.Group"/> 对象。
        /// </summary>
        public Group Group { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="Member"/> 对象是否有不良记录。
        /// </summary>
        public bool HasBadRecord => GetInfo().HasBadRecord;

        /// <summary>
        /// 获取一个值，指示当前 <see cref="Member"/> 对象的 <see cref="Role"/> 属性是否为 <see cref="MemberRole.Administrator"/> 或 <see cref="MemberRole.Owner"/>。
        /// </summary>
        public bool IsAdministrator => Role == MemberRole.Administrator || Role == MemberRole.Owner;

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的最后发言时间。
        /// </summary>
        public DateTime LastSpeakTime => GetInfo().LastSpeakTime;

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的等级。
        /// </summary>
        public string Level => GetInfo().Level;

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的位置。
        /// </summary>
        public string Location => GetInfo().Location;

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的昵称。
        /// </summary>
        public override string Nickname => GetInfo().Nickname;

        /// <summary>
        /// 获取当前 <see cref="Member"/> 对象的角色。
        /// </summary>
        public MemberRole Role => GetInfo().Role;

        /// <summary>
        /// 在被禁言时引发。
        /// </summary>
        public static event EventHandler<MemberMutedEventArgs> Muted;

        /// <summary>
        /// 在被解除禁言时引发。
        /// </summary>
        public static event EventHandler<MemberUnmutedEventArgs> Unmuted;

        /// <summary>
        /// 将当前 <see cref="Member"/> 对象踢出所在群。
        /// </summary>
        /// <param name="disallowRejoin">如果不再接收当前 <see cref="Member"/> 对象的 <see cref="EntranceRequest"/>，则为 <c>true</c>；否则为 <c>false</c>。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Kick(bool disallowRejoin = false)
        {
            NativeMethods.KickMember(
                Bot.AuthCode, Group.Number, Number, disallowRejoin).CheckError();
        }

        /// <summary>
        /// 以异步操作将当前 <see cref="Member"/> 对象踢出所在群。
        /// </summary>
        /// <param name="disallowRejoin">如果不再接收当前 <see cref="Member"/> 对象的 <see cref="EntranceRequest"/>，则为 <c>true</c>；否则为 <c>false</c>。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task KickAsync(bool disallowRejoin = false)
        {
            return Task.Run(() => Kick(disallowRejoin));
        }

        /// <summary>
        /// 禁言当前 <see cref="Member"/> 对象。
        /// </summary>
        /// <param name="duration">要禁言当前 <see cref="Member"/> 对象的时长。</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="duration"/> 小于或等于 <see cref="TimeSpan.Zero"/>。</exception>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是 <see cref="Group"/> 的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Mute(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }

            if (!Bot.CurrentUser.AsMemberOf(Group).IsAdministrator)
            {
                throw new InvalidOperationException(Resources.AdministratorOnlyOperation);
            }

            NativeMethods.MuteMember(
                Bot.AuthCode, Group.Number, Number, (long)duration.TotalSeconds).CheckError();
        }

        /// <summary>
        /// 以异步操作禁言当前 <see cref="Member"/> 对象。
        /// </summary>
        /// <param name="duration">要禁言当前 <see cref="Member"/> 对象的时长。</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="duration"/> 小于或等于 <see cref="TimeSpan.Zero"/>。</exception>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是 <see cref="Group"/> 的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task MuteAsync(TimeSpan duration)
        {
            return Task.Run(() => Mute(duration));
        }

        /// <summary>
        /// 请求当前 <see cref="Member"/> 对象的信息。
        /// </summary>
        /// <param name="refresh">如果要求酷Q不使用缓存的信息，则为 <c>true</c>；否则为 <c>false</c>。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public override void RequestInfo(bool refresh = false)
        {
            _info = null;
            GetInfo(true, refresh);
        }

        /// <summary>
        /// 将当前 <see cref="Member"/> 对象设为 <see cref="Group"/> 的管理员。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是<see cref="Group"/> 的群主。</exception>
        /// <exception cref="InvalidOperationException"><see cref="Role"/> 为 <see cref="MemberRole.Owner"/>。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void SetAsAdministrator()
        {
            SetIsAdministrator(true);
        }

        /// <summary>
        /// 以异步操作将当前 <see cref="Member"/> 对象设为 <see cref="Group"/> 的管理员。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是<see cref="Group"/> 的群主。</exception>
        /// <exception cref="InvalidOperationException"><see cref="Role"/> 为 <see cref="MemberRole.Owner"/>。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task SetAsAdministratorAsync()
        {
            return Task.Run(SetAsAdministrator);
        }

        /// <summary>
        /// 设置当前 <see cref="Member"/> 对象的 <see cref="Alias"/>。
        /// </summary>
        /// <param name="alias">要设置的 <see cref="Alias"/> 的值。</param>
        /// <exception cref="ArgumentException"><paramref name="alias"/> 为 <c>null</c>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是 <see cref="Group"/> 的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void SetAlias(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace);
            }

            if (!Bot.CurrentUser.AsMemberOf(Group).IsAdministrator)
            {
                throw new InvalidOperationException(Resources.AdministratorOnlyOperation);
            }

            NativeMethods.SetMemberAlias(
                Bot.AuthCode, Group.Number, Number, alias).CheckError();

            _info.Alias = alias;
        }

        /// <summary>
        /// 以异步操作设置当前 <see cref="Member"/> 对象的 <see cref="Alias"/>。
        /// </summary>
        /// <param name="alias">要设置的 <see cref="Alias"/> 的值。</param>
        /// <exception cref="ArgumentException"><paramref name="alias"/> 为 <c>null</c>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是 <see cref="Group"/> 的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task SetAliasAsync(string alias)
        {
            return Task.Run(() => SetAlias(alias));
        }

        /// <summary>
        /// 设置当前 <see cref="Member"/> 对象的 <see cref="CustomTitle"/>。
        /// </summary>
        /// <param name="title">要设置的 <see cref="CustomTitle"/> 的值。</param>
        /// <exception cref="ArgumentNullException"><paramref name="title"/> 为 <c>null</c>。</exception>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是 <see cref="Group"/> 的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void SetCustomTitle(CustomTitle title)
        {
            if (title is null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (!Bot.CurrentUser.AsMemberOf(Group).IsAdministrator)
            {
                throw new InvalidOperationException(Resources.AdministratorOnlyOperation);
            }

            var expirationSeconds = (long)((title.ExpirationTime - DateTime.Now)?.TotalSeconds ?? -1);

            NativeMethods.SetMemberCustomTitle(
                Bot.AuthCode, Group.Number, Number, title.Text, expirationSeconds).CheckError();

            _info.CustomTitle = title;
        }

        /// <summary>
        /// 以异步操作设置当前 <see cref="Member"/> 对象的 <see cref="CustomTitle"/>。
        /// </summary>
        /// <param name="title">要设置的 <see cref="CustomTitle"/> 的值。</param>
        /// <exception cref="ArgumentNullException"><paramref name="title"/> 为 <c>null</c>。</exception>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是 <see cref="Group"/> 的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task SetCustomTitleAsync(CustomTitle title)
        {
            return Task.Run(() => SetCustomTitle(title));
        }

        /// <summary>
        /// 将当前 <see cref="Member"/> 对象解除禁言。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是 <see cref="Group"/> 的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Unmute()
        {
            NativeMethods.MuteMember(
                Bot.AuthCode, Group.Number, Number, 0).CheckError();
        }

        /// <summary>
        /// 以异步操作将当前 <see cref="Member"/> 对象解除禁言。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是 <see cref="Group"/> 的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task UnmuteAsync()
        {
            return Task.Run(Unmute);
        }

        /// <summary>
        /// 解除当前 <see cref="Member"/> 对象在 <see cref="Group"/> 的管理员身份。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是<see cref="Group"/> 的群主。</exception>
        /// <exception cref="InvalidOperationException"><see cref="Role"/> 为 <see cref="MemberRole.Owner"/>。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void UnsetAsAdministrator()
        {
            SetIsAdministrator(false);
        }

        /// <summary>
        /// 以异步操作解除当前 <see cref="Member"/> 对象在 <see cref="Group"/> 的管理员身份。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是<see cref="Group"/> 的群主。</exception>
        /// <exception cref="InvalidOperationException"><see cref="Role"/> 为 <see cref="MemberRole.Owner"/>。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task UnsetAsAdministratorAsync()
        {
            return Task.Run(UnsetAsAdministrator);
        }

        public override bool Equals(Chat other)
        {
            return base.Equals(other) && other is Member member && member.Group == Group;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Group.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + $"({Number},{Group})";
        }

        private MemberInfo GetInfo(bool throwException = false, bool refresh = false)
        {
            if (refresh || _info is null)
            {
                try
                {
                    using var reader = new MemberInfoReader(
                        NativeMethods.GetMemberInfoBase64(Bot.AuthCode, Group.Number, Number, refresh));
                    _info = reader.Read();
                }
                catch (CoolQException) when (!throwException)
                {
                    return new MemberInfo();
                }
            }

            return _info;
        }

        private void SetIsAdministrator(bool isAdministrator)
        {
            if (!Bot.CurrentUser.AsMemberOf(Group).IsAdministrator)
            {
                throw new InvalidOperationException(Resources.AdministratorOnlyOperation);
            }

            if (Role == MemberRole.Owner)
            {
                throw new InvalidOperationException(Resources.MemberIsOwner);
            }

            NativeMethods.SetMemberIsAdministrator(
                    Bot.AuthCode, Group.Number, Number, isAdministrator).CheckError();

            _info.Role = MemberRole.Administrator;
        }
    }
}