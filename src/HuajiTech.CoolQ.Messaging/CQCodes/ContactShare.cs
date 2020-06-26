using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示联系人分享的 <see cref="CQCode"/>。
    /// </summary>
    public class ContactShare : CQCode
    {
        public ContactShare()
            : base("contact")
        {
        }

        public ContactShare(IDictionary<string, string> parameters)
            : base("contact", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="ContactShare"/> 实例的内容类型。
        /// </summary>
        public string? ContentType
        {
            get => this["type"];
            set => this["type"] = value;
        }

        /// <summary>
        /// 获取或设置当前 <see cref="ContactShare"/> 实例的内容号码。
        /// </summary>
        public long ContentNumber
        {
            get => GetParameterAsInt64("id");
            set => SetParameter("id", value);
        }
    }
}