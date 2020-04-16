using HuajiTech.CoolQ.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 提供与当前用户交互的方法、事件和属性的静态类。
    /// </summary>
    public static partial class CurrentUser
    {
        private static User _currentUser;

        /// <summary>
        /// 获取当前用户的昵称。
        /// </summary>
        public static string Nickname =>
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
        /// 将当前用户作为指定群的成员。
        /// </summary>
        /// <param name="group">指定的群。</param>
        /// <returns>指定群中表示当前用户的成员。</returns>
        public static Member AsMemberOf(Group group)
        {
            return AsUser().AsMemberOf(group);
        }

        /// <summary>
        /// 将当前用户作为 <see cref="User"/> 类的实例。
        /// </summary>
        /// <returns>一个 <see cref="User"/> 类的实例，表示当前用户。</returns>
        public static User AsUser()
        {
            _currentUser ??= new User(NativeMethods.GetCurrentUserNumber(Bot.AuthCode));
            return _currentUser;
        }

        /// <summary>
        /// 获取当前用户的所有联系人。
        /// </summary>
        public static IReadOnlyList<Contact> GetContacts()
        {
            using var reader = new ContactInfoReader(
                NativeMethods.GetContactsBase64(Bot.AuthCode, false));

            return reader.ReadAll()
                .Select(info => new Contact(info))
                .ToList();
        }

        /// <summary>
        /// 以异步操作获取当前用户的所有联系人。
        /// </summary>
        public static Task<IReadOnlyList<Contact>> GetContactsAsync()
        {
            return Task.Run(GetContacts);
        }

        /// <summary>
        /// 获取当前用户在指定域下的 Cookies。
        /// </summary>
        /// <param name="domain">指定的域名。</param>
        public static string GetCookies(string domain)
        {
            var cookies = NativeMethods.GetCookies(Bot.AuthCode, domain);

            if (cookies is null)
            {
                throw new CoolQException(Resources.NullReturnValue);
            }

            return cookies;
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
        public static IReadOnlyList<Group> GetGroups()
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
        public static Task<IReadOnlyList<Group>> GetGroupsAsync()
        {
            return Task.Run(GetGroups);
        }
    }
}