namespace HuajiTech.QQ
{
    /// <summary>
    /// 用作插件的基类，并提供操作 <see cref="PluginContext"/> 的方法。
    /// </summary>
    public abstract class Plugin
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
        /// 当前 <see cref="Plugin"/> 对象的 <see cref="IBot"/>。
        /// </summary>
        protected IBot Bot => Context.Bot;

        /// <summary>
        /// 当前 <see cref="Plugin"/> 对象的 <see cref="CurrentUser"/>。
        /// </summary>
        protected CurrentUser CurrentUser => Bot.CurrentUser;

        /// <summary>
        /// 当前 <see cref="Plugin"/> 对象的 <see cref="Logger"/>。
        /// </summary>
        protected Logger Logger => Bot.Logger;

        /// <summary>
        /// 获取当前 <see cref="Plugin"/> 对象的 <see cref="PluginContext"/>。
        /// </summary>
        protected PluginContext Context { get; }

        /// <summary>
        /// 创建指定号码的联系人。
        /// </summary>
        /// <param name="number">号码。</param>
        protected Contact Contact(long number) => Context.GetContact(number);

        /// <summary>
        /// 创建指定号码的群。
        /// </summary>
        /// <param name="number">号码。</param>
        protected Group Group(long number) => Context.GetGroup(number);

        /// <summary>
        /// 创建指定号码和群的成员。
        /// </summary>
        /// <param name="number">号码。</param>
        /// <param name="group">群。</param>
        protected Member Member(long number, Group group) => Context.GetMember(number, group);

        /// <summary>
        /// 创建指定号码和群号码的成员。
        /// </summary>
        /// <param name="number">号码。</param>
        /// <param name="groupNumber">群号码。</param>
        protected Member Member(long number, long groupNumber) => Context.GetMember(number, groupNumber);

        /// <summary>
        /// 创建指定号码的用户。
        /// </summary>
        /// <param name="number">号码。</param>
        protected User User(long number) => Context.GetUser(number);
    }
}