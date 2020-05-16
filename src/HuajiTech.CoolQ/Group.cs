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

        public override string Name => _name ?? GetInfo().Name ?? ToString();

        public bool IsRequestedSuccessfully => !(_info is null);

        public bool IsRequested { get; protected set; }

        public int MemberCapacity => GetInfo().MemberCapacity;

        public int MemberCount => GetInfo().MemberCount;

        public void DisableAnonymous() =>
            NativeMethods.Group_SetIsAnonymousEnabled(Bot.Instance.AuthCode, Number, false).CheckError();

        public void Disband() => Leave(true);

        public void EnableAnonymous() =>
            NativeMethods.Group_SetIsAnonymousEnabled(Bot.Instance.AuthCode, Number, true).CheckError();

        public IReadOnlyCollection<IMember> GetMembers()
        {
            using var reader = new MemberInfoReader(
                NativeMethods.Group_GetMembers(Bot.Instance.AuthCode, Number).CheckError());
            return reader.ReadAll()
                .Select(info => new Member(info))
                .ToList();
        }

        public void Leave() => Leave(false);

        public void Mute() =>
            NativeMethods.Group_SetIsMuted(Bot.Instance.AuthCode, Number, true).CheckError();

        public void Refresh() => GetInfo(true, true);

        public void Request()
        {
            _info = null;
            GetInfo(true);
        }

        public override IContentfulMessage Send(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmpty, nameof(message));
            }

            var id = NativeMethods.Group_Send(Bot.Instance.AuthCode, Number, message).CheckError();

            return new ContentfulMessage(id, message);
        }

        public void Unmute() =>
            NativeMethods.Group_SetIsMuted(Bot.Instance.AuthCode, Number, false).CheckError();

        public override bool Equals(IChattable? other) => base.Equals(other) && other is Group;

        private GroupInfo GetInfo(bool requesting = false, bool refresh = false)
        {
            if (IsRequested && !requesting)
            {
                return new GroupInfo();
            }

            IsRequested = true;

            try
            {
                using var reader = new GroupInfoReader(
                    NativeMethods.Group_GetInfo(Bot.Instance.AuthCode, Number, refresh).CheckError());
                _info = reader.Read();
            }
            catch (CoolQException) when (!requesting)
            {
                return new GroupInfo();
            }

            return _info;
        }

        private void Leave(bool disband) =>
            NativeMethods.Group_Leave(Bot.Instance.AuthCode, Number, disband).CheckError();
    }
}