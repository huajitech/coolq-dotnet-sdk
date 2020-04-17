using HuajiTech.CoolQ.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示联系人。
    /// </summary>
    public class Contact : User
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
        /// 获取一个值，指示是否已请求信息。
        /// </summary>
        public override bool HasInfo => !(_info is null);

        /// <summary>
        /// 获取备注名。
        /// </summary>
        public string Alias => _info.Alias;

        /// <summary>
        /// 获取昵称。
        /// </summary>
        public override string Nickname => GetInfo().Nickname ?? base.Nickname;

        /// <summary>
        /// 请求信息。
        /// </summary>
        public void RequestInfo()
        {
            _info = null;
            GetInfo(false);
        }

        private ContactInfo GetInfo(bool handleException = true)
        {
            try
            {
                return _info ??= CurrentUser.GetContactInfos()
                    .First(info => info.Number == Number);
            }
            catch (InvalidOperationException) when (handleException)
            {
                return new ContactInfo();
            }
        }
    }
}