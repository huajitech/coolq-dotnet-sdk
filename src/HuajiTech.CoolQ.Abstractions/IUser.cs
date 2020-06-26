namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义用户。
    /// </summary>
    public interface IUser : IChattable, IRequestable
    {
        /// <summary>
        /// 获取当前 <see cref="IUser"/> 实例的昵称。
        /// </summary>
        string? Nickname { get; }

        /// <summary>
        /// 获取当前 <see cref="IUser"/> 实例的年龄。
        /// </summary>
        public int Age { get; }

        /// <summary>
        /// 获取当前 <see cref="IUser"/> 实例的性别。
        /// </summary>
        public Gender Gender { get; }

        /// <summary>
        /// 给予当前 <see cref="IUser"/> 实例指定数量的赞。
        /// </summary>
        /// <param name="count">赞的数量。</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Naming", "CA1716:标识符不应与关键字匹配", Justification = "<挂起>")]
        void Like(int count = 1);
    }
}