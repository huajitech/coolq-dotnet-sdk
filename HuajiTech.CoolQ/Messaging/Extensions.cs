using System;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 定义扩展方法。
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 创建目标为指定用户的 <see cref="Messaging.At"/> 类的新实例。
        /// </summary>
        /// <param name="target">At (@) 的目标。</param>
        /// <returns>目标为指定用户的 <see cref="Messaging.At"/> 类的新实例。</returns>
        public static At At(this User target)
        {
            return new At
            {
                Target = target ?? throw new ArgumentNullException(nameof(target))
            };
        }

        /// <summary>
        /// 将 <see cref="Message"/> 解析为 <see cref="ComplexMessage"/> 对象。
        /// </summary>
        /// <param name="message">要解析为 <see cref="ComplexMessage"/> 对象的 <see cref="Message"/> 对象。</param>
        /// <returns>与<see cref="Message"/> 对象等效的 <see cref="ComplexMessage"/> 对象。</returns>
        public static ComplexMessage Parse(this Message message)
        {
            return ComplexMessage.Parse(message?.Content);
        }

        /// <summary>
        /// 向指定聊天发送 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <param name="chat">目标聊天。</param>
        /// <param name="message">要发送的 <see cref="ComplexMessage"/> 对象。</param>
        /// <returns>一个 <see cref="Message"/> 对象，表示已发送的消息。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="chat"/> 为 <c>null</c>。</exception>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        /// <exception cref="ArgumentException"><paramref name="message"/> 不包含任何元素，或其等效字符串表示形式为 <see cref="string.Empty"/>。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示发送失败的值。</exception>
        public static Message Send(this Chat chat, ComplexMessage message)
        {
            if (chat is null)
            {
                throw new ArgumentNullException(nameof(chat));
            }

            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return chat.Send(message.ToString());
        }

        /// <summary>
        /// 以异步操作向指定聊天发送 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <param name="chat">目标聊天。</param>
        /// <param name="message">要发送的 <see cref="ComplexMessage"/> 对象。</param>
        /// <returns>一个 <see cref="Message"/> 对象，表示已发送的消息。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="chat"/> 为 <c>null</c>。</exception>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        /// <exception cref="ArgumentException"><paramref name="message"/> 不包含任何元素，或其等效字符串表示形式为 <see cref="string.Empty"/>。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示发送失败的值。</exception>
        public static Task<Message> SendAsync(this Chat chat, ComplexMessage message)
        {
            return Task.Run(() => Send(chat, message));
        }

        /// <summary>
        /// 向指定聊天发送 <see cref="MessageElement"/>。
        /// </summary>
        /// <param name="chat">目标聊天。</param>
        /// <param name="element">要发送的 <see cref="MessageElement"/> 对象。</param>
        /// <returns>一个 <see cref="Message"/> 对象，表示已发送的消息。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="chat"/> 为 <c>null</c>。</exception>
        /// <exception cref="ArgumentNullException"><paramref name="element"/> 为 <c>null</c>。</exception>
        /// <exception cref="ArgumentException"><paramref name="element"/> 的等效字符串表示形式为 <see cref="string.Empty"/>。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示发送失败的值。</exception>
        public static Message Send(this Chat chat, MessageElement element)
        {
            if (chat is null)
            {
                throw new ArgumentNullException(nameof(chat));
            }

            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return chat.Send(element.ToString());
        }

        /// <summary>
        /// 以异步操作向指定聊天发送 <see cref="MessageElement"/>。
        /// </summary>
        /// <param name="chat">目标聊天。</param>
        /// <param name="element">要发送的 <see cref="MessageElement"/> 对象。</param>
        /// <returns>一个 <see cref="Message"/> 对象，表示已发送的消息。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="chat"/> 为 <c>null</c>。</exception>
        /// <exception cref="ArgumentNullException"><paramref name="element"/> 为 <c>null</c>。</exception>
        /// <exception cref="ArgumentException"><paramref name="element"/> 的等效字符串表示形式为 <see cref="string.Empty"/>。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示发送失败的值。</exception>
        public static Task<Message> SendAsync(this Chat chat, MessageElement element)
        {
            return Task.Run(() => Send(chat, element));
        }
    }
}