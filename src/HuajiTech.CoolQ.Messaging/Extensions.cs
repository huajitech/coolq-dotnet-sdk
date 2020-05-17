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
    }
}