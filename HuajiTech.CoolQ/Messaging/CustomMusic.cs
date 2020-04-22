using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示自定义音乐的 <see cref="CQCode"/>。
    /// </summary>
    public class CustomMusic : CQCode
    {
        public CustomMusic()
            : base("music")
        {
            this["type"] = "custom";
        }

        public CustomMusic(IDictionary<string, string> parameters)
            : base("music", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="CustomMusic"/> 对象的音频 URL。
        /// </summary>
        public Uri AudioUrl
        {
            get => GetParameterAsUri("audio");
            set => SetParameter("audio", value);
        }

        /// <summary>
        /// 获取或设置当前 <see cref="CustomMusic"/> 对象的内容 URL。
        /// </summary>
        public Uri ContentUrl
        {
            get => GetParameterAsUri("url");
            set => SetParameter("url", value);
        }

        /// <summary>
        /// 获取或设置当前 <see cref="CustomMusic"/> 对象的描述。
        /// </summary>
        public string Description
        {
            get => this["content"];
            set => this["content"] = value;
        }

        /// <summary>
        /// 获取或设置当前 <see cref="CustomMusic"/> 对象的图片URL。
        /// </summary>
        public Uri ImageUrl
        {
            get => GetParameterAsUri("image");
            set => SetParameter("image", value);
        }

        /// <summary>
        /// 获取或设置当前 <see cref="CustomMusic"/> 对象的标题。
        /// </summary>
        public string Title
        {
            get => this["title"];
            set => this["title"] = value;
        }
    }
}