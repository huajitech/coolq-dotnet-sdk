using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示匿名消息的 <see cref="CQCode"/>。
    /// </summary>
    public class Anonymous : CQCode
    {
        public Anonymous()
            : base("anonymous")
        {
        }

        public Anonymous(IDictionary<string, string> parameters)
            : base("anonymous", parameters)
        {
        }

        /// <summary>
        /// 获取或设置一个值，指示是否在不能发送匿名消息时回退到普通消息。
        /// </summary>
        public bool Fallback
        {
            get => GetParameterAsBoolean("ignore");
            set => SetParameter("ignore", value);
        }
    }
}