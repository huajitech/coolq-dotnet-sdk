namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义用户。
    /// </summary>
    public interface IUser : IChattable, IRequestable, IRefreshable
    {
        /// <summary>
        /// 获取当前 <see cref="IUser"/> 对象的昵称。
        /// </summary>
        string? Nickname { get; }

        /// <summary>
        /// 给予当前 <see cref="IUser"/> 对象指定数量的赞。
        /// </summary>
        /// <param name="count">赞的数量。</param>
        void GiveThumbsUp(int count);
    }
}