using HuajiTech.CoolQ.DataExchange;
using HuajiTech.QQ;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuajiTech.CoolQ
{
    internal partial class Group : Chat, IGroup
    {
        private readonly string? _name;
        private GroupInfo? _info;

        public Group(long number)
            : base(number)
        {
        }

        internal Group(long number, string? name)
            : this(number)
        {
            _name = name;
        }

        public override string? DisplayName => Name;

        public bool HasRequested => !(_info is null);

        public int MemberCapacity => GetInfo().MemberCapacity;

        public int MemberCount => GetInfo().MemberCount;

        public string? Name => _name ?? GetInfo().Name;

        public void DisableAnonymous() =>
            NativeMethods.SetGroupIsAnonymousEnabled(Bot.Instance.AuthCode, Number, false).CheckError();

        public void Disband() => Leave(true);

        public void EnableAnonymous() =>
            NativeMethods.SetGroupIsAnonymousEnabled(Bot.Instance.AuthCode, Number, true).CheckError();

        public IReadOnlyCollection<IMember> GetMembers()
        {
            using var reader = new MemberInfoReader(
                NativeMethods.GetGroupMembersBase64(Bot.Instance.AuthCode, Number));
            return reader.ReadAll()
                .Select(info => new Member(info))
                .ToList();
        }

        public void Leave() => Leave(false);

        public void Mute() =>
            NativeMethods.SetGroupIsMuted(Bot.Instance.AuthCode, Number, true).CheckError();

        public void Refresh() => GetInfo(true, true);

        public void Request()
        {
            _info = null;
            GetInfo(true);
        }

        public override IMessage Send(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmpty, nameof(message));
            }

            var id = NativeMethods.SendGroupMessage(Bot.Instance.AuthCode, Number, message).CheckError();

            return new Message(id, message);
        }

        public void Unmute() =>
            NativeMethods.SetGroupIsMuted(Bot.Instance.AuthCode, Number, false).CheckError();

        public override bool Equals(IChattable other) => base.Equals(other) && other is Group;

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

        private void Leave(bool disband) =>
            NativeMethods.LeaveGroup(Bot.Instance.AuthCode, Number, disband).CheckError();
    }
}