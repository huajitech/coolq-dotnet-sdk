using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HuajiTech.CoolQ.Interop;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 提供用于操作正则消息的扩展方法。
    /// </summary>
    public static class RegexMessage
    {
        /// <summary>
        /// 将 <see cref="Message.Content"/> 为编码后的正则消息匹配结果的 <see cref="Message"/> 实例解码为只读字典。
        /// </summary>
        /// <param name="message">要解析为只读字典的 <see cref="Message"/> 实例。</param>
        /// <returns>与 <see cref="Message.Content"/> 等效的只读字典。</returns>
        /// <exception cref="InvalidDataException"><paramref name="message"/> 不合法。</exception>
        public static IReadOnlyDictionary<string, string>? Decode(this Message message)
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
                throw new InvalidDataException(CoreResources.FailedToDecodeRegexMessage, ex);
            }
        }
    }
}