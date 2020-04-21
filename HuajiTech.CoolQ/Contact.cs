using HuajiTech.CoolQ.DataExchange;
using System;
using System.Linq;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示联系人。
    /// </summary>
    public class Contact : User
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

        /// <summary>
        /// 获取一个值，指示当前 <see cref="Contact"/> 对象是否含有信息。
        /// </summary>
        public override bool HasInfo => !(_info is null);

        /// <summary>
        /// 获取当前 <see cref="Contact"/> 对象的备注名。
        /// </summary>
        public string Alias => GetInfo().Alias;

        /// <summary>
        /// 获取当前 <see cref="Contact"/> 对象的昵称。
        /// </summary>
        public override string Nickname => _info?.Nickname ?? base.Nickname;

        /// <summary>
        /// 请求信息。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        /// <exception cref="InvalidOperationException">当前 <see cref="Contact"/> 对象表示的联系人不存在。</exception>
        public void RequestInfo()
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