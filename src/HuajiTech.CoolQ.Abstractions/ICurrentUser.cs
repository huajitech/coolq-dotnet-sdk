using System.Collections.Generic;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义当前用户。
    /// </summary>
    public interface ICurrentUser : IUser
    {
        /// <summary>
        /// 获取当前 <see cref="ICurrentUser"/> 对象的所有好友。
        /// </summary>
        IReadOnlyCollection<IFriend> GetFriends();

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