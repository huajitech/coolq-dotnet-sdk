namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义可刷新的实例。
    /// </summary>
    public interface IRefreshable
    {
        /// <summary>
        /// 刷新当前 <see cref="IRefreshable"/> 实例。
        /// </summary>
        void Refresh();
    }
}