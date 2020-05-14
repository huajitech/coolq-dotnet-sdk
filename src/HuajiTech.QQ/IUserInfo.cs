namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义用户信息。
    /// </summary>
    public interface IUserInfo
    {
        /// <summary>
        /// 获取当前 <see cref="IUser"/> 对象的昵称。
        /// </summary>
        string? Nickname { get; }
    }
}