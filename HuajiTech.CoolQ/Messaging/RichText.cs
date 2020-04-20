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
    }
}