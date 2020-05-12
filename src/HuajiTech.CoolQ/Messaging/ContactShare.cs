using HuajiTech.QQ;
using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示名片分享的 <see cref="CQCode"/>。
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
        /// 获取或设置当前 <see cref="ContactShare"/> 对象的内容。
        /// </summary>
        public IChattable? Content
        {
            get => this["type"] switch
            {
                "qq" => QQ.PluginContext.CurrentContext.GetUser(GetParameterAsInt64("id")),
                "group" => QQ.PluginContext.CurrentContext.GetGroup(GetParameterAsInt64("id")),
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