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
        void Accept(string alias);
    }
}