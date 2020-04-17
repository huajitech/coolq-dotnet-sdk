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
        /// <param name="group">群。</param>
        public Member(long number, Group group)
            : base(number)
        {
            Group = group;
        }

        internal Member(MemberInfo info)
            : this(info.Number, info.Group)
        {
            _info = info;
        }

        /// <summary>
        /// 获取一个值，指示是否已请求信息。
        /// </summary>
        public override bool HasInfo => !(_info is null);

        /// <summary>
        /// 获取年龄。
        /// </summary>
        public int Age => GetInfo().Age;

        /// <summary>
        /// 获取群名片。
        /// </summary>
        public string Alias => GetInfo().Alias;

        /// <summary>
        /// 获取一个值，指示该成员是否可以编辑群名片。
        /// </summary>
        public bool CanEditAlias => GetInfo().CanEditAlias;

        /// <summary>
        /// 获取自定义头衔。
        /// </summary>
        public CustomTitle CustomTitle => GetInfo().CustomTitle;

        /// <summary>
        /// 获取显示名称。
        /// </summary>
        public override string DisplayName => Alias ?? Nickname;

        /// <summary>
        /// 获取入群时间。
        /// </summary>
        public DateTime EntranceTime => GetInfo().EntranceTime;

        /// <summary>
        /// 获取性别。
        /// </summary>
        public Gender Gender => GetInfo().Gender;

        /// <summary>
        /// 获取所在群。
        /// </summary>
        public Group Group { get; }

        /// <summary>
        /// 获取一个值，指示是否有不良记录。
        /// </summary>
        public bool HasBadRecord => GetInfo().HasBadRecord;

        /// <summary>
        /// 获取一个值，指示是否为管理员或群主。
        /// </summary>
        public bool IsAdministrator => Role == MemberRole.Administrator || Role == MemberRole.Owner;

        /// <summary>
        /// 获取最后发言时间。
        /// </summary>
        public DateTime LastSpeakTime => GetInfo().LastSpeakTime;

        /// <summary>
        /// 获取等级。
        /// </summary>
        public string Level => GetInfo().Level;

        /// <summary>
        /// 获取位置。
        /// </summary>
        public string Location => GetInfo().Location;

        /// <summary>
        /// 获取昵称。
        /// </summary>
        public override string Nickname => GetInfo().Nickname;

        /// <summary>
        /// 获取角色。
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
        /// 踢出。
        /// </summary>
        /// <param name="disallowRejoin">是否禁止再次加群。</param>
        public void Kick(bool disallowRejoin = false)
        {
            NativeMethods.KickMember(
                Bot.AuthCode, Group.Number, Number, disallowRejoin).CheckError();
        }

        /// <summary>
        /// 以异步操作踢出。
        /// </summary>
        /// <param name="disallowRejoin">是否禁止再次加群。</param>
        public Task KickAsync(bool disallowRejoin = false)
        {
            return Task.Run(() => Kick(disallowRejoin));
        }

        /// <summary>
        /// 禁言。
        /// </summary>
        /// <param name="duration">禁言时长。</param>
        public void Mute(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }

            NativeMethods.MuteMember(
                Bot.AuthCode, Group.Number, Number, (long)duration.TotalSeconds).CheckError();
        }

        /// <summary>
        /// 以异步操作禁言。
        /// </summary>
        /// <param name="duration">禁言时长。</param>
        public Task MuteAsync(TimeSpan duration)
        {
            return Task.Run(() => Mute(duration));
        }

        /// <summary>
        /// 请求信息。
        /// </summary>
        /// <param name="refresh">是否刷新缓存。</param>
        public override void RequestInfo(bool refresh = false)
        {
            _info = null;
            GetInfo(false, refresh);
        }

        /// <summary>
        /// 设为管理员。
        /// </summary>
        public void SetAsAdministrator()
        {
            SetIsAdministrator(true);
        }

        /// <summary>
        /// 以异步操作设为管理员。
        /// </summary>
        public Task SetAsAdministratorAsync()
        {
            return Task.Run(SetAsAdministrator);
        }

        /// <summary>
        /// 设置群名片。
        /// </summary>
        /// <param name="alias">要设置的群名片。</param>
        public void SetAlias(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmpty);
            }

            NativeMethods.SetMemberAlias(
                Bot.AuthCode, Group.Number, Number, alias).CheckError();

            _info.Alias = alias;
        }

        /// <summary>
        /// 以异步操作设置群名片。
        /// </summary>
        /// <param name="alias">要设置的群名片。</param>
        public Task SetAliasAsync(string alias)
        {
            return Task.Run(() => SetAlias(alias));
        }

        /// <summary>
        /// 设置自定义头衔。
        /// </summary>
        /// <param name="title">要设置的头衔。</param>
        public void SetCustomTitle(CustomTitle title)
        {
            if (title is null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            var expirationSeconds = (long)((title.ExpirationTime - DateTime.Now)?.TotalSeconds ?? -1);

            NativeMethods.SetMemberCustomTitle(
                Bot.AuthCode, Group.Number, Number, title.Text, expirationSeconds).CheckError();

            _info.CustomTitle = title;
        }

        /// <summary>
        /// 以异步操作设置自定义头衔。
        /// </summary>
        /// <param name="title">要设置的头衔。</param>
        public Task SetCustomTitleAsync(CustomTitle title)
        {
            return Task.Run(() => SetCustomTitle(title));
        }

        /// <summary>
        /// 解除禁言。
        /// </summary>
        public void Unmute()
        {
            NativeMethods.MuteMember(
                Bot.AuthCode, Group.Number, Number, 0).CheckError();
        }

        /// <summary>
        /// 以异步操作解除禁言。
        /// </summary>
        public Task UnmuteAsync()
        {
            return Task.Run(Unmute);
        }

        /// <summary>
        /// 解除管理员身份。
        /// </summary>
        public void UnsetAsAdministrator()
        {
            SetIsAdministrator(false);
        }

        /// <summary>
        /// 以异步操作解除管理员身份。
        /// </summary>
        public Task UnsetAsAdministratorAsync()
        {
            return Task.Run(UnsetAsAdministrator);
        }

        public override string ToString()
        {
            return $"Member({Number},{Group})";
        }

        private MemberInfo GetInfo(bool handleException = true, bool refresh = false)
        {
            if (refresh || _info is null)
            {
                try
                {
                    using var reader = new MemberInfoReader(
                        NativeMethods.GetMemberInfoBase64(Bot.AuthCode, Group.Number, Number, refresh));
                    _info = reader.Read();
                }
                catch (CoolQException) when (handleException)
                {
                    return new MemberInfo();
                }
            }

            return _info;
        }

        private void SetIsAdministrator(bool isAdministrator)
        {
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