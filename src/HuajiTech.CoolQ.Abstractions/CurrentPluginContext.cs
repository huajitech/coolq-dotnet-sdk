namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 提供操作 <see cref="PluginContext.Current"/> 的静态方法。
    /// </summary>
    public static class CurrentPluginContext
    {
        /// <summary>
        /// 获取 <see cref="PluginContext.Current"/> 的 <see cref="PluginContext.Bot"/>。
        /// </summary>
        public static IBot Bot => PluginContext.Current.Bot;

        /// <summary>
        /// 获取 <see cref="Bot"/> 的 <see cref="IBot.CurrentUser"/>。
        /// </summary>
        public static ICurrentUser CurrentUser => Bot.CurrentUser;

        /// <summary>
        /// 获取 <see cref="Bot"/> 的 <see cref="IBot.Logger"/>。
        /// </summary>
        public static ILogger Logger => Bot.Logger;

        /// <summary>
        /// 创建指定号码的好友。
        /// </summary>
        /// <param name="number">号码。</param>
        public static IFriend Friend(long number) => PluginContext.Current.GetFriend(number);

        /// <summary>
        /// 创建指定号码的群。
        /// </summary>
        /// <param name="number">号码。</param>
        public static IGroup Group(long number) => PluginContext.Current.GetGroup(number);

        /// <summary>
        /// 创建指定号码和群的成员。
        /// </summary>
        /// <param name="number">号码。</param>
        /// <param name="group">群。</param>
        public static IMember Member(long number, IGroup group) => PluginContext.Current.GetMember(number, group);

        /// <summary>
        /// 创建指定号码和群号码的成员。
        /// </summary>
        /// <param name="number">号码。</param>
        /// <param name="groupNumber">群号码。</param>
        public static IMember Member(long number, long groupNumber) => PluginContext.Current.GetMember(number, groupNumber);

        /// <summary>
        /// 创建指定号码的用户。
        /// </summary>
        /// <param name="number">号码。</param>
        public static IUser User(long number) => PluginContext.Current.GetUser(number);

        /// <summary>
        /// 创建指定 ID 的消息。
        /// </summary>
        /// <param name="id">ID。</param>
        public static Message Message(int id) => PluginContext.Current.GetMessage(id);
    }
}