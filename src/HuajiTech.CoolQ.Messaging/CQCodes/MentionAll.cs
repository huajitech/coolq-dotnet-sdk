using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示提及 (@) 全体成员的 <see cref="CQCode"/>。
    /// </summary>
    public class MentionAll : CQCode
    {
        public MentionAll()
            : base("at")
        {
            this["qq"] = "all";
        }

        public MentionAll(IDictionary<string, string> parameters)
            : base("at", parameters)
        {
        }
    }
}