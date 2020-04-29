using HuajiTech.CoolQ.DataExchange;
using System;

namespace HuajiTech.CoolQ
{
    internal class User : QQ.User
    {
        private UserInfo _info;

        public User(long number)
            : base(number)
        {
        }

        internal User(UserInfo info)
            : this(info.Number)
        {
            _info = info;
        }

        public override bool HasRequested => !(_info is null);

        public override string DisplayName => Nickname;

        public override string Nickname => GetInfo().Nickname;

        public override void GiveThumbsUp(int count)
        {
            NativeMethods.GiveThumbsUp(Bot.Instance.AuthCode, Number, count).CheckError();
        }

        public override void Request()
        {
            _info = null;
            GetInfo(true);
        }

        public override void Refresh()
        {
            GetInfo(true, true);
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

        private UserInfo GetInfo(bool throwException = false, bool refresh = false)
        {
            if (refresh || _info is null)
            {
                try
                {
                    using var reader = new UserInfoReader(
                        NativeMethods.GetUserInfoBase64(Bot.Instance.AuthCode, Number, refresh));
                    _info = reader.Read();
                }
                catch (CoolQException) when (!throwException)
                {
                    return new UserInfo();
                }
            }

            return _info;
        }
    }
}