using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义可刷新的对象。
    /// </summary>
    public interface IRefreshable
    {
        /// <summary>
        /// 刷新当前 <see cref="IRefreshable"/> 对象。
        /// </summary>
        void Refresh();

        /// <summary>
        /// 以异步操作刷新当前 <see cref="IRefreshable"/> 对象。
        /// </summary>
        Task RefreshAsync();
    }
}