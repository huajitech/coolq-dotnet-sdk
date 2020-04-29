using HuajiTech.CoolQ.DataExchange;
using HuajiTech.QQ;
using System;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    internal partial class Member : QQ.Member
    {
        public static readonly TimeSpan MaxMuteDuration = TimeSpan.FromDays(30);

        private MemberInfo _info;

        public Member(long number, QQ.Group group)
            : base(number, group)
        {
        }

        internal Member(MemberInfo info)
            : this(info.Number, info.Group)
        {
            _info = info;
        }

        public override int Age => GetInfo().Age;

        public override string Alias => GetInfo().Alias;

        public override bool CanEditAlias => GetInfo().CanEditAlias;

        public override CustomTitle CustomTitle => GetInfo().CustomTitle;

        public override Gender Gender => GetInfo().Gender;

        public override bool HasBadRecord => GetInfo().HasBadRecord;

        public override bool HasRequested => !(_info is null);

        public override DateTime LastSpeakTime => GetInfo().LastSpeakTime;

        public override string Level => GetInfo().Level;

        public override string Location => GetInfo().Location;

        public override string Nickname => GetInfo().Nickname;

        public override MemberRole Role => GetInfo().Role;

        public override DateTime TimeEntered => GetInfo().TimeEntered;

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Group.GetHashCode();
        }

        public override void GiveThumbsUp(int count)
        {
            NativeMethods.GiveThumbsUp(Bot.Instance.AuthCode, Number, count).CheckError();
        }

        public override void Kick(bool disallowRejoin = false)
        {
            NativeMethods.KickMember(
                Bot.Instance.AuthCode, Group.Number, Number, disallowRejoin).CheckError();
        }

        public override void Mute(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }

            NativeMethods.MuteMember(
                Bot.Instance.AuthCode, Group.Number, Number, (long)duration.TotalSeconds).CheckError();
        }

        public override void Mute()
        {
            Mute(MaxMuteDuration);
        }

        public override void Refresh()
        {
            GetInfo(true, true);
        }

        public override void Request()
        {
            _info = null;
            GetInfo(true);
        }

        public override QQ.Message Send(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmpty, nameof(message));
            }

            var id = NativeMethods.SendPrivateMessage(Bot.Instance.AuthCode, Number, message).CheckError();

            return new Message(id, message);
        }

        public override void SetAlias(string card)
        {
            if (string.IsNullOrWhiteSpace(card))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace);
            }

            NativeMethods.SetMemberAlias(
                Bot.Instance.AuthCode, Group.Number, Number, card).CheckError();

            _info.Alias = card;
        }

        public override void SetAsAdministrator()
        {
            SetIsAdministrator(true);
        }

        public override Task SetAsAdministratorAsync()
        {
            return Task.Run(SetAsAdministrator);
        }

        public override void SetCustomTitle(CustomTitle title)
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

        public override string ToString()
        {
            return GetType().Name + $"({Number},{Group})";
        }

        public override void Unmute()
        {
            NativeMethods.MuteMember(
                Bot.Instance.AuthCode, Group.Number, Number, 0).CheckError();
        }

        public override void UnsetAsAdministrator()
        {
            SetIsAdministrator(false);
        }

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

        private void SetIsAdministrator(bool isAdministrator)
        {
            NativeMethods.SetMemberIsAdministrator(
                    Bot.Instance.AuthCode, Group.Number, Number, isAdministrator).CheckError();

            _info.Role = MemberRole.Administrator;
        }
    }
}