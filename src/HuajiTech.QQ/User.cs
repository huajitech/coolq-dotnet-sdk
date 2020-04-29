using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示用户。
    /// 此类为抽象类。
    /// </summary>
    public abstract class User : Chat, IRequestable, IRefreshable
    {
        /// <summary>
        /// 以指定的号码初始化一个 <see cref="User"/> 类的新实例。
        /// </summary>
        /// <param name="number">号码。</param>
        protected User(long number)
            : base(number)
        {
        }

        /// <summary>
        /// 获取当前 <see cref="User"/> 对象的昵称。
        /// </summary>
        public abstract string Nickname { get; }

        public abstract bool HasRequested { get; }

        /// <summary>
        /// 给予当前 <see cref="User"/> 对象指定数量的赞。
        /// </summary>
        /// <param name="count">赞的数量。</param>
        public abstract void GiveThumbsUp(int count);

        /// <summary>
        /// 以异步操作给予当前 <see cref="User"/> 对象指定数量的赞。
        /// </summary>
        /// <param name="count">赞的数量。</param>
        public virtual Task GiveThumbsUpAsync(int count)
        {
            return Task.Run(() => GiveThumbsUp(count));
        }

        public abstract void Refresh();

        public virtual Task RefreshAsync()
        {
            return Task.Run(Refresh);
        }

        public abstract void Request();

        public virtual Task RequestAsync()
        {
            return Task.Run(Request);
        }
    }
}