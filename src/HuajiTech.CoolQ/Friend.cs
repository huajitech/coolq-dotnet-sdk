using HuajiTech.CoolQ.DataExchange;
using HuajiTech.QQ;
using System;
using System.Linq;

namespace HuajiTech.CoolQ
{
    internal class Friend : User, IFriend
    {
        private FriendInfo _info;

        public Friend(long number)
            : base(number)
        {
        }

        internal Friend(FriendInfo info)
            : base(info.Number)
        {
            _info = info;
        }

        public override bool HasRequested => !(_info is null);

        public string Alias => GetInfo().Alias;

        public override string Nickname => GetInfo().Nickname;

        public override string DisplayName => Alias ?? Nickname;

        public override void Refresh() => Request();

        public override void Request()
        {
            _info = null;
            GetInfo(true);
        }

        private FriendInfo GetInfo(bool throwException = false)
        {
            try
            {
                return _info ??= CurrentUser.GetFriendInfos()
                    .First(info => info.Number == Number);
            }
            catch (CoolQException) when (!throwException)
            {
                return new FriendInfo();
            }
            catch (InvalidOperationException)
            {
                if (throwException)
                {
                    throw new InvalidOperationException(Resources.FriendNotExist);
                }

                return new FriendInfo();
            }
        }
    }
}