using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示猜拳的 <see cref="CQCode"/>。
    /// </summary>
    public class RockPaperScissors : CQCode
    {
        public RockPaperScissors()
            : base("rps")
        {
        }

        public RockPaperScissors(IDictionary<string, string> arguments)
            : base("rps", arguments)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="RockPaperScissors"/> 对象的类别。
        /// </summary>
        public RockPaperScissorsKind Kind
        {
            get => (RockPaperScissorsKind)GetArgumentAsInt32("type");
            set => SetArgument("type", (int)value);
        }
    }
}