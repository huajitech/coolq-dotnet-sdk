namespace HuajiTech.QQ
{
    /// <summary>
    /// 用作插件的基类，并提供操作 <see cref="PluginContext"/> 的实例方法。
    /// 此类为抽象类。
    /// </summary>
    public abstract class Plugin : IPlugin
    {
        /// <summary>
        /// 以指定的 <see cref="PluginContext"/> 初始化一个 <see cref="Plugin"/> 类的新实例。
        /// </summary>
        /// <param name="context">当前 <see cref="Plugin"/> 对象所使用的 <see cref="PluginContext"/>。</param>
        protected Plugin(PluginContext context) => Context = context;

        /// <summary>
        /// 以 <see cref="PluginContext.Current"/> 初始化一个 <see cref="Plugin"/> 类的新实例。
        /// </summary>
        protected Plugin()
            : this(PluginContext.Current)
        {
        }

        /// <summary>
        /// 获取当前 <see cref="Plugin"/> 对象的 <see cref="IBot"/>。
        /// </summary>
        protected IBot Bot => Context.Bot;

        /// <summary>
        /// 获取当前 <see cref="Plugin"/> 对象的 <see cref="CurrentUser"/>。
        /// </summary>
        protected ICurrentUser CurrentUser => Bot.CurrentUser;

        /// <summary>
        /// 获取当前 <see cref="Plugin"/> 对象的 <see cref="Logger"/>。
        /// </summary>
        protected ILogger Logger => Bot.Logger;

        /// <summary>
        /// 获取当前 <see cref="Plugin"/> 对象的 <see cref="PluginContext"/>。
        /// </summary>
        protected PluginContext Context { get; }

        /// <summary>
        /// 创建指定号码的好友。
        /// </summary>
        /// <param name="number">号码。</param>
        protected IFriend Friend(long number) => Context.GetFriend(number);

        /// <summary>
        /// 创建指定号码的群。
        /// </summary>
        /// <param name="number">号码。</param>
        protected IGroup Group(long number) => Context.GetGroup(number);

        /// <summary>
        /// 创建指定号码和群的成员。
        /// </summary>
        /// <param name="number">号码。</param>
        /// <param name="group">群。</param>
        protected IMember Member(long number, IGroup group) => Context.GetMember(number, group);

        /// <summary>
        /// 创建指定号码和群号码的成员。
        /// </summary>
        /// <param name="number">号码。</param>
        /// <param name="groupNumber">群号码。</param>
        protected IMember Member(long number, long groupNumber) => Context.GetMember(number, groupNumber);

        /// <summary>
        /// 创建指定号码的用户。
        /// </summary>
        /// <param name="number">号码。</param>
        protected IUser User(long number) => Context.GetUser(number);

        /// <summary>
        /// 创建指定 ID 的消息。
        /// </summary>
        /// <param name="id">ID。</param>
        protected IMessage Message(long id) => Context.GetMessage(id);
    }
}