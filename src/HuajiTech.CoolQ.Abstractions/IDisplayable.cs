namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 提供用于显示的属性。
    /// </summary>
    public interface IDisplayable
    {
        /// <summary>
        /// 获取当前 <see cref="IDisplayable"/> 实例的显示名称。
        /// </summary>
        string DisplayName { get; }
    }
}