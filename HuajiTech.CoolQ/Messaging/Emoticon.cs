using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示表情的 <see cref="CQCode"/>。
    /// </summary>
    public class Emoticon : CQCode
    {
        public Emoticon()
            : base("image")
        {
        }

        public Emoticon(IDictionary<string, string> arguments)
            : base("image", arguments)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Emoticon"/> 对象的 ID。
        /// </summary>
        public int Id
        {
            get => GetArgumentAsInt32("id");
            set => SetArgument("id", value);
        }
    }
}