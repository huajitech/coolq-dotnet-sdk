using HuajiTech.CoolQ.DataExchange;

using System;

namespace HuajiTech.CoolQ
{
    internal class Member : User, IMember
    {
        public static readonly TimeSpan MaxMuteDuration = TimeSpan.FromDays(30);

        private MemberInfo? _info;

        public Member(long number, IGroup group)
            : base(number)
        {
            Group = group ?? throw new ArgumentNullException(nameof(group));
        }

        internal Member(MemberInfo info)
            : this(info.Number, info.Group!)
        {
            _info = info;
        }

        public int Age => GetInfo().Age;

        public string? Alias => GetInfo().Alias;

        public bool CanEditAlias => GetInfo().CanEditAlias;

        public CustomTitle? CustomTitle => GetInfo().CustomTitle;

        public Gender Gender => GetInfo().Gender;

        public IGroup Group { get; }

        public bool HasBadRecord => GetInfo().HasBadRecord;

        public override bool IsRequestedSuccessfully => !(_info is null);

        public DateTime LastSpeakTime => GetInfo().LastSpeakTime;

        public string? Level => GetInfo().Level;

        public string? Location => GetInfo().Location;

        public override string DisplayName => Alias ?? base.DisplayName;

        public override string? Nickname => GetInfo().Nickname;

        public MemberRole Role => GetInfo().Role;

        public DateTime TimeEntered => GetInfo().TimeEntered;

        public bool IsAdministrator => Role is MemberRole.Administrator || Role is MemberRole.Owner;

        public bool Equals(IMember? other) => base.Equals(other) && other is Member && Group.Equals(other.Group);

        public override bool Equals(IChattable? other) =>
            other is IMember member ? Equals(member) : base.Equals(other);

        public override int GetHashCode() => base.GetHashCode() ^ Group.GetHashCode();

        public void Kick(bool disallowRejoin = false) =>
            NativeMethods.Member_Kick(
                Bot.Instance.AuthCode, Group.Number, Number, disallowRejoin).CheckError();

        public void Mute(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }

            NativeMethods.Member_Mute(
                Bot.Instance.AuthCode, Group.Number, Number, (long)duration.TotalSeconds).CheckError();
        }

        public void Mute() => Mute(MaxMuteDuration);

        public override void Refresh() => GetInfo(true, true);

        public override void Request() => GetInfo(true);

        public void SetAlias(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                throw new ArgumentException(CoreResources.FieldCannotBeEmptyOrWhiteSpace);
            }

            NativeMethods.Member_SetAlias(
                Bot.Instance.AuthCode, Group.Number, Number, alias).CheckError();
        }

        public void SetAsAdministrator() => SetIsAdministrator(true);

        public void SetCustomTitle(CustomTitle title)
        {
            if (title is null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            var expirationSeconds = (long)((title.ExpirationTime - DateTime.Now)?.TotalSeconds ?? -1);

            NativeMethods.Member_SetCustomTitle(
                Bot.Instance.AuthCode, Group.Number, Number, title.Text, expirationSeconds).CheckError();
        }

        public override string ToString() => GetType().Name + $"({Number},{Group})";

        public void Unmute() =>
            NativeMethods.Member_Mute(
                Bot.Instance.AuthCode, Group.Number, Number, 0).CheckError();

        public void UnsetAsAdministrator() => SetIsAdministrator(false);

        private MemberInfo GetInfo(bool requesting = false, bool refresh = false)
        {
            if (IsRequested && !IsRequestedSuccessfully && !requesting)
            {
                return MemberInfo.Empty;
            }

            IsRequested = true;

            try
            {
                using var reader = new MemberInfoReader(
                    NativeMethods.Member_GetInfo(Bot.Instance.AuthCode, Group.Number, Number, refresh).CheckError());
                _info = reader.Read();
                return _info;
            }
            catch (ApiException) when (!requesting)
            {
                return MemberInfo.Empty;
            }
        }

        private void SetIsAdministrator(bool isAdministrator) =>
            NativeMethods.Member_SetIsAdministrator(
                    Bot.Instance.AuthCode, Group.Number, Number, isAdministrator).CheckError();
    }
}