using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示分享。
    /// </summary>
    public class Share : CQCode
    {
        public Share()
        {
        }

        public Share(IDictionary<string, string> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// 获取或设置描述。
        /// </summary>
        public string Description
        {
            get => this["content"];
            set => this["content"] = value;
        }

        /// <summary>
        /// 获取或设置图片 URL。
        /// </summary>
        public Uri ImageUrl
        {
            get => GetParameterAsUri("image");
            set => SetParameter("image", value);
        }

        /// <summary>
        /// 获取或设置标题。
        /// </summary>
        public string Title
        {
            get => this["title"];
            set => this["title"] = value;
        }

        /// <summary>
        /// 获取或设置 URL。
        /// </summary>
        public Uri Url
        {
            get => GetParameterAsUri("url");
            set => SetParameter("url", value);
        }

        public override string Type => "share";
    }
}