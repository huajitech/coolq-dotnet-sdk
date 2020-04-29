using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示联系人请求。
    /// 此类为抽象类。
    /// </summary>
    public abstract class ContactRequest : Request
    {
        /// <summary>
        /// 同意当前请求，并为联系人设置别名。
        /// </summary>
        /// <param name="alias">要设置的别名。</param>
        public abstract void Accept(string alias);

        /// <summary>
        /// 以异步操作同意当前请求，并为联系人设置别名。
        /// </summary>
        /// <param name="alias">要设置的别名。</param>
        public virtual Task AcceptAsync(string alias) => Task.Run(() => Accept(alias));

        public override void Accept() => Accept(null);

        public override Task AcceptAsync() => AcceptAsync(null);
    }
}