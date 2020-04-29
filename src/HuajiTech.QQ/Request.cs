using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示请求。
    /// 此类为抽象类。
    /// </summary>
    public abstract class Request
    {
        /// <summary>
        /// 获取当前 <see cref="Request"/> 对象的消息。
        /// </summary>
        public abstract string Message { get; }

        /// <summary>
        /// 同意当前请求。
        /// </summary>
        public abstract void Accept();

        /// <summary>
        /// 以异步操作同意当前请求。
        /// </summary>
        public virtual Task AcceptAsync() => Task.Run(Accept);

        /// <summary>
        /// 拒绝当前请求。
        /// </summary>
        public abstract void Reject();

        /// <summary>
        /// 以异步操作拒绝当前请求。
        /// </summary>
        public virtual Task RejectAsync() => Task.Run(Reject);
    }
}