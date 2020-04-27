using HuajiTech.QQ;
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
        public IChattable Content
        {
            get => this["type"] switch
            {
                "qq" => QQ.AppContext.CurrentContext.GetUser(GetParameterAsInt64("id")),
                "group" => QQ.AppContext.CurrentContext.GetGroup(GetParameterAsInt64("id")),
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
                    IUser _ => "qq",
                    IGroup _ => "group",
                    _ => throw new ArgumentOutOfRangeException(nameof(value)),
                };

                SetParameter("id", value.Number);
            }
        }
    }
}