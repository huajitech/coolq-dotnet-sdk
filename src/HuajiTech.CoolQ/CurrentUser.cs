using HuajiTech.CoolQ.DataExchange;
using HuajiTech.QQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示当前用户，并提供与当前用户交互的静态方法、事件和属性。
    /// </summary>
    internal partial class CurrentUser : User, ICurrentUser
    {
        internal CurrentUser()
            : base(NativeMethods.GetCurrentUserNumber(Bot.Instance.AuthCode).CheckError())
        {
        }

        /// <summary>
        /// 获取当前用户的昵称。
        /// </summary>
        public override string Nickname =>
            NativeMethods.GetCurrentUserNickname(Bot.Instance.AuthCode);

        internal static IReadOnlyCollection<ContactInfo> GetContactInfos()
        {
            using var reader = new ContactInfoReader(
                NativeMethods.GetContactsBase64(Bot.Instance.AuthCode, false).CheckError());

            return reader.ReadAll().ToList();
        }

        /// <summary>
        /// 获取当前用户的所有联系人。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public IReadOnlyCollection<IContact> GetContacts()
        {
            return GetContactInfos()
                .Select(info => new Contact(info))
                .ToList();
        }

        /// <summary>
        /// 以异步操作获取当前用户的所有联系人。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task<IReadOnlyCollection<IContact>> GetContactsAsync()
        {
            return Task.Run(GetContacts);
        }

        /// <summary>
        /// 获取当前用户在指定域下的 Cookies。
        /// </summary>
        /// <param name="domain">指定的域名。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public string GetCookies(string domain)
        {
            return NativeMethods.GetCookies(Bot.Instance.AuthCode, domain).CheckError();
        }

        /// <summary>
        /// 获取当前用户的 CSRF 令牌。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public int GetCsrfToken()
        {
            return NativeMethods.GetCsrfToken(Bot.Instance.AuthCode).CheckError();
        }

        /// <summary>
        /// 获取当前用户的所有群。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public IReadOnlyCollection<IGroup> GetGroups()
        {
            using var reader = new BasicGroupInfoReader(
                NativeMethods.GetGroupsBase64(Bot.Instance.AuthCode).CheckError());

            return reader.ReadAll()
                .Select(info => new Group(info.Number, info.Name))
                .ToList();
        }

        /// <summary>
        /// 以异步操作获取当前用户的所有群。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task<IReadOnlyCollection<IGroup>> GetGroupsAsync()
        {
            return Task.Run(GetGroups);
        }
    }
}