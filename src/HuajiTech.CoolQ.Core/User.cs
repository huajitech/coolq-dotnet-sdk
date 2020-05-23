using HuajiTech.CoolQ.DataExchange;

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

        private protected User()
        {
        }

        public virtual bool IsRequested { get; protected set; }

        public virtual bool IsRequestedSuccessfully => !(_info is null);

        public override string DisplayName => Nickname ?? ToString();

        public virtual string? Nickname => GetInfo().Nickname;

        public void GiveThumbsUp(int count) =>
            NativeMethods.User_GiveThumbsUp(Bot.Instance.AuthCode, Number, count).CheckError();

        public virtual void Request() => GetInfo(true);

        public virtual void Refresh() => GetInfo(true, true);

        public override IMessage Send(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(CoreResources.FieldCannotBeEmpty, nameof(message));
            }

            var id = NativeMethods.User_Send(Bot.Instance.AuthCode, Number, message).CheckError();

            return new Message(id, message);
        }

        public override bool Equals(IChattable? other) => base.Equals(other) && other is User;

        private UserInfo GetInfo(bool requesting = false, bool refresh = false)
        {
            if (IsRequested && !IsRequestedSuccessfully && !requesting)
            {
                return UserInfo.Empty;
            }

            IsRequested = true;

            try
            {
                using var reader = new UserInfoReader(
                    NativeMethods.User_GetInfo(Bot.Instance.AuthCode, Number, refresh).CheckError());
                _info = reader.Read();
                return _info;
            }
            catch (ApiException) when (!requesting)
            {
                return UserInfo.Empty;
            }
        }
    }
}