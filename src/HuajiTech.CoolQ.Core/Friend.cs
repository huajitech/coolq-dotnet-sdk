using System;
using System.Linq;
using HuajiTech.CoolQ.Interop;

namespace HuajiTech.CoolQ
{
    internal class Friend : User, IFriend
    {
        private FriendInfo? _info = null;
        private bool _isRequested = false;

        public Friend(long number)
            : base(number)
        {
        }

        internal Friend(FriendInfo info)
            : base(info.Number)
        {
            _info = info;
        }

        public override bool IsRequested => _isRequested;

        public override bool IsRequestedSuccessfully => !(_info is null);

        public string? Alias => GetInfo().Alias;

        public override string? Nickname => _info?.Nickname ?? base.Nickname;

        public override string DisplayName => Alias ?? base.DisplayName;

        public override void Request()
        {
            base.Request();
            GetInfo(true);
        }

        public override void Refresh()
        {
            base.Refresh();
            Request();
        }

        private FriendInfo GetInfo(bool requesting = false)
        {
            if (_isRequested && !IsRequestedSuccessfully && !requesting)
            {
                return FriendInfo.Empty;
            }

            _isRequested = true;

            try
            {
                _info = CurrentUser.GetFriendInfos().First(info => info.Number == Number);
                return _info;
            }
            catch (ApiException) when (!requesting)
            {
            }
            catch (InvalidOperationException)
            {
                if (requesting)
                {
                    throw new InvalidOperationException(CoreResources.FriendNotExist);
                }
            }

            return FriendInfo.Empty;
        }
    }
}