namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示联系人。
    /// 此类为抽象类。
    /// </summary>
    public abstract class Contact : User, IAliased
    {
        /// <summary>
        /// 以指定的号码初始化一个 <see cref="Contact"/> 类的新实例。
        /// </summary>
        /// <param name="number">号码。</param>
        protected Contact(long number)
            : base(number)
        {
        }

        public abstract string Alias { get; }
    }
}