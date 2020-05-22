using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示 At (@) 的 <see cref="CQCode"/>。
    /// </summary>
    public class At : CQCode
    {
        public At()
            : base("at")
        {
        }

        public At(IDictionary<string, string> parameters)
            : base("at", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="At"/> 实例的目标号码。
        /// </summary>
        public long TargetNumber
        {
            get => GetParameterAsInt64("qq");
            set => SetParameter("qq", value);
        }
    }
}