using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示 At (@) 全体成员的 <see cref="CQCode"/>。
    /// </summary>
    public class AtAll : CQCode
    {
        public AtAll()
            : base("at")
        {
            this["qq"] = "all";
        }

        public AtAll(IDictionary<string, string> arguments)
            : base("at", arguments)
        {
        }
    }
}