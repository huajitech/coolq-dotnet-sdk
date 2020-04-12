using System.Collections.Generic;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示猜拳。
    /// </summary>
    public class RockPaperScissors : CQCode
    {
        public RockPaperScissors()
        {
        }

        public RockPaperScissors(IDictionary<string, string> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// 获取猜拳的结果。
        /// </summary>
        public RockPaperScissorsResult Result =>
            (RockPaperScissorsResult)GetArgumentAsInt32("type");

        public override string Type => "rps";
    }
}