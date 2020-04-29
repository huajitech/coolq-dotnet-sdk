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

        public ChatShare(IDictionary<string, string> parameters)
            : base("contact", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="ChatShare"/> 对象的内容。
        /// </summary>
        public QQ.Chat Content
        {
            get => this["type"] switch
            {
                "qq" => QQ.PluginContext.Current.GetUser(GetParameterAsInt64("id")),
                "group" => QQ.PluginContext.Current.GetGroup(GetParameterAsInt64("id")),
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
                    QQ.User _ => "qq",
                    QQ.Group _ => "group",
                    _ => throw new ArgumentOutOfRangeException(nameof(value)),
                };

                SetParameter("id", value.Number);
            }
        }
    }
}