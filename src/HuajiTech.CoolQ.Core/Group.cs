using System;
using System.Collections.Generic;
using System.Linq;
using HuajiTech.CoolQ.Interop;

namespace HuajiTech.CoolQ
{
    internal class Group : Chat, IGroup
    {
        public static readonly Group Empty = new Group(0);

        private readonly string? _name;
        private GroupInfo? _info = null;
        private bool _isRequested;

        public Group(long number)
            : base(number)
        {
        }

        internal Group(long number, string? name)
            : this(number)
        {
            _name = name;
        }

        public string? Name => _name ?? GetInfo().Name;

        public override string DisplayName => Name ?? ToString();

        public bool IsRequestedSuccessfully => !(_info is null);

        public bool IsRequested => _isRequested;

        public int MemberCapacity => GetInfo().MemberCapacity;

        public int MemberCount => GetInfo().MemberCount;

        public void DisableAnonymous()
            => NativeMethods.Group_SetIsAnonymousEnabled(Bot.Instance.AuthCode, Number, false).CheckError();

        public void Disband() => Leave(true);

        public void EnableAnonymous()
            => NativeMethods.Group_SetIsAnonymousEnabled(Bot.Instance.AuthCode, Number, true).CheckError();

        public IReadOnlyCollection<IMember> GetMembers()
        {
            using var reader = new MemberInfoReader(
                NativeMethods.Group_GetMembers(Bot.Instance.AuthCode, Number).CheckError());
            return reader.ReadAll()
                .Select(info => new Member(info))
                .ToList();
        }

        public void Leave() => Leave(false);

        public void Mute()
            => NativeMethods.Group_SetIsMuted(Bot.Instance.AuthCode, Number, true).CheckError();

        public void Request() => GetInfo(true, false);

        public void Refresh() => GetInfo(true, true);

        public override Message Send(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(CoreResources.FieldCannotBeEmpty, nameof(message));
            }

            var id = NativeMethods.Group_Send(Bot.Instance.AuthCode, Number, message).CheckError();

            return new MessageCore(id, message);
        }

        public void Unmute()
            => NativeMethods.Group_SetIsMuted(Bot.Instance.AuthCode, Number, false).CheckError();

        public override bool Equals(IChattable? other) => base.Equals(other) && other is Group;

        private GroupInfo GetInfo(bool requesting = false, bool refresh = false)
        {
            if (_isRequested && !IsRequestedSuccessfully && !requesting)
            {
                return GroupInfo.Empty;
            }

            _isRequested = true;

            try
            {
                using var reader = new GroupInfoReader(
                    NativeMethods.Group_GetInfo(Bot.Instance.AuthCode, Number, refresh).CheckError());
                _info = reader.Read();
            }
            catch (ApiException) when (!requesting)
            {
                return GroupInfo.Empty;
            }

            return _info;
        }

        private void Leave(bool disband)
            => NativeMethods.Group_Leave(Bot.Instance.AuthCode, Number, disband).CheckError();
    }
}