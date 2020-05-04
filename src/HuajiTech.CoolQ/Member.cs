using HuajiTech.CoolQ.DataExchange;
using HuajiTech.QQ;
using System;

namespace HuajiTech.CoolQ
{
    internal class Member : User, IMember
    {
        public static readonly TimeSpan MaxMuteDuration = TimeSpan.FromDays(30);

        private MemberInfo _info;

        public Member(long number, IGroup group)
            : base(number)
        {
            Group = group;
        }

        internal Member(MemberInfo info)
            : this(info.Number, info.Group)
        {
            _info = info;
        }

        public int Age => GetInfo().Age;

        public string Alias => GetInfo().Alias;

        public bool CanEditAlias => GetInfo().CanEditAlias;

        public CustomTitle CustomTitle => GetInfo().CustomTitle;

        public Gender Gender => GetInfo().Gender;

        public IGroup Group { get; }

        public bool HasBadRecord => GetInfo().HasBadRecord;

        public override bool HasRequested => !(_info is null);

        public DateTime LastSpeakTime => GetInfo().LastSpeakTime;

        public string Level => GetInfo().Level;

        public string Location => GetInfo().Location;

        public override string Nickname => GetInfo().Nickname;

        public MemberRole Role => GetInfo().Role;

        public DateTime TimeEntered => GetInfo().TimeEntered;

        public bool IsAdministrator => Role is MemberRole.Administrator || Role is MemberRole.Owner;

        public bool Equals(IMember other) => base.Equals(other) && Group.Equals(other.Group);

        public override int GetHashCode() => base.GetHashCode() ^ Group.GetHashCode();

        public void Kick(bool disallowRejoin = false)
            => NativeMethods.KickMember(
                Bot.Instance.AuthCode, Group.Number, Number, disallowRejoin).CheckError();

        public void Mute(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }

            NativeMethods.MuteMember(
                Bot.Instance.AuthCode, Group.Number, Number, (long)duration.TotalSeconds).CheckError();
        }

        public void Mute() => Mute(MaxMuteDuration);

        public override void Refresh() => GetInfo(true, true);

        public override void Request()
        {
            _info = null;
            GetInfo(true);
        }

        public void SetAlias(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace);
            }

            NativeMethods.SetMemberAlias(
                Bot.Instance.AuthCode, Group.Number, Number, alias).CheckError();

            _info.Alias = alias;
        }

        public void SetAsAdministrator() => SetIsAdministrator(true);

        public void SetCustomTitle(CustomTitle title)
        {
            if (title is null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            var expirationSeconds = (long)((title.ExpirationTime - DateTime.Now)?.TotalSeconds ?? -1);

            NativeMethods.SetMemberCustomTitle(
                Bot.Instance.AuthCode, Group.Number, Number, title.Text, expirationSeconds).CheckError();

            _info.CustomTitle = title;
        }

        public override string ToString() => GetType().Name + $"({Number},{Group})";

        public void Unmute()
            => NativeMethods.MuteMember(
                Bot.Instance.AuthCode, Group.Number, Number, 0).CheckError();

        public void UnsetAsAdministrator() => SetIsAdministrator(false);

        private MemberInfo GetInfo(bool throwException = false, bool refresh = false)
        {
            if (refresh || _info is null)
            {
                try
                {
                    using var reader = new MemberInfoReader(
                        NativeMethods.GetMemberInfoBase64(Bot.Instance.AuthCode, Group.Number, Number, refresh));
                    _info = reader.Read();
                }
                catch (CoolQException) when (!throwException)
                {
                    return new MemberInfo();
                }
            }

            return _info;
        }

        private void SetIsAdministrator(bool isAdministrator) =>
            NativeMethods.SetMemberIsAdministrator(
                    Bot.Instance.AuthCode, Group.Number, Number, isAdministrator).CheckError();
    }
}