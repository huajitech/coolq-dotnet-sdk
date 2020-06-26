using System;
using System.IO;
using HuajiTech.CoolQ.Interop;

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
            : this(info.Number, new Group(info.GroupNumber))
        {
            _info = info;
        }

        public override int Age
        {
            get
            {
                GetInfo();
                return _info?.Age ?? base.Age;
            }
        }

        public virtual string? Alias => GetInfo().Alias;

        public virtual bool CanEditAlias => GetInfo().CanEditAlias;

        public virtual CustomTitle? CustomTitle => GetInfo().CustomTitle;

        public override Gender Gender
        {
            get
            {
                GetInfo();
                return _info?.Gender ?? base.Gender;
            }
        }

        public IGroup Group { get; }

        public virtual bool HasBadRecord => GetInfo().HasBadRecord;

        public override bool IsRequestedSuccessfully => !(_info is null);

        public virtual DateTime LastSpeakTime => GetInfo().LastSpeakTime;

        public virtual string? Level => GetInfo().Level;

        public virtual string? Location => GetInfo().Location;

        public override string DisplayName => Alias ?? base.DisplayName;

        public override string? Nickname
        {
            get
            {
                GetInfo();
                return _info?.Nickname ?? base.Nickname;
            }
        }

        public virtual MemberRole Role => GetInfo().Role;

        public virtual DateTime TimeJoined => GetInfo().TimeEntered;

        public virtual bool IsAdministrator
            => Role is MemberRole.Administrator || Role is MemberRole.Owner;

        public bool Equals(IMember? other)
            => base.Equals(other) && other is Member && Group.Equals(other.Group);

        public override bool Equals(object? obj)
            => obj == this || (obj is Member member && Equals(member));

        public override int GetHashCode() => base.GetHashCode() ^ Group.GetHashCode();

        public void SetAlias(string? alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                throw new ArgumentException(CoreResources.FieldCannotBeEmptyOrWhiteSpace);
            }

            NativeMethods.Member_SetAlias(
                Bot.Instance.AuthCode, Group.Number, Number, alias).CheckError();
        }

        public void Kick(bool ignoreFurtherRequests = false)
            => NativeMethods.Member_Kick(
                   Bot.Instance.AuthCode, Group.Number, Number, ignoreFurtherRequests).CheckError();

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

        public void Unmute()
            => NativeMethods.Member_Mute(
                   Bot.Instance.AuthCode, Group.Number, Number, 0).CheckError();

        public void SetCustomTitle(CustomTitle? title)
        {
            if (title is null)
            {
                NativeMethods.Member_SetCustomTitle(
                    Bot.Instance.AuthCode, Group.Number, Number, null, 0).CheckError();
            }
            else
            {
                NativeMethods.Member_SetCustomTitle(
                    Bot.Instance.AuthCode,
                    Group.Number,
                    Number,
                    title.Text,
                    (long)((title.ExpirationTime - DateTime.Now)?.TotalSeconds ?? -1))
                    .CheckError();
            }
        }

        public void MakeAdministrator() => SetIsAdministrator(true);

        public void UnmakeAdministrator() => SetIsAdministrator(false);

        public override void Request() => GetInfo(true, false);

        public override void Refresh() => GetInfo(true, true);

        public override string ToString() => GetType().Name + $"({Number},{Group})";

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

        private void SetIsAdministrator(bool value)
            => NativeMethods.Member_SetIsAdministrator(
                Bot.Instance.AuthCode, Group.Number, Number, value).CheckError();
    }
}