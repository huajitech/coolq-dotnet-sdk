using HuajiTech.CoolQ.DataExchange;
using HuajiTech.QQ;
using System;
using System.Linq;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示联系人。
    /// </summary>
    internal class Contact : User, IContact
    {
        private ContactInfo _info;

        internal Contact(long number)
            : base(number)
        {
        }

        internal Contact(ContactInfo info)
            : base(info.Number)
        {
            _info = info;
        }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="Contact"/> 对象是否含有信息。
        /// </summary>
        public override bool HasRequested => !(_info is null);

        /// <summary>
        /// 获取当前 <see cref="Contact"/> 对象的备注名。
        /// </summary>
        public string Alias => GetInfo().Alias;

        /// <summary>
        /// 获取当前 <see cref="Contact"/> 对象的昵称。
        /// </summary>
        public override string Nickname => _info?.Nickname ?? base.Nickname;

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