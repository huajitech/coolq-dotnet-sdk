using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义用于操作 <see cref="PluginContext"/> 的扩展方法。
    /// </summary>
    public static class PluginContextExtensions
    {
        public static IUser? AsUser(this IUser? user, PluginContext? context = null)
        {
            if (user is null)
            {
                return null;
            }

            context ??= PluginContext.Current;

            return context.GetUser(user.Number);
        }

        public static IMember? AsMemberOf(this IUser? user, IGroup group, PluginContext? context = null)
        {
            if (user is null)
            {
                return null;
            }

            context ??= PluginContext.Current;

            return context.GetMember(user, group);
        }

        public static IMember? AsMemberOf(this IUser? user, long groupNumber, PluginContext? context = null)
        {
            if (user is null)
            {
                return null;
            }

            context ??= PluginContext.Current;

            return context.GetMember(user, groupNumber);
        }

        public static TException? LogAsWarning<TException>(this TException? exception, ILogger? logger = null)
            where TException : notnull, Exception
        {
            if (exception is null)
            {
                return null;
            }

            logger ??= PluginContext.Current.Bot.Logger;

            logger.LogWarning(exception);
            return exception;
        }

        public static TException? LogAsError<TException>(this TException? exception, ILogger? logger = null)
             where TException : Exception
        {
            if (exception is null)
            {
                return null;
            }

            logger ??= PluginContext.Current.Bot.Logger;

            logger.LogError(exception);
            return exception;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design", "CA1031:不捕获常规异常类型", Justification = "<挂起>")]
        public static IMessage? SendToOrLogAsWarning(
            this Exception? exception,
            ISendee? sendee,
            ILogger? logger = null,
            [CallerMemberName] string callerMemberName = "?",
            [CallerFilePath] string callerFilePath = "?",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            if (exception is null || sendee is null)
            {
                return null;
            }

            var message = string.Format(
                    System.Globalization.CultureInfo.CurrentCulture,
                    AbstractionResources.ExceptionRaised,
                    exception.GetType().FullName,
                    exception.Message,
                    callerMemberName,
                    callerFilePath,
                    callerLineNumber);

            try
            {
                return sendee.Send(message);
            }
            catch
            {
                logger ??= PluginContext.Current.Bot.Logger;
                logger.LogWarning(AbstractionResources.FailedToSendException, message);
            }

            return null;
        }
    }
}