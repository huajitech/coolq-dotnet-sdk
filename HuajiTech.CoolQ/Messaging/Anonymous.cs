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

        public Anonymous(IDictionary<string, string> arguments)
            : base("anonymous", arguments)
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
    }
}