using HuajiTech.CoolQ.DataExchange;
using HuajiTech.QQ;
using System;
using System.Linq;

namespace HuajiTech.CoolQ
{
    internal class Contact : User, IContact
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

        public string Alias => GetInfo().Alias;

        public override string Nickname => GetInfo().Nickname;

        public override string DisplayName => Alias ?? Nickname;

        public override void Refresh() => Request();

        public override void Request()
        {
            _info = null;
            GetInfo(true);
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