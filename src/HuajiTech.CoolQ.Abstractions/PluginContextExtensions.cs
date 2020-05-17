using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义用于操作 <see cref="PluginContext"/> 的扩展方法。
    /// </summary>
    public static class PluginContextExtensions
    {
        public static IUser? AsUser(this IUser user, PluginContext context)
        {
            if (user is null)
            {
                return null;
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.GetUser(user.Number);
        }

        public static IUser? AsUser(this IUser user) => AsUser(user, PluginContext.Current);

        public static IMember? AsMemberOf(this IUser user, IGroup group, PluginContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.GetMember(user, group);
        }

        public static IMember? AsMemberOf(this IUser user, IGroup group) =>
            AsMemberOf(user, group, PluginContext.Current);

        public static IMember? AsMemberOf(this IUser user, long groupNumber, PluginContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.GetMember(user, groupNumber);
        }

        public static IMember? AsMemberOf(this IUser user, long groupNumber) =>
            AsMemberOf(user, groupNumber, PluginContext.Current);

        public static TException? LogAsWarning<TException>(this TException exception, ILogger logger)
            where TException : notnull, Exception
        {
            if (exception is null)
            {
                return null;
            }

            logger?.LogWarning(exception);
            return exception;
        }

        public static TException? LogAsWarning<TException>(this TException exception, PluginContext context)
            where TException : Exception
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return exception.LogAsWarning(context.Bot.Logger);
        }

        public static TException? LogAsWarning<TException>(this TException exception)
            where TException : Exception =>
            LogAsWarning(exception, PluginContext.Current);

        public static TException? LogAsError<TException>(this TException exception, ILogger logger)
             where TException : Exception
        {
            if (exception is null)
            {
                return null;
            }

            logger?.LogError(exception);
            return exception;
        }

        public static TException? LogAsError<TException>(this TException exception, PluginContext context)
            where TException : Exception
        {
            if (exception is null)
            {
                return null;
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return LogAsError(exception, context.Bot.Logger);
        }

        public static TException? LogAsError<TException>(this TException exception)
            where TException : Exception =>
            LogAsError(exception, PluginContext.Current);
    }
}