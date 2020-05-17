namespace HuajiTech.CoolQ
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
    }
}