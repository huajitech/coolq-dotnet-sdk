using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示复合消息。
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Naming", "CA1710:标识符应具有正确的后缀", Justification = "<挂起>")]
    public partial class ComplexMessage : IEquatable<ComplexMessage>
    {
        private static readonly Regex CQCodeRegex = new Regex(
            @"\[CQ:(?<Type>[a-zA-Z\-_\.]+)(,(?<Key>[a-zA-Z\-_\.]+)=(?<Value>[^,\]]+))*\]",
            RegexOptions.Compiled);

        private readonly List<MessageElement> _elements;

        /// <summary>
        /// 初始化一个空的 <see cref="ComplexMessage"/> 类的新实例。
        /// </summary>
        public ComplexMessage()
        {
            _elements = new List<MessageElement>();
        }

        /// <summary>
        /// 以指定的 <see cref="MessageElement"/> 对象初始化一个 <see cref="ComplexMessage"/> 类的新实例。
        /// </summary>
        /// <param name="element">消息元素。</param>
        public ComplexMessage(MessageElement element)
            : this()
        {
            Add(element);
        }

        /// <summary>
        /// 以指定的 <see cref="MessageElement"/> 集合初始化一个 <see cref="ComplexMessage"/> 类的新实例。
        /// </summary>
        /// <param name="elements"><see cref="MessageElement"/> 集合。</param>
        /// <exception cref="ArgumentNullException"><paramref name="elements"/> 为 <c>null</c>。</exception>
        public ComplexMessage(IEnumerable<MessageElement> elements)
        {
            _elements = new List<MessageElement>(elements);
        }

        /// <summary>
        /// 获取或设置指定索引处的 <see cref="MessageElement"/> 对象。
        /// </summary>
        /// <param name="index">要获取或设置的<see cref="MessageElement"/> 对象从 0 开始的索引。</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> 小于 0。 或 <paramref name="index"/> 等于或大于 <see cref="Count"/>。</exception>
        public MessageElement this[int index]
        {
            get => _elements[index];
            set => _elements[index] = value;
        }

        /// <summary>
        /// 将字符串解析为 <see cref="ComplexMessage"/> 对象。
        /// </summary>
        /// <param name="str">要解析的 <see cref="ComplexMessage"/> 对象的字符串表示形式。</param>
        /// <param name="useEmojiCQCode">如果要在返回的 <see cref="ComplexMessage"/> 对象中包含 <see cref="Emoji"/> 对象，则为 <c>true</c>；否则为 <c>false</c>。</param>
        /// <returns>与字符串等效的 <see cref="ComplexMessage"/> 对象。</returns>
        public static ComplexMessage Parse(string str, bool useEmojiCQCode = false)
        {
            IEnumerable<MessageElement> GetMessageElements()
            {
                var matches = CQCodeRegex.Matches(str);
                var lastMatchEndIndex = 0;

                foreach (Match match in matches)
                {
                    var length = match.Index - lastMatchEndIndex;
                    if (length != 0)
                    {
                        var text = str.Substring(lastMatchEndIndex, length);
                        yield return new PlainText(PlainText.Unescape(text));
                    }

                    lastMatchEndIndex = match.Index + match.Length;

                    var groups = match.Groups;
                    var type = groups["Type"].Value;

                    IEnumerable<KeyValuePair<string, string>> GetParameters()
                    {
                        var keyCaptures = groups["Key"].Captures;
                        var valueCaptures = groups["Value"].Captures;

                        for (var i = 0; i < keyCaptures.Count; i++)
                        {
                            yield return new KeyValuePair<string, string>(
                                keyCaptures[i].Value,
                                valueCaptures[i].Value);
                        }
                    }

                    yield return CQCodeFactory.Create(
                        type, GetParameters().Distinct().ToDictionary(
                            item => item.Key,
                            item => CQCode.Unescape(item.Value)));
                }

                if (lastMatchEndIndex != str.Length)
                {
                    yield return new PlainText(PlainText.Unescape(str.Substring(lastMatchEndIndex)));
                }
            }

            if (!useEmojiCQCode)
            {
                var convertedElements = GetMessageElements()
                    .Select(element => element is Emoji emoji ? emoji.ConvertToString() : element);
                return new ComplexMessage(GetPlainTextCombinedMessageElements(convertedElements));
            }

            return new ComplexMessage(GetMessageElements());
        }

        public static ComplexMessage FromString(string str)
        {
            return new ComplexMessage(str);
        }

        /// <summary>
        /// 使用指定的分隔符串联 <see cref="ComplexMessage"/> 集合中的所有成员。
        /// </summary>
        /// <param name="separator">要用作分隔符的 <see cref="MessageElement"/> 对象。</param>
        /// <param name="messages">一个包含要串联的 <see cref="ComplexMessage"/> 对象的集合。</param>
        /// <returns>
        /// 一个包含 <paramref name="messages"/> 中所有成员的 <see cref="ComplexMessage"/> 对象，这些成员以 <paramref name="separator"/> 分隔。
        /// 如果 <paramref name="messages"/> 没有成员，则该方法返回一个空的 <see cref="ComplexMessage"/> 对象。
        /// </returns>
        public static ComplexMessage Join(MessageElement separator, IEnumerable<ComplexMessage> messages)
        {
            if (!messages.Any())
            {
                return new ComplexMessage();
            }

            if (messages.Count() == 1)
            {
                return messages.First();
            }

            IEnumerable<MessageElement> GetMessageElements()
            {
                foreach (var element in messages.First())
                {
                    yield return element;
                }

                foreach (var message in messages.Skip(1))
                {
                    yield return separator;

                    foreach (var element in message)
                    {
                        yield return element;
                    }
                }
            }

            return new ComplexMessage(GetMessageElements());
        }

        private static IEnumerable<MessageElement> GetPlainTextCombinedMessageElements(IEnumerable<MessageElement> elements)
        {
            var buffer = new StringBuilder();

            foreach (var element in elements)
            {
                if (element is PlainText text)
                {
                    buffer.Append(text);
                    continue;
                }

                if (buffer.Length > 0)
                {
                    yield return buffer.ToString();
                    buffer.Clear();
                }

                yield return element;
            }

            if (buffer.Length > 0)
            {
                yield return buffer.ToString();
            }
        }

        /// <summary>
        /// 将当前 <see cref="ComplexMessage"/> 对象中的所有连续的 <see cref="PlainText"/> 对象拼接为单个 <see cref="PlainText"/> 对象。
        /// </summary>
        /// <returns>一个新的 <see cref="ComplexMessage"/> 对象，包含已被拼接的 <see cref="PlainText"/> 对象。</returns>
        public ComplexMessage CombinePlainText()
        {
            return new ComplexMessage(GetPlainTextCombinedMessageElements(this));
        }

        public string GetPlainText()
        {
            return string.Join(string.Empty, this.OfType<PlainText>());
        }

        /// <summary>
        /// 将 <see cref="MessageElement"/> 对象添加到 <see cref="ComplexMessage"/> 的结尾处。
        /// </summary>
        /// <param name="item">要添加到 <see cref="ComplexMessage"/> 末尾的对象。</param>
        /// <returns>当前 <see cref="ComplexMessage"/> 对象。</returns>
        public ComplexMessage Add(MessageElement item)
        {
            _elements.Add(item);
            return this;
        }

        /// <summary>
        /// 将指定 <see cref="MessageElement"/> 集合添加到 <see cref="ComplexMessage"/> 的末尾。
        /// </summary>
        /// <param name="collection">
        /// 应将其元素添加到 <see cref="ComplexMessage"/> 的末尾的集合。
        /// 集合自身不能为 <c>null</c>。
        /// </param>
        /// <returns>当前 <see cref="ComplexMessage"/> 对象。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> 为 <c>null</c>。</exception>
        public ComplexMessage Add(IEnumerable<MessageElement> collection)
        {
            _elements.AddRange(collection);
            return this;
        }

        /// <summary>
        /// 将 <see cref="MessageElement"/> 插入到 <see cref="ComplexMessage"/> 的指定索引处。
        /// </summary>
        /// <param name="index">应插入 <paramref name="item"/> 的从零开始的索引。</param>
        /// <param name="item">要插入的 <see cref="MessageElement"/> 对象。</param>
        /// <returns>当前 <see cref="ComplexMessage"/> 对象。</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> 小于 0。
        /// 或 <paramref name="index"/> 大于 <see cref="Count"/>。
        /// </exception>
        public ComplexMessage Insert(int index, MessageElement item)
        {
            _elements.Insert(index, item);
            return this;
        }

        /// <summary>
        /// 从 <see cref="ComplexMessage"/> 中移除特定 <see cref="MessageElement"/> 的第一个匹配项。
        /// </summary>
        /// <param name="item">要从 <see cref="ComplexMessage"/> 中删除的对象。</param>
        /// <returns>当前 <see cref="ComplexMessage"/> 对象。</returns>
        public ComplexMessage Remove(MessageElement item)
        {
            _elements.Remove(item);
            return this;
        }

        /// <summary>
        /// 尝试从 <see cref="ComplexMessage"/> 中移除特定 <see cref="MessageElement"/> 的第一个匹配项。
        /// </summary>
        /// <param name="item">要从 <see cref="ComplexMessage"/> 中删除的对象。</param>
        /// <returns>
        /// 如果成功移除了 <paramref name="item"/>，则为 <c>true</c>；否则为 <c>false</c>。
        /// 如果在 <see cref="ComplexMessage"/> 中没有找到 <paramref name="item"/>，则此方法也会返回 <c>false</c>。
        /// </returns>
        public bool TryRemove(MessageElement item)
        {
            return _elements.Remove(item);
        }

        /// <summary>
        /// 从 <see cref="ComplexMessage"/> 中移除特定 <see cref="MessageElement"/> 的所有匹配项。
        /// </summary>
        /// <param name="item">要从 <see cref="ComplexMessage"/> 中删除的对象。</param>
        /// <returns>当前 <see cref="ComplexMessage"/> 对象。</returns>
        public ComplexMessage RemoveAll(MessageElement item)
        {
            _elements.RemoveAll(element => element == item);
            return this;
        }

        /// <summary>
        /// 移除 <see cref="ComplexMessage"/> 的指定索引处的 <see cref="MessageElement"/>。
        /// </summary>
        /// <param name="index">要移除的元素的从零开始的索引。</param>
        /// <returns>当前 <see cref="ComplexMessage"/> 对象。</returns>
        public ComplexMessage RemoveAt(int index)
        {
            _elements.RemoveAt(index);
            return this;
        }

        /// <summary>
        /// 从 <see cref="ComplexMessage"/> 中移除一定范围的 <see cref="MessageElement"/>。
        /// </summary>
        /// <param name="index">要移除的元素范围的从零开始的起始索引。</param>
        /// <param name="count">要移除的元素数。</param>
        /// <returns>当前 <see cref="ComplexMessage"/> 对象。</returns>
        public ComplexMessage RemoveRange(int index, int count)
        {
            _elements.RemoveRange(index, count);
            return this;
        }

        /// <summary>
        /// 将当前 <see cref="ComplexMessage"/> 对象的值转换为它的等效字符串表示形式。
        /// </summary>
        /// <returns>当前 <see cref="ComplexMessage"/> 对象的值的字符串表示形式。</returns>
        public override string ToString()
        {
            return string.Join(string.Empty, _elements);
        }

        public bool Equals(ComplexMessage other)
        {
            return base.Equals(other) || other?.ToString() == ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ComplexMessage);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static bool operator ==(ComplexMessage left, ComplexMessage right)
        {
            return left?.Equals(right) ?? right is null;
        }

        public static bool operator !=(ComplexMessage left, ComplexMessage right)
        {
            return !(left == right);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Usage", "CA2225:运算符重载具有命名的备用项", Justification = "<挂起>")]
        public static ComplexMessage operator -(ComplexMessage left, MessageElement right)
        {
            return left?.Remove(right);
        }

        public static ComplexMessage operator +(ComplexMessage left, MessageElement right)
        {
            return left?.Add(right);
        }

        public static ComplexMessage operator +(MessageElement left, ComplexMessage right)
        {
            return left?.ToComplexMessage()?.Add(right);
        }

        public static ComplexMessage operator +(ComplexMessage left, ComplexMessage right)
        {
            return left?.Add(right);
        }

        public static implicit operator ComplexMessage(string str)
        {
            return FromString(str);
        }
    }

    /// <summary>
    /// <see cref="IList{T}"/> 和 <see cref="IReadOnlyList{T}"/> 的实现。
    /// </summary>
    public partial class ComplexMessage : IList<MessageElement>, IReadOnlyList<MessageElement>
    {
        public int Count => _elements.Count;

        public bool IsReadOnly => ((IList<MessageElement>)_elements).IsReadOnly;

        void ICollection<MessageElement>.Add(MessageElement item)
        {
            _elements.Add(item);
        }

        public void Clear()
        {
            _elements.Clear();
        }

        public bool Contains(MessageElement item)
        {
            return _elements.Contains(item);
        }

        public void CopyTo(MessageElement[] array, int arrayIndex)
        {
            _elements.CopyTo(array, arrayIndex);
        }

        public IEnumerator<MessageElement> GetEnumerator()
        {
            return ((IList<MessageElement>)_elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<MessageElement>)_elements).GetEnumerator();
        }

        public int IndexOf(MessageElement item)
        {
            return _elements.IndexOf(item);
        }

        void IList<MessageElement>.Insert(int index, MessageElement item)
        {
            _elements.Insert(index, item);
        }

        bool ICollection<MessageElement>.Remove(MessageElement item)
        {
            return _elements.Remove(item);
        }

        void IList<MessageElement>.RemoveAt(int index)
        {
            _elements.RemoveAt(index);
        }
    }
}