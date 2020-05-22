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

        public RockPaperScissors(IDictionary<string, string> parameters)
            : base("rps", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="RockPaperScissors"/> 实例的类别。
        /// </summary>
        public RockPaperScissorsKind Kind
        {
            get => (RockPaperScissorsKind)GetParameterAsInt32("type");
            set => SetParameter("type", (int)value);
        }
    }
}