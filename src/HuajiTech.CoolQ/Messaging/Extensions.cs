using HuajiTech.QQ;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public static At At(this IUser target) =>
            new At
            {
                Target = target ?? throw new ArgumentNullException(nameof(target))
            };

        /// <summary>
        /// 将 <see cref="IContentfulMessage"/> 对象解析为 <see cref="ComplexMessage"/> 对象。
        /// </summary>
        /// <param name="message">一个 <see cref="IContentfulMessage"/>对象，该 <see cref="IContentfulMessage"/> 对象的 <see cref="QQ.IContentfulMessage.Content"/> 属性为要解析的 <see cref="ComplexMessage"/> 对象的字符串表示形式。</param>
        /// <param name="useEmojiCQCode">如果要在返回的 <see cref="ComplexMessage"/> 对象中包含 <see cref="Emoji"/> 对象，则为 <c>true</c>；否则为 <c>false</c>。</param>
        /// <returns>与 <see cref="IContentfulMessage.Content"/> 等效的 <see cref="ComplexMessage"/> 对象。</returns>
        public static ComplexMessage Parse(this IContentfulMessage? message, bool useEmojiCQCode = false) =>
            ComplexMessage.Parse(message?.Content, useEmojiCQCode);

        /// <summary>
        /// 从 <see cref="IEnumerable{T}"/> 创建 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <param name="elements">要用于创建 <see cref="ComplexMessage"/> 的消息元素集合。</param>
        /// <returns>一个 <see cref="ComplexMessage"/>，其中包含输入序列中的元素。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="elements"/> 为 <c>null</c>。</exception>
        public static ComplexMessage ToComplexMessage(this IEnumerable<MessageElement> elements) =>
            new ComplexMessage(elements);

        /// <summary>
        /// 使用指定的分隔符从 <see cref="IEnumerable{T}"/> 创建 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <param name="elements">要用于创建 <see cref="ComplexMessage"/> 的消息元素集合。</param>
        /// <param name="separator">要用作分隔符的 <see cref="MessageElement"/> 对象。</param>
        /// <returns>
        /// 一个包含 <paramref name="elements"/> 中所有成员的 <see cref="ComplexMessage"/> 对象，这些成员以 <paramref name="separator"/> 分隔。
        /// 如果 <paramref name="elements"/> 没有成员，则该方法返回一个空的 <see cref="ComplexMessage"/> 对象。
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="elements"/> 为 <c>null</c>。</exception>
        public static ComplexMessage ToComplexMessage(this IEnumerable<MessageElement> elements, MessageElement separator)
        {
            if (elements is null)
            {
                throw new ArgumentNullException(nameof(elements));
            }

            if (!elements.Any())
            {
                return new ComplexMessage();
            }

            if (elements.Count() is 1)
            {
                return new ComplexMessage(elements.First());
            }

            IEnumerable<MessageElement> GetMessageElements()
            {
                yield return elements.First();

                foreach (var element in elements.Skip(1))
                {
                    yield return separator;
                    yield return element;
                }
            }

            return new ComplexMessage(GetMessageElements());
        }

        /// <summary>
        /// 向指定聊天发送 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <param name="sendee">目标可被发送对象。</param>
        /// <param name="message">要发送的 <see cref="ComplexMessage"/> 对象。</param>
        /// <returns>一个 <see cref="Message"/> 对象，表示已发送的消息。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="sendee"/> 为 <c>null</c>。</exception>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        /// <exception cref="ArgumentException"><paramref name="message"/> 不包含任何元素，或其等效字符串表示形式为 <see cref="string.Empty"/>。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public static IContentfulMessage Send(this ISendee sendee, ComplexMessage message)
        {
            if (sendee is null)
            {
                throw new ArgumentNullException(nameof(sendee));
            }

            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return sendee.Send(message.ToString());
        }

        /// <summary>
        /// 向指定聊天发送 <see cref="MessageElement"/>。
        /// </summary>
        /// <param name="sendee">目标可发送对象。</param>
        /// <param name="element">要发送的 <see cref="MessageElement"/> 对象。</param>
        /// <returns>一个 <see cref="Message"/> 对象，表示已发送的消息。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="sendee"/> 为 <c>null</c>。</exception>
        /// <exception cref="ArgumentNullException"><paramref name="element"/> 为 <c>null</c>。</exception>
        /// <exception cref="ArgumentException"><paramref name="element"/> 的等效字符串表示形式为 <see cref="string.Empty"/>。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示发送失败的值。</exception>
        public static IContentfulMessage Send(this ISendee sendee, MessageElement element)
        {
            if (sendee is null)
            {
                throw new ArgumentNullException(nameof(sendee));
            }

            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return sendee.Send(element.ToString());
        }
    }
}