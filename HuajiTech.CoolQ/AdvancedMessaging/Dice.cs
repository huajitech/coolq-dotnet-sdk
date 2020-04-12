using System.Collections.Generic;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示骰子。
    /// </summary>
    public class Dice : CQCode
    {
        public Dice()
        {
        }

        public Dice(IDictionary<string, string> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// 获取或设置点数。
        /// </summary>
        public int Point
        {
            get => GetArgumentAsInt32("type");
            set => SetArgument("type", value);
        }

        public override string Type => "dice";
    }
}