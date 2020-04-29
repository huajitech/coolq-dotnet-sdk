using System.Collections.Generic;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示当前用户。
    /// 此类为抽象类。
    /// </summary>
    public abstract class CurrentUser : User
    {
        /// <summary>
        /// 以指定的号码初始化一个 <see cref="CurrentUser"/> 类的新实例。
        /// </summary>
        /// <param name="number">号码。</param>
        protected CurrentUser(long number)
            : base(number)
        {
        }

        /// <summary>
        /// 获取当前 <see cref="CurrentUser"/> 对象的所有联系人。
        /// </summary>
        public abstract IReadOnlyCollection<Contact> GetContacts();

        /// <summary>
        /// 以异步操作获取当前 <see cref="CurrentUser"/> 对象的所有联系人。
        /// </summary>
        public virtual Task<IReadOnlyCollection<Contact>> GetContactsAsync() => Task.Run(GetContacts);

        /// <summary>
        /// 获取当前 <see cref="CurrentUser"/> 对象在指定域下的 Cookies。
        /// </summary>
        public abstract string GetCookies(string domain);

        /// <summary>
        /// 获取当前 <see cref="CurrentUser"/> 对象的 CSRF 令牌。
        /// </summary>
        public abstract int GetCsrfToken();

        /// <summary>
        /// 获取当前 <see cref="CurrentUser"/> 对象的所有群。
        /// </summary>
        public abstract IReadOnlyCollection<Group> GetGroups();

        /// <summary>
        /// 以异步操作获取当前 <see cref="CurrentUser"/> 对象的所有群。
        /// </summary>
        public virtual Task<IReadOnlyCollection<Group>> GetGroupsAsync() => Task.Run(GetGroups);
    }
}