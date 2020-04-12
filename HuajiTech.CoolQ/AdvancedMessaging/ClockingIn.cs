using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示签到。
    /// </summary>
    public class ClockingIn : CQCode
    {
        public ClockingIn()
        {
        }

        public ClockingIn(IDictionary<string, string> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// 获取或设置图片 URL。
        /// </summary>
        public Uri ImageUrl
        {
            get => GetArgumentAsUri("image");
            set => SetArgument("image", value);
        }

        /// <summary>
        /// 获取或设置位置。
        /// </summary>
        public string Location
        {
            get => this["location"];
            set => this["location"] = value;
        }

        /// <summary>
        /// 获取或设置标题。
        /// </summary>
        public string Title
        {
            get => this["title"];
            set => this["title"] = value;
        }

        public override string Type => "sign";
    }
}