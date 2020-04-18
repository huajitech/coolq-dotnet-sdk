using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示富文本的 <see cref="CQCode"/>。
    /// </summary>
    public class RichText : CQCode
    {
        public RichText()
            : base("rich")
        {
        }

        public RichText(IDictionary<string, string> arguments)
            : base("rich", arguments)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="RichText"/> 对象的内容。
        /// </summary>
        public string Content
        {
            get => this["content"];
            set => this["content"] = value;
        }

        /// <summary>
        /// 获取或设置当前 <see cref="RichText"/> 对象的标题。
        /// </summary>
        public string Title
        {
            get => this["title"];
            set => this["title"] = value;
        }
    }
}