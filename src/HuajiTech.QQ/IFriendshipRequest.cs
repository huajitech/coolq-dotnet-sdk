namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示好友请求。
    /// 此类为抽象类。
    /// </summary>
    public interface IFriendshipRequest : IRequest
    {
        /// <summary>
        /// 同意当前请求，并为好友设置别名。
        /// </summary>
        /// <param name="alias">要设置的别名。</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Naming", "CA1716:标识符不应与关键字匹配", Justification = "<挂起>")]
        void Accept(string alias);
    }
}