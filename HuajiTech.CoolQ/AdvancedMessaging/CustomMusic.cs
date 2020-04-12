using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示自定义音乐。
    /// </summary>
    public class CustomMusic : CQCode
    {
        public CustomMusic()
        {
            this["type"] = "custom";
        }

        public CustomMusic(IDictionary<string, string> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// 获取或设置音频 URL。
        /// </summary>
        public Uri AudioUrl
        {
            get => GetArgumentAsUri("audio");
            set => SetArgument("audio", value);
        }

        /// <summary>
        /// 获取或设置内容 URL。
        /// </summary>
        public Uri ContentUrl
        {
            get => GetArgumentAsUri("url");
            set => SetArgument("url", value);
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
        /// 获取或设置图片URL。
        /// </summary>
        public Uri ImageUrl
        {
            get => GetArgumentAsUri("image");
            set => SetArgument("image", value);
        }

        /// <summary>
        /// 获取或设置标题。
        /// </summary>
        public string Title
        {
            get => this["title"];
            set => this["title"] = value;
        }

        public override string Type => "music";
    }
}