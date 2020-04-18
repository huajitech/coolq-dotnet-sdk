namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示纯文本。
    /// </summary>
    public sealed class PlainText : MessageElement
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
        /// 获取未经转义的原始内容。
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

        /// <summary>
        /// 获取转义后的内容。
        /// </summary>
        /// <returns>转义后的内容。</returns>
        public override string ToString()
        {
            return Escape(Content);
        }

        /// <summary>
        /// 将纯文本的原始内容隐式转换为字符串。
        /// </summary>
        /// <param name="text">要转换的纯文本。</param>
        public static implicit operator string(PlainText text)
        {
            return text?.Content;
        }
    }
}