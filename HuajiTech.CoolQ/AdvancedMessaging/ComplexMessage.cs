using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示复合消息。
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Naming", "CA1710:标识符应具有正确的后缀", Justification = "<挂起>")]
    public partial class ComplexMessage
    {
        private static readonly Regex CQCodeRegex = new Regex(
            @"\[CQ:(?<Type>[a-zA-Z\-_\.]+)(,(?<Key>[a-zA-Z\-_\.]+)=(?<Value>[^,\]]+))*\]",
            RegexOptions.Compiled);

        private readonly List<MessageElement> _elements;

        /// <summary>
        /// 初始化一个 <see cref="ComplexMessage"/> 类的新实例。
        /// </summary>
        public ComplexMessage()
        {
            _elements = new List<MessageElement>();
        }

        /// <summary>
        /// 以指定消息元素初始化一个 <see cref="ComplexMessage"/> 类的新实例。
        /// </summary>
        /// <param name="element">消息元素。</param>
        public ComplexMessage(MessageElement element)
            : this()
        {
            Add(element);
        }

        /// <summary>
        /// 以指定消息元素集合初始化一个 <see cref="ComplexMessage"/> 类的新实例。
        /// </summary>
        /// <param name="elements">消息元素集合。</param>
        public ComplexMessage(IEnumerable<MessageElement> elements)
            : this()
        {
            Add(elements);
        }

        /// <summary>
        /// 获取或设置指定索引处的消息元素。
        /// </summary>
        /// <param name="index">索引。</param>
        public MessageElement this[int index]
        {
            get => _elements[index];
            set => _elements[index] = value;
        }

        /// <summary>
        /// 将字符串解析为 <see cref="ComplexMessage"/> 对象。
        /// </summary>
        /// <param name="str">要解析的字符串。</param>
        /// <returns>解析后的复合消息。</returns>
        public static ComplexMessage Parse(string str)
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
                    yield return new PlainText(PlainText.Escape(str.Substring(lastMatchEndIndex)));
                }
            }

            return new ComplexMessage(GetMessageElements());
        }

        public static ComplexMessage FromMessageElement(MessageElement element)
        {
            return new ComplexMessage(element);
        }

        /// <summary>
        /// 获取当前 <see cref="ComplexMessage"/> 对象中的所有 <see cref="PlainText"/> 对象使用指定分隔符拼接而成的字符串。
        /// </summary>
        /// <param name="separator">分隔符。</param>
        public string GetPlainText(string separator = "")
        {
            return string.Join(separator, this.OfType<PlainText>());
        }

        /// <summary>
        /// 将指定消息元素添加到末尾。
        /// </summary>
        /// <param name="item">消息元素。</param>
        /// <returns>当前复合消息。</returns>
        public ComplexMessage Add(MessageElement item)
        {
            _elements.Add(item);
            return this;
        }

        /// <summary>
        /// 将指定消息元素集合添加到末尾。
        /// </summary>
        /// <param name="collection">消息元素集合。</param>
        /// <returns>当前复合消息。</returns>
        public ComplexMessage Add(IEnumerable<MessageElement> collection)
        {
            _elements.AddRange(collection);
            return this;
        }

        /// <summary>
        /// 将指定消息元素插入到指定索引处。
        /// </summary>
        /// <param name="index">索引。</param>
        /// <param name="item">消息元素。</param>
        /// <returns>当前复合消息。</returns>
        public ComplexMessage Insert(int index, MessageElement item)
        {
            _elements.Insert(index, item);
            return this;
        }

        /// <summary>
        /// 移除指定消息元素的第一个匹配项。
        /// </summary>
        /// <param name="item">消息元素。</param>
        /// <returns>当前复合消息。</returns>
        public ComplexMessage Remove(MessageElement item)
        {
            _elements.Remove(item);
            return this;
        }

        /// <summary>
        /// 尝试移除指定消息元素的第一个匹配项。
        /// </summary>
        /// <param name="item">消息元素。</param>
        /// <returns>是否成功移除 <paramref name="item"/>。</returns>
        public bool TryRemove(MessageElement item)
        {
            return _elements.Remove(item);
        }

        /// <summary>
        /// 移除指定消息元素的所有匹配项。
        /// </summary>
        /// <param name="item">消息元素。</param>
        /// <returns>当前复合消息。</returns>
        public ComplexMessage RemoveAll(MessageElement item)
        {
            _elements.RemoveAll(element => element == item);
            return this;
        }

        /// <summary>
        /// 移除指定索引处的消息元素。
        /// </summary>
        /// <param name="index">索引。</param>
        /// <returns>当前复合消息。</returns>
        public ComplexMessage RemoveAt(int index)
        {
            _elements.RemoveAt(index);
            return this;
        }

        /// <summary>
        /// 移除一定范围的消息元素。
        /// </summary>
        /// <param name="index">索引。</param>
        /// <param name="count">数量。</param>
        /// <returns>当前复合消息。</returns>
        public ComplexMessage RemoveRange(int index, int count)
        {
            _elements.RemoveRange(index, count);
            return this;
        }

        public override string ToString()
        {
            return string.Join(string.Empty, _elements);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Usage", "CA2225:运算符重载具有命名的备用项", Justification = "<挂起>")]
        public static ComplexMessage operator -(ComplexMessage left, MessageElement right)
        {
            if (left is null || right is null)
            {
                return null;
            }

            return left.Remove(right);
        }

        public static ComplexMessage operator +(ComplexMessage left, MessageElement right)
        {
            if (left is null || right is null)
            {
                return null;
            }

            return left.Add(right);
        }

        public static ComplexMessage operator +(MessageElement left, ComplexMessage right)
        {
            if (left is null || right is null)
            {
                return null;
            }

            return right.Insert(0, left);
        }

        public static ComplexMessage operator +(ComplexMessage left, ComplexMessage right)
        {
            if (left is null || right is null)
            {
                return null;
            }

            return left.Add(right);
        }

        public static explicit operator ComplexMessage(MessageElement element)
        {
            return FromMessageElement(element);
        }
    }

    /// <summary>
    /// <see cref="IList{MessageElement}"/> 的实现。
    /// </summary>
    public partial class ComplexMessage : IList<MessageElement>
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