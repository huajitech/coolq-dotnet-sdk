using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示提及 (@) 的 <see cref="CQCode"/>。
    /// </summary>
    public class Mention : CQCode
    {
        public Mention()
            : base("at")
        {
        }

        public Mention(IDictionary<string, string> parameters)
            : base("at", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Mention"/> 实例的目标号码。
        /// </summary>
        public long TargetNumber
        {
            get => GetParameterAsInt64("qq");
            set => SetParameter("qq", value);
        }
    }

    [Obsolete("请改为使用 Mention。")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "<挂起>")]
    public class At : Mention
    {
        public At()
        {
        }

        public At(IDictionary<string, string> parameters)
            : base(parameters)
        {
        }
    }
}