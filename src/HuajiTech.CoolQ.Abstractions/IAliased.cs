namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义有别名的对象。
    /// </summary>
    public interface IAliased
    {
        /// <summary>
        /// 获取当前 <see cref="IAliased"/> 对象的别名。
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Naming", "CA1716:标识符不应与关键字匹配", Justification = "<挂起>")]
        string? Alias { get; }
    }
}