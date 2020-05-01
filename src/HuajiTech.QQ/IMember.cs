namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义成员。
    /// </summary>
    public interface IMember
    {
        /// <summary>
        /// 获取当前 <see cref="IMember"/> 对象的所在群。
        /// </summary>
        IChattable Group { get; }
    }
}