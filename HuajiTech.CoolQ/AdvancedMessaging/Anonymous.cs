using System.Collections.Generic;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示匿名消息标识。
    /// </summary>
    public class Anonymous : CQCode
    {
        public Anonymous()
        {
        }

        public Anonymous(IDictionary<string, string> parameters)
            : base(parameters)
        {
        }

        /// <summary>
        /// 获取或设置一个值，指示是否在不能发送匿名消息时回退到普通消息。
        /// </summary>
        public bool DoesFallback
        {
            get => GetParameterAsBoolean("ignore");
            set => SetParameter("ignore", value);
        }

        public override string Type => "anonymous";
    }
}