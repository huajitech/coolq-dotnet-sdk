namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义有别名的对象。
    /// </summary>
    public interface IAliased
    {
        /// <summary>
        /// 获取当前 <see cref="IAliased"/> 对象的别名。
        /// </summary>
        string Alias { get; }
    }
}