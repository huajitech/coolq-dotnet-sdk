using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示名片分享的 <see cref="CQCode"/>。
    /// </summary>
    public class ChatShare : CQCode
    {
        public ChatShare()
            : base("contact")
        {
        }

        public ChatShare(IDictionary<string, string> arguments)
            : base("contact", arguments)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="ChatShare"/> 对象的内容。
        /// </summary>
        public Chat Content
        {
            get => this["type"] switch
            {
                "qq" => new User(GetParameterAsInt64("id")),
                "group" => new Group(GetParameterAsInt64("id")),
                _ => null
            };

            set
            {
                if (value is null)
                {
                    return;
                }

                this["type"] = value switch
                {
                    User _ => "qq",
                    Group _ => "group",
                    _ => throw new ArgumentOutOfRangeException(nameof(value)),
                };

                SetParameter("id", value.Number);
            }
        }
    }
}