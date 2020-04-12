using System.Collections.Generic;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示 Emoji。
    /// </summary>
    public class Emoji : CQCode
    {
        public Emoji()
        {
        }

        public Emoji(IDictionary<string, string> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// 获取或设置 ID。
        /// </summary>
        public int Id
        {
            get => GetArgumentAsInt32("id");
            set => SetArgument("id", value);
        }

        public override string Type => "emoji";
    }
}