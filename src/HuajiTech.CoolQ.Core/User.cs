using System;
using HuajiTech.CoolQ.Interop;

namespace HuajiTech.CoolQ
{
    internal class User : Chat, IUser
    {
        private UserInfo? _info = null;
        private bool _isRequested;

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

        public virtual bool IsRequested => _isRequested;

        public virtual bool IsRequestedSuccessfully => !(_info is null);

        public override string DisplayName => Nickname ?? ToString();

        public virtual string? Nickname => GetInfo().Nickname;

        public virtual int Age => GetInfo().Age;

        public virtual Gender Gender => GetInfo().Gender;

        public void Like(int count = 1)
        {
            if (count <= 0 || count > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            NativeMethods.User_Like(Bot.Instance.AuthCode, Number, count).CheckError();
        }

        public virtual void Request() => GetInfo(true, false);

        public virtual void Refresh() => GetInfo(true, true);

        public override Message Send(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(CoreResources.FieldCannotBeEmpty, nameof(message));
            }

            var id = NativeMethods.User_Send(Bot.Instance.AuthCode, Number, message).CheckError();

            return new MessageCore(id, message);
        }

        public override bool Equals(IChattable? other) => base.Equals(other) && other is User;

        private UserInfo GetInfo(bool requesting = false, bool refresh = false)
        {
            if (_isRequested && !IsRequestedSuccessfully && !requesting)
            {
                return UserInfo.Empty;
            }

            _isRequested = true;

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