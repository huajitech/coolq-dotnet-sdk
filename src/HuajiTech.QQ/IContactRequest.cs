namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示联系人请求。
    /// 此类为抽象类。
    /// </summary>
    public interface IContactRequest : IRequest
    {
        /// <summary>
        /// 同意当前请求，并为联系人设置别名。
        /// </summary>
        /// <param name="alias">要设置的别名。</param>
        void Accept(string alias);
    }
}