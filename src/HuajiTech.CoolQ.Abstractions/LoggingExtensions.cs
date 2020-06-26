using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace HuajiTech.CoolQ
{
    public static class LoggingExtensions
    {
        public static void LogDebug(this ILogger logger, string message)
        {
            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.LogDebug(AbstractionResources.Debug, message);
        }

        public static void Log(this ILogger logger, string message)
        {
            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.Log(AbstractionResources.Info, message);
        }

        public static void LogSending(this ILogger logger, string message)
        {
            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.LogSending(AbstractionResources.Sending, message);
        }

        public static void LogReceiving(this ILogger logger, string message)
        {
            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.LogReceiving(AbstractionResources.Receiving, message);
        }

        public static void LogSuccess(this ILogger logger, string message)
        {
            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.LogSuccess(AbstractionResources.Success, message);
        }

        public static void LogWarning(this ILogger logger, string message)
        {
            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.LogWarning(AbstractionResources.Warning, message);
        }

        public static void LogError(this ILogger logger, string message)
        {
            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.LogError(AbstractionResources.Error, message);
        }

        public static TException LogAsWarning<TException>(this TException exception, ILogger logger)
            where TException : notnull, Exception
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.LogWarning(AbstractionResources.Exception, exception.ToString());
            return exception;
        }

        public static TException LogAsWarning<TException>(this TException exception)
            where TException : notnull, Exception
            => exception.LogAsWarning(PluginContext.Current.Bot.Logger);

        public static TException LogAsError<TException>(this TException exception, ILogger logger)
             where TException : notnull, Exception
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            logger.LogError(AbstractionResources.Exception, exception.ToString());
            return exception;
        }

        public static TException LogAsError<TException>(this TException exception)
            where TException : notnull, Exception
            => exception.LogAsError(PluginContext.Current.Bot.Logger);

        /// <summary>
        /// 将指定异常及其内部异常的信息发送至指定的 <see cref="ISendee"/> 实例。
        /// </summary>
        /// <param name="exception">要发送的异常。</param>
        /// <param name="sendee">异常发送的目标。</param>
        /// <param name="filePath">包含对该方法的调用的源文件路径。</param>
        /// <param name="lineNumber">包含对该方法的调用的源文件行号。</param>
        /// <returns>一个 <see cref="Message"/> 实例，表示已发送的消息。如果发送失败，则为 <see langword="null"/>。</returns>
        public static Message? SendTo(
            this Exception? exception,
            ISendee? sendee,
            [CallerFilePath] string filePath = "?",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (exception is null || sendee is null)
            {
                return null;
            }

            var messageBuffer = new StringBuilder();

            while (!(exception is null))
            {
                messageBuffer.AppendLine(exception.Message);
                exception = exception.InnerException;
            }

            var message = string.Format(
                    CultureInfo.CurrentCulture,
                    AbstractionResources.ExceptionRaised,
                    messageBuffer.ToString(),
                    Path.GetFileName(filePath),
                    lineNumber).Trim();

            try
            {
                return sendee.Send(message);
            }
            catch (ApiException)
            {
            }

            return null;
        }
    }
}