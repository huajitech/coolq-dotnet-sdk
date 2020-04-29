using System;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义用于操作 <see cref="PluginContext.Current"/> 的扩展方法。
    /// </summary>
    public static class CurrentPluginContextExtensions
    {
        public static User AsUser(this User user) => PluginContext.Current.GetUser(user);

        public static Member AsMemberOf(this User user, Group group) => PluginContext.Current.GetMember(user, group);

        public static Member AsMemberOf(this User user, long groupNumber) => PluginContext.Current.GetMember(user, groupNumber);

        public static void LogAsWarning(this Exception ex) => PluginContext.Current.Bot.Logger.LogWarning(ex);

        public static void LogAsError(this Exception ex) => PluginContext.Current.Bot.Logger.LogError(ex);
    }
}