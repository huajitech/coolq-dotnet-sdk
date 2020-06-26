using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示分享的 <see cref="CQCode"/>。
    /// </summary>
    public class Share : CQCode
    {
        public Share()
            : base("share")
        {
        }

        public Share(IDictionary<string, string> parameters)
            : base("share", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Share"/> 实例的描述。
        /// </summary>
        public string? Description
        {
            get => this["content"];
            set => this["content"] = value;
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Share"/> 实例的图片 URL。
        /// </summary>
        public Uri? ImageUrl
        {
            get => GetParameterAsUri("image");
            set => SetParameter("image", value);
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Share"/> 实例的标题。
        /// </summary>
        public string? Title
        {
            get => this["title"];
            set => this["title"] = value;
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Share"/> 实例的 URL。
        /// </summary>
        public Uri? Url
        {
            get => GetParameterAsUri("url");
            set => SetParameter("url", value);
        }
    }
}