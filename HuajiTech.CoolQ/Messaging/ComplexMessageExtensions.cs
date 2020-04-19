using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 定义 <see cref="ComplexMessage"/> 类的扩展方法。
    /// </summary>
    public static class ComplexMessageExtensions
    {
        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 1 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 2 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 3 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
            element2 = message.ElementAtOrDefault(2);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 4 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
            element2 = message.ElementAtOrDefault(2);
            element3 = message.ElementAtOrDefault(3);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 5 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
            element2 = message.ElementAtOrDefault(2);
            element3 = message.ElementAtOrDefault(3);
            element4 = message.ElementAtOrDefault(4);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 6 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
            element2 = message.ElementAtOrDefault(2);
            element3 = message.ElementAtOrDefault(3);
            element4 = message.ElementAtOrDefault(4);
            element5 = message.ElementAtOrDefault(5);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 7 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
            element2 = message.ElementAtOrDefault(2);
            element3 = message.ElementAtOrDefault(3);
            element4 = message.ElementAtOrDefault(4);
            element5 = message.ElementAtOrDefault(5);
            element6 = message.ElementAtOrDefault(6);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 8 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
            element2 = message.ElementAtOrDefault(2);
            element3 = message.ElementAtOrDefault(3);
            element4 = message.ElementAtOrDefault(4);
            element5 = message.ElementAtOrDefault(5);
            element6 = message.ElementAtOrDefault(6);
            element7 = message.ElementAtOrDefault(7);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 9 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
            element2 = message.ElementAtOrDefault(2);
            element3 = message.ElementAtOrDefault(3);
            element4 = message.ElementAtOrDefault(4);
            element5 = message.ElementAtOrDefault(5);
            element6 = message.ElementAtOrDefault(6);
            element7 = message.ElementAtOrDefault(7);
            element8 = message.ElementAtOrDefault(8);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 10 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out MessageElement element9)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
            element2 = message.ElementAtOrDefault(2);
            element3 = message.ElementAtOrDefault(3);
            element4 = message.ElementAtOrDefault(4);
            element5 = message.ElementAtOrDefault(5);
            element6 = message.ElementAtOrDefault(6);
            element7 = message.ElementAtOrDefault(7);
            element8 = message.ElementAtOrDefault(8);
            element9 = message.ElementAtOrDefault(9);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 11 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out MessageElement element9,
            out MessageElement element10)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
            element2 = message.ElementAtOrDefault(2);
            element3 = message.ElementAtOrDefault(3);
            element4 = message.ElementAtOrDefault(4);
            element5 = message.ElementAtOrDefault(5);
            element6 = message.ElementAtOrDefault(6);
            element7 = message.ElementAtOrDefault(7);
            element8 = message.ElementAtOrDefault(8);
            element9 = message.ElementAtOrDefault(9);
            element10 = message.ElementAtOrDefault(10);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 12 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out MessageElement element9,
            out MessageElement element10,
            out MessageElement element11)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
            element2 = message.ElementAtOrDefault(2);
            element3 = message.ElementAtOrDefault(3);
            element4 = message.ElementAtOrDefault(4);
            element5 = message.ElementAtOrDefault(5);
            element6 = message.ElementAtOrDefault(6);
            element7 = message.ElementAtOrDefault(7);
            element8 = message.ElementAtOrDefault(8);
            element9 = message.ElementAtOrDefault(9);
            element10 = message.ElementAtOrDefault(10);
            element11 = message.ElementAtOrDefault(11);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 13 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out MessageElement element9,
            out MessageElement element10,
            out MessageElement element11,
            out MessageElement element12)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
            element2 = message.ElementAtOrDefault(2);
            element3 = message.ElementAtOrDefault(3);
            element4 = message.ElementAtOrDefault(4);
            element5 = message.ElementAtOrDefault(5);
            element6 = message.ElementAtOrDefault(6);
            element7 = message.ElementAtOrDefault(7);
            element8 = message.ElementAtOrDefault(8);
            element9 = message.ElementAtOrDefault(9);
            element10 = message.ElementAtOrDefault(10);
            element11 = message.ElementAtOrDefault(11);
            element12 = message.ElementAtOrDefault(12);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 14 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out MessageElement element9,
            out MessageElement element10,
            out MessageElement element11,
            out MessageElement element12,
            out MessageElement element13)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
            element2 = message.ElementAtOrDefault(2);
            element3 = message.ElementAtOrDefault(3);
            element4 = message.ElementAtOrDefault(4);
            element5 = message.ElementAtOrDefault(5);
            element6 = message.ElementAtOrDefault(6);
            element7 = message.ElementAtOrDefault(7);
            element8 = message.ElementAtOrDefault(8);
            element9 = message.ElementAtOrDefault(9);
            element10 = message.ElementAtOrDefault(10);
            element11 = message.ElementAtOrDefault(11);
            element12 = message.ElementAtOrDefault(12);
            element13 = message.ElementAtOrDefault(13);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 15 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out MessageElement element9,
            out MessageElement element10,
            out MessageElement element11,
            out MessageElement element12,
            out MessageElement element13,
            out MessageElement element14)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
            element2 = message.ElementAtOrDefault(2);
            element3 = message.ElementAtOrDefault(3);
            element4 = message.ElementAtOrDefault(4);
            element5 = message.ElementAtOrDefault(5);
            element6 = message.ElementAtOrDefault(6);
            element7 = message.ElementAtOrDefault(7);
            element8 = message.ElementAtOrDefault(8);
            element9 = message.ElementAtOrDefault(9);
            element10 = message.ElementAtOrDefault(10);
            element11 = message.ElementAtOrDefault(11);
            element12 = message.ElementAtOrDefault(12);
            element13 = message.ElementAtOrDefault(13);
            element14 = message.ElementAtOrDefault(14);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 16 个不同的 <see cref="MessageElement"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out MessageElement element9,
            out MessageElement element10,
            out MessageElement element11,
            out MessageElement element12,
            out MessageElement element13,
            out MessageElement element14,
            out MessageElement element15)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            element0 = message.ElementAtOrDefault(0);
            element1 = message.ElementAtOrDefault(1);
            element2 = message.ElementAtOrDefault(2);
            element3 = message.ElementAtOrDefault(3);
            element4 = message.ElementAtOrDefault(4);
            element5 = message.ElementAtOrDefault(5);
            element6 = message.ElementAtOrDefault(6);
            element7 = message.ElementAtOrDefault(7);
            element8 = message.ElementAtOrDefault(8);
            element9 = message.ElementAtOrDefault(9);
            element10 = message.ElementAtOrDefault(10);
            element11 = message.ElementAtOrDefault(11);
            element12 = message.ElementAtOrDefault(12);
            element13 = message.ElementAtOrDefault(13);
            element14 = message.ElementAtOrDefault(14);
            element15 = message.ElementAtOrDefault(15);
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 1 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0);

            rest = new ComplexMessage(message.Skip(1));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 2 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1);

            rest = new ComplexMessage(message.Skip(2));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 3 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1,
                out element2);

            rest = new ComplexMessage(message.Skip(3));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 4 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1,
                out element2,
                out element3);

            rest = new ComplexMessage(message.Skip(4));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 5 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1,
                out element2,
                out element3,
                out element4);

            rest = new ComplexMessage(message.Skip(5));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 6 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1,
                out element2,
                out element3,
                out element4,
                out element5);

            rest = new ComplexMessage(message.Skip(6));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 7 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1,
                out element2,
                out element3,
                out element4,
                out element5,
                out element6);

            rest = new ComplexMessage(message.Skip(7));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 8 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1,
                out element2,
                out element3,
                out element4,
                out element5,
                out element6,
                out element7);

            rest = new ComplexMessage(message.Skip(8));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 9 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1,
                out element2,
                out element3,
                out element4,
                out element5,
                out element6,
                out element7,
                out element8);

            rest = new ComplexMessage(message.Skip(9));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 10 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out MessageElement element9,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1,
                out element2,
                out element3,
                out element4,
                out element5,
                out element6,
                out element7,
                out element8,
                out element9);

            rest = new ComplexMessage(message.Skip(10));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 11 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out MessageElement element9,
            out MessageElement element10,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1,
                out element2,
                out element3,
                out element4,
                out element5,
                out element6,
                out element7,
                out element8,
                out element9,
                out element10);

            rest = new ComplexMessage(message.Skip(11));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 12 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out MessageElement element9,
            out MessageElement element10,
            out MessageElement element11,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1,
                out element2,
                out element3,
                out element4,
                out element5,
                out element6,
                out element7,
                out element8,
                out element9,
                out element10,
                out element11);

            rest = new ComplexMessage(message.Skip(12));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 13 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out MessageElement element9,
            out MessageElement element10,
            out MessageElement element11,
            out MessageElement element12,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1,
                out element2,
                out element3,
                out element4,
                out element5,
                out element6,
                out element7,
                out element8,
                out element9,
                out element10,
                out element11,
                out element12);

            rest = new ComplexMessage(message.Skip(13));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 14 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out MessageElement element9,
            out MessageElement element10,
            out MessageElement element11,
            out MessageElement element12,
            out MessageElement element13,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1,
                out element2,
                out element3,
                out element4,
                out element5,
                out element6,
                out element7,
                out element8,
                out element9,
                out element10,
                out element11,
                out element12,
                out element13);

            rest = new ComplexMessage(message.Skip(14));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 15 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out MessageElement element9,
            out MessageElement element10,
            out MessageElement element11,
            out MessageElement element12,
            out MessageElement element13,
            out MessageElement element14,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1,
                out element2,
                out element3,
                out element4,
                out element5,
                out element6,
                out element7,
                out element8,
                out element9,
                out element10,
                out element11,
                out element12,
                out element13,
                out element14);

            rest = new ComplexMessage(message.Skip(15));
        }

        /// <summary>
        /// 将 <see cref="ComplexMessage"/> 对象解构为 16 个不同的 <see cref="MessageElement"/> 对象和剩余的 <see cref="ComplexMessage"/> 对象。
        /// 如果要求的 <see cref="MessageElement"/> 对象数量大于 <paramref name="message"/> 的元素数量，则超出部分的值为 <c>null</c>。
        /// 如果没有剩余 <see cref="MessageElement"/> 对象，则 <paramref name="rest"/> 为空的 <see cref="ComplexMessage"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> 为 <c>null</c>。</exception>
        public static void Deconstruct(
            this ComplexMessage message,
            out MessageElement element0,
            out MessageElement element1,
            out MessageElement element2,
            out MessageElement element3,
            out MessageElement element4,
            out MessageElement element5,
            out MessageElement element6,
            out MessageElement element7,
            out MessageElement element8,
            out MessageElement element9,
            out MessageElement element10,
            out MessageElement element11,
            out MessageElement element12,
            out MessageElement element13,
            out MessageElement element14,
            out MessageElement element15,
            out ComplexMessage rest)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Deconstruct(
                out element0,
                out element1,
                out element2,
                out element3,
                out element4,
                out element5,
                out element6,
                out element7,
                out element8,
                out element9,
                out element10,
                out element11,
                out element12,
                out element13,
                out element14,
                out element15);

            rest = new ComplexMessage(message.Skip(16));
        }
    }
}