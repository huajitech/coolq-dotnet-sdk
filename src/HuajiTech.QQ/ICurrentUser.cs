using System.Collections.Generic;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示当前用户。
    /// 此类为抽象类。
    /// </summary>
    public interface ICurrentUser : IUser
    {
        /// <summary>
        /// 获取当前 <see cref="ICurrentUser"/> 对象的所有联系人。
        /// </summary>
        IReadOnlyCollection<IContact> GetContacts();

        /// <summary>
        /// 获取当前 <see cref="ICurrentUser"/> 对象在指定域下的 Cookies。
        /// </summary>
        string GetCookies(string domain);

        /// <summary>
        /// 获取当前 <see cref="ICurrentUser"/> 对象的 CSRF 令牌。
        /// </summary>
        int GetCsrfToken();

        /// <summary>
        /// 获取当前 <see cref="ICurrentUser"/> 对象的所有群。
        /// </summary>
        IReadOnlyCollection<IGroup> GetGroups();
    }
}