namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义群信息。
    /// </summary>
    public interface IGroupInfo
    {
        /// <summary>
        /// 获取当前 <see cref="IGroupInfo"/> 对象的成员容量。
        /// </summary>
        int MemberCapacity { get; }

        /// <summary>
        /// 获取当前 <see cref="IGroupInfo"/> 对象的成员数。
        /// </summary>
        int MemberCount { get; }

        /// <summary>
        /// 获取当前 <see cref="IGroupInfo"/> 对象的名称。
        /// </summary>
        public abstract string? Name { get; }
    }
}