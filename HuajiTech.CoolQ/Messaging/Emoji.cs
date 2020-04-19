using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示 Emoji 的 <see cref="CQCode"/>。
    /// </summary>
    public class Emoji : CQCode
    {
        public Emoji()
            : base("emoji")
        {
        }

        public Emoji(IDictionary<string, string> arguments)
            : base("emoji", arguments)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Emoji"/> 对象的 ID。
        /// </summary>
        public int Id
        {
            get => GetArgumentAsInt32("id");
            set => SetArgument("id", value);
        }
    }
}