using HuajiTech.CoolQ.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示当前用户，并提供与当前用户交互的静态方法、事件和属性。
    /// 可通过 <seealso cref="Bot.CurrentUser"/> 属性获取 <see cref="CurrentUser"/> 类的实例。
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
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public static IReadOnlyCollection<Contact> GetContacts()
        {
            return GetContactInfos()
                .Select(info => new Contact(info))
                .ToList();
        }

        /// <summary>
        /// 以异步操作获取当前用户的所有联系人。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public static Task<IReadOnlyCollection<Contact>> GetContactsAsync()
        {
            return Task.Run(GetContacts);
        }

        /// <summary>
        /// 获取当前用户在指定域下的 Cookies。
        /// </summary>
        /// <param name="domain">指定的域名。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public static string GetCookies(string domain)
        {
            return NativeMethods.GetCookies(Bot.AuthCode, domain).CheckError();
        }

        /// <summary>
        /// 获取当前用户的 CSRF 令牌。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public static int GetCsrfToken()
        {
            return NativeMethods.GetCsrfToken(Bot.AuthCode).CheckError();
        }

        /// <summary>
        /// 获取当前用户的所有群。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public static IReadOnlyCollection<Group> GetGroups()
        {
            using var reader = new BasicGroupInfoReader(
                NativeMethods.GetGroupsBase64(Bot.AuthCode).CheckError());

            return reader.ReadAll()
                .Select(info => new Group(info.Number, info.Name))
                .ToList();
        }

        /// <summary>
        /// 以异步操作获取当前用户的所有群。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public static Task<IReadOnlyCollection<Group>> GetGroupsAsync()
        {
            return Task.Run(GetGroups);
        }

        internal static IReadOnlyCollection<ContactInfo> GetContactInfos()
        {
            using var reader = new ContactInfoReader(
                NativeMethods.GetContactsBase64(Bot.AuthCode, false).CheckError());

            return reader.ReadAll().ToList();
        }
    }
}