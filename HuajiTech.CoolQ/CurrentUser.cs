using HuajiTech.CoolQ.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示当前用户，并提供与当前用户交互的静态方法、事件和属性。
    /// </summary>
    public partial class CurrentUser : User
    {
        internal CurrentUser()
            : base(NativeMethods.GetCurrentUserNumber(Bot.AuthCode))
        {
        }

        /// <summary>
        /// 获取当前用户的昵称。
        /// </summary>
        public override string Nickname =>
            NativeMethods.GetCurrentUserNickname(Bot.AuthCode);

        /// <summary>
        /// 在收到匿名消息时引发。
        /// </summary>
        public static event EventHandler<AnonymousMessageReceivedEventArgs> AnonymousMessageReceived;

        /// <summary>
        /// 在好友已添加时引发。
        /// </summary>
        public static event EventHandler<ContactAddedEventArgs> ContactAdded;

        /// <summary>
        /// 在收到好友请求时引发。
        /// </summary>
        public static event EventHandler<ContactRequestedEventArgs> ContactRequested;

        /// <summary>
        /// 在被邀请加群时引发。
        /// </summary>
        public static event EventHandler<EntranceInvitedEventArgs> EntranceInvited;

        /// <summary>
        /// 在收到消息时引发。
        /// </summary>
        public static event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        /// 获取当前用户的所有联系人。
        /// </summary>
        public static IReadOnlyCollection<Contact> GetContacts()
        {
            return GetContactInfos()
                .Select(info => new Contact(info))
                .ToList();
        }

        /// <summary>
        /// 以异步操作获取当前用户的所有联系人。
        /// </summary>
        public static Task<IReadOnlyCollection<Contact>> GetContactsAsync()
        {
            return Task.Run(GetContacts);
        }

        /// <summary>
        /// 获取当前用户在指定域下的 Cookies。
        /// </summary>
        /// <param name="domain">指定的域名。</param>
        public static string GetCookies(string domain)
        {
            return NativeMethods.GetCookies(Bot.AuthCode, domain).CheckError();
        }

        /// <summary>
        /// 以异步操作获取当前用户在指定域下的 Cookies。
        /// </summary>
        /// <param name="domain">指定的域名。</param>
        public static Task<string> GetCookiesAsync(string domain)
        {
            return Task.Run(() => GetCookies(domain));
        }

        /// <summary>
        /// 获取当前用户的 CSRF 令牌。
        /// </summary>
        public static int GetCsrfToken()
        {
            return NativeMethods.GetCsrfToken(Bot.AuthCode).CheckError();
        }

        /// <summary>
        /// 以异步操作获取当前用户的 CSRF 令牌。
        /// </summary>
        public static Task<int> GetCsrfTokenAsync()
        {
            return Task.Run(GetCsrfToken);
        }

        /// <summary>
        /// 获取当前用户的所有群。
        /// </summary>
        public static IReadOnlyCollection<Group> GetGroups()
        {
            using var reader = new BasicGroupInfoReader(
                NativeMethods.GetGroupsBase64(Bot.AuthCode));

            return reader.ReadAll()
                .Select(info => new Group(info.Number, info.Name))
                .ToList();
        }

        /// <summary>
        /// 以异步操作获取当前用户的所有群。
        /// </summary>
        public static Task<IReadOnlyCollection<Group>> GetGroupsAsync()
        {
            return Task.Run(GetGroups);
        }

        internal static IReadOnlyCollection<ContactInfo> GetContactInfos()
        {
            using var reader = new ContactInfoReader(
                NativeMethods.GetContactsBase64(Bot.AuthCode, false));

            return reader.ReadAll().ToList();
        }
    }
}