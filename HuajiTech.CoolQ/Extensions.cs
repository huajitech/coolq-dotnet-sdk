using HuajiTech.CoolQ.DataExchange;
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
        /// <summary>
        /// 获取在指定 <see cref="Group"/> 中表示 <see cref="User"/> 对象的 <see cref="Member"/> 对象。
        /// </summary>
        /// <param name="user">要表示为成员的 <see cref="User"/> 对象。</param>
        /// <param name="group">成员所属的 <see cref="Group"/> 对象。</param>
        /// <returns>在指定 <paramref name="group"/> 中表示 <paramref name="user"/> 的 <see cref="Member"/> 对象。</returns>
        public static Member AsMemberOf(this User user, Group group)
        {
            if (user is null)
            {
                return null;
            }

            return new Member(user.Number, group);
        }

        /// <summary>
        /// 将 <see cref="User"/> (包括其派生类) 对象转换为 <see cref="User"/> 类的实例。
        /// </summary>
        /// <param name="user">要转换的 <see cref="User"/> 对象。</param>
        /// <returns><see cref="User"/> 类的实例。</returns>
        public static User AsUser(this User user)
        {
            if (user is null)
            {
                return null;
            }

            return new User(user.Number);
        }

        /// <summary>
        /// 将 <see cref="Message.Content"/> 为编码后的正则消息匹配结果的 <see cref="Message"/> 对象解码为只读字典。
        /// </summary>
        /// <param name="message">要解析为只读字典的 <see cref="Message"/> 对象。</param>
        /// <returns>与 <see cref="Message.Content"/> 等效的只读字典。</returns>
        /// <exception cref="InvalidOperationException"><paramref name="message"/> 的值不合法。</exception>
        public static IReadOnlyDictionary<string, string> RegexDecode(this Message message)
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