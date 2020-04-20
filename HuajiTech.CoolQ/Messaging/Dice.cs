using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示骰子的 <see cref="CQCode"/>。
    /// </summary>
    public class Dice : CQCode
    {
        public Dice()
            : base("dice")
        {
        }

        public Dice(IDictionary<string, string> arguments)
            : base("dice", arguments)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Dice"/> 对象的点数。
        /// </summary>
        public int Point
        {
            get => GetArgumentAsInt32("type");
            set => SetArgument("type", value);
        }
    }
}