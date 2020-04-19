using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示小表情的 <see cref="CQCode"/>。
    /// </summary>
    public class SmallEmoticon : CQCode
    {
        public SmallEmoticon()
            : base("sface")
        {
        }

        public SmallEmoticon(IDictionary<string, string> arguments)
            : base("sface", arguments)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="SmallEmoticon"/> 对象的 ID。
        /// </summary>
        public int Id
        {
            get => GetArgumentAsInt32("id");
            set => SetArgument("id", value);
        }
    }
}