namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义用于操作 <see cref="PluginContext.Current"/> 的扩展方法。
    /// </summary>
    public static class CurrentPluginContextExtensions
    {
        public static User AsUser(this User user)
        {
            return PluginContext.Current.GetUser(user);
        }

        public static Member AsMemberOf(this User user, Group group)
        {
            return PluginContext.Current.GetMember(user, group);
        }

        public static Member AsMemberOf(this User user, long groupNumber)
        {
            return PluginContext.Current.GetMember(user, groupNumber);
        }
    }
}