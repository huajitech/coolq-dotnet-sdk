namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义用户。
    /// 此类为抽象类。
    /// </summary>
    public interface IUser : IChattable, IUserInfo, IRequestable, IRefreshable
    {
        /// <summary>
        /// 给予当前 <see cref="IUser"/> 对象指定数量的赞。
        /// </summary>
        /// <param name="count">赞的数量。</param>
        void GiveThumbsUp(int count);
    }
}