using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义可请求的对象。
    /// </summary>
    public interface IRequestable
    {
        /// <summary>
        /// 获取一个值，指示当前 <see cref="IRequestable"/> 对象是否已请求。
        /// </summary>
        bool HasRequested { get; }

        /// <summary>
        /// 请求当前 <see cref="IRequestable"/> 对象。
        /// </summary>
        void Request();

        /// <summary>
        /// 以异步操作请求当前 <see cref="IRequestable"/> 对象。
        /// </summary>
        Task RequestAsync();
    }
}