namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义可显示的实例。
    /// </summary>
    public interface IDisplayable
    {
        /// <summary>
        /// 获取当前 <see cref="IDisplayable"/> 实例的显示名称。
        /// </summary>
        string DisplayName { get; }
    }
}