using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示签到的 <see cref="CQCode"/>。
    /// </summary>
    public class ClockingIn : CQCode
    {
        public ClockingIn()
            : base("sign")
        {
        }

        public ClockingIn(IDictionary<string, string> arguments)
            : base("sign", arguments)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="ClockingIn"/> 对象的图片 URL。
        /// </summary>
        public Uri ImageUrl
        {
            get => GetArgumentAsUri("image");
            set => SetArgument("image", value);
        }

        /// <summary>
        /// 获取或设置当前 <see cref="ClockingIn"/> 对象的发布位置。
        /// </summary>
        public string Location
        {
            get => this["location"];
            set => this["location"] = value;
        }

        /// <summary>
        /// 获取或设置当前 <see cref="ClockingIn"/> 对象的标题。
        /// </summary>
        public string Title
        {
            get => this["title"];
            set => this["title"] = value;
        }
    }
}