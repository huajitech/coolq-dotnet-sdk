namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示纯文本。
    /// </summary>
    public class PlainText : MessageElement
    {
        /// <summary>
        /// 以指定内容初始化一个 <see cref="PlainText"/> 类的新实例。
        /// </summary>
        /// <param name="content">内容。</param>
        public PlainText(string content)
        {
            Content = content ?? throw new System.ArgumentNullException(nameof(content));
        }

        /// <summary>
        /// 获取内容。
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// 对字符串进行转义。
        /// </summary>
        /// <param name="str">要转义的字符串。</param>
        /// <returns>转义后的字符串。</returns>
        public static string Escape(string str)
        {
            return str?.Replace("&", "&amp;")
                       .Replace("[", "&#91;")
                       .Replace("]", "&#93;");
        }

        public static implicit operator string(PlainText text)
        {
            return text?.Content;
        }

        /// <summary>
        /// 对字符串进行反转义。
        /// </summary>
        /// <param name="str">要反转义的字符串。</param>
        /// <returns>反转义后的字符串。</returns>
        public static string Unescape(string str)
        {
            return str?.Replace("&#91;", "[")
                       .Replace("&#93;", "]")
                       .Replace("&amp;", "&");
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) || obj as PlainText == this;
        }

        public bool Equals(PlainText text)
        {
            return text == this;
        }

        public override int GetHashCode()
        {
            return Content.GetHashCode();
        }

        public override string ToString()
        {
            return Escape(Content);
        }

        public static bool operator ==(PlainText left, PlainText right)
        {
            return left?.Content == right?.Content;
        }

        public static bool operator !=(PlainText left, PlainText right)
        {
            return !(left == right);
        }
    }
}