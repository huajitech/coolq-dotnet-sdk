using HuajiTech.CoolQ.DataExchange;
using System;
using System.Linq;

namespace HuajiTech.CoolQ
{
    internal class Contact : QQ.Contact
    {
        private ContactInfo _info;

        public Contact(long number)
            : base(number)
        {
        }

        internal Contact(ContactInfo info)
            : base(info.Number)
        {
            _info = info;
        }

        public override bool HasRequested => !(_info is null);

        public override string Alias => GetInfo().Alias;

        public override string Nickname => GetInfo().Nickname;

        public override string DisplayName => Alias ?? Nickname;

        public override void GiveThumbsUp(int count) =>
            NativeMethods.GiveThumbsUp(Bot.Instance.AuthCode, Number, count).CheckError();

        public override void Refresh() => Request();

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

            var id = NativeMethods.SendPrivateMessage(Bot.Instance.AuthCode, Number, message).CheckError();

            return new Message(id, message);
        }

        private ContactInfo GetInfo(bool throwException = false)
        {
            try
            {
                return _info ??= CurrentUser.GetContactInfos()
                    .First(info => info.Number == Number);
            }
            catch (CoolQException) when (!throwException)
            {
                return new ContactInfo();
            }
            catch (InvalidOperationException)
            {
                if (throwException)
                {
                    throw new InvalidOperationException(Resources.ContactNotExist);
                }

                return new ContactInfo();
            }
        }
    }
}