using HuajiTech.CoolQ.DataExchange;
using HuajiTech.QQ;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义扩展方法。
    /// </summary>
    public static class Extensions
    {
        public static IUser AsUser(this IUser user)
        {
            return QQ.AppContext.CurrentContext.GetUser(user);
        }

        public static IMember AsMemberOf(this IUser user, IGroup group)
        {
            return QQ.AppContext.CurrentContext.GetMember(user, group);
        }

        /// <summary>
        /// 将 <see cref="IMessage.Content"/> 为编码后的正则消息匹配结果的 <see cref="IMessage"/> 对象解码为只读字典。
        /// </summary>
        /// <param name="message">要解析为只读字典的 <see cref="Message"/> 对象。</param>
        /// <returns>与 <see cref="Message.Content"/> 等效的只读字典。</returns>
        /// <exception cref="InvalidOperationException"><paramref name="message"/> 的值不合法。</exception>
        public static IReadOnlyDictionary<string, string> RegexDecode(this IMessage message)
        {
            if (message is null)
            {
                return null;
            }

            using var reader = new StringKeyValuePairReader(message.Content);

            try
            {
                return reader.ReadAll().ToDictionary(pair => pair.Key, pair => pair.Value);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resources.FailedToDecodeRegexMessage, ex);
            }
        }

        internal static int CheckError(this int returnValue)
        {
            if (returnValue < 0)
            {
                throw new CoolQException(string.Format(
                    System.Globalization.CultureInfo.CurrentCulture,
                    Resources.UnexpectedReturnValue,
                    returnValue));
            }

            return returnValue;
        }

        internal static T CheckError<T>(this T returnValue)
        {
            if (returnValue is null)
            {
                throw new CoolQException(Resources.NullReturnValue);
            }

            return returnValue;
        }
    }
}