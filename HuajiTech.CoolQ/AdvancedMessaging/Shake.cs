using System.Collections.Generic;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示抖动。
    /// </summary>
    public class Shake : CQCode
    {
        public Shake()
        {
        }

        public Shake(IDictionary<string, string> arguments)
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

        public override string Type => "shake";
    }
}