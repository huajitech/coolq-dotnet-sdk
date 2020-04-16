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
        /// 将用户作为指定群的成员。
        /// </summary>
        /// <param name="user">用户。</param>
        /// <param name="group">指定群。</param>
        /// <returns>在指定群中表示用户的成员。</returns>
        public static Member AsMemberOf(this User user, Group group)
        {
            if (user is null)
            {
                return null;
            }

            return new Member(user.Number, group);
        }

        /// <summary>
        /// 将用户作为 <see cref="User"/> 对象。
        /// </summary>
        /// <param name="user">用户。</param>
        /// <returns><see cref="User"/> 对象。</returns>
        public static User AsUser(this User user)
        {
            if (user is null)
            {
                return null;
            }

            return new User(user.Number);
        }

        /// <summary>
        /// 解码正则消息。
        /// </summary>
        /// <param name="message">要解码的消息。</param>
        /// <returns>解码后的字典。</returns>
        public static IReadOnlyDictionary<string, string> RegexDecode(this Message message)
        {
            if (message is null)
            {
                return null;
            }

            using var reader = new StringKeyValuePairReader(message.Content);
            return reader.ReadAll().ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        internal static int CheckError(this int returnValue)
        {
            if (returnValue < 0)
            {
                throw new CoolQException(string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
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