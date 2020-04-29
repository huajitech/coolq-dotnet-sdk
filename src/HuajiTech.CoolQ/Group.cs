using HuajiTech.CoolQ.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuajiTech.CoolQ
{
    internal partial class Group : QQ.Group
    {
        private readonly string _name;
        private GroupInfo _info;

        public Group(long number)
            : base(number)
        {
        }

        internal Group(long number, string name)
            : this(number)
        {
            _name = name;
        }

        public override string DisplayName => Name;

        public override bool HasRequested => !(_info is null);

        public override int MemberCapacity => GetInfo().MemberCapacity;

        public override int MemberCount => GetInfo().MemberCount;

        public override string Name => _name ?? GetInfo().Name;

        public override void DisableAnonymous()
        {
            NativeMethods.SetGroupIsAnonymousEnabled(Bot.Instance.AuthCode, Number, false).CheckError();
        }

        public override void Disband() => Leave(true);

        public override void EnableAnonymous()
        {
            NativeMethods.SetGroupIsAnonymousEnabled(Bot.Instance.AuthCode, Number, true).CheckError();
        }

        public override IReadOnlyCollection<QQ.Member> GetMembers()
        {
            using var reader = new MemberInfoReader(
                NativeMethods.GetGroupMembersBase64(Bot.Instance.AuthCode, Number));
            return reader.ReadAll()
                .Select(info => new Member(info))
                .ToList();
        }

        public override void Leave() => Leave(false);

        public override void Mute()
        {
            NativeMethods.SetGroupIsMuted(Bot.Instance.AuthCode, Number, true).CheckError();
        }

        public override void Refresh() => GetInfo(true, true);

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

            var id = NativeMethods.SendGroupMessage(Bot.Instance.AuthCode, Number, message).CheckError();

            return new Message(id, message);
        }

        public override void Unmute()
        {
            NativeMethods.SetGroupIsMuted(Bot.Instance.AuthCode, Number, false).CheckError();
        }

        private GroupInfo GetInfo(bool throwException = false, bool refresh = false)
        {
            if (refresh || _info is null)
            {
                try
                {
                    using var reader = new GroupInfoReader(
                        NativeMethods.GetGroupInfoBase64(Bot.Instance.AuthCode, Number, refresh));
                    _info = reader.Read();
                }
                catch (CoolQException) when (!throwException)
                {
                    return new GroupInfo();
                }
            }

            return _info;
        }

        private void Leave(bool disband)
        {
            NativeMethods.LeaveGroup(Bot.Instance.AuthCode, Number, disband).CheckError();
        }
    }
}