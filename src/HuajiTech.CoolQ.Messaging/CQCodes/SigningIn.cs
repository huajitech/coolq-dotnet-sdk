using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示签到的 <see cref="CQCode"/>。
    /// </summary>
    public class SigningIn : CQCode
    {
        public SigningIn()
            : base("sign")
        {
        }

        public SigningIn(IDictionary<string, string> parameters)
            : base("sign", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="SigningIn"/> 实例的图片 URL。
        /// </summary>
        public Uri? ImageUrl
        {
            get => GetParameterAsUri("image");
            set => SetParameter("image", value);
        }

        /// <summary>
        /// 获取或设置当前 <see cref="SigningIn"/> 实例的发布位置。
        /// </summary>
        public string? Location
        {
            get => this["location"];
            set => this["location"] = value;
        }

        /// <summary>
        /// 获取或设置当前 <see cref="SigningIn"/> 实例的标题。
        /// </summary>
        public string? Title
        {
            get => this["title"];
            set => this["title"] = value;
        }
    }
}