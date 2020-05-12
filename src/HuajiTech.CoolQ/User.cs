using HuajiTech.CoolQ.DataExchange;
using HuajiTech.QQ;
using System;

namespace HuajiTech.CoolQ
{
    internal class User : Chat, IUser
    {
        private UserInfo? _info;

        public User(long number)
            : base(number)
        {
        }

        internal User(UserInfo info)
            : this(info.Number)
        {
            _info = info;
        }

        public virtual bool HasRequested => !(_info is null);

        public override string? DisplayName => Nickname;

        public virtual string? Nickname => GetInfo().Nickname;

        public void GiveThumbsUp(int count) =>
            NativeMethods.GiveThumbsUp(Bot.Instance.AuthCode, Number, count).CheckError();

        public virtual void Request()
        {
            _info = null;
            GetInfo(true);
        }

        public virtual void Refresh() => GetInfo(true, true);

        public override IMessage Send(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmpty, nameof(message));
            }

            var id = NativeMethods.SendPrivateMessage(Bot.Instance.AuthCode, Number, message).CheckError();

            return new Message(id, message);
        }

        public override bool Equals(IChattable other) => base.Equals(other) && other is User;

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