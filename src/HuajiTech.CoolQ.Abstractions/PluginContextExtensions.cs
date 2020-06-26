using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义用于操作 <see cref="PluginContext"/> 的扩展方法。
    /// </summary>
    public static class PluginContextExtensions
    {
        public static IUser AsUser(this IUser user, PluginContext context)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.GetUser(user.Number);
        }

        public static IUser AsUser(this IUser user) => user.AsUser(PluginContext.Current);

        public static IMember AsMemberOf(this IUser user, IGroup group, PluginContext context)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.GetMember(user, group);
        }

        public static IMember AsMemberOf(this IUser user, IGroup group)
            => user.AsMemberOf(group, PluginContext.Current);

        public static IMember? AsMemberOf(this IUser user, long groupNumber, PluginContext context)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.GetMember(user, groupNumber);
        }

        public static IMember? AsMemberOf(this IUser user, long groupNumber)
            => user.AsMemberOf(groupNumber, PluginContext.Current);
    }
}