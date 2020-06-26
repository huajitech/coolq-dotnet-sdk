using System;
using System.Collections.Generic;
using System.Text;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示红包的 <see cref="CQCode"/>。
    /// </summary>
    public class RedEnvelope : CQCode
    {
        public RedEnvelope()
            : base("hb")
        {
        }

        public RedEnvelope(IDictionary<string, string> parameters)
            : base("hb", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="RedEnvelope"/> 实例的标题。
        /// </summary>
        public string? Title
        {
            get => this["title"];
            set => this["title"] = value;
        }
    }
}