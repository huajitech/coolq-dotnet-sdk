using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示音乐的 <see cref="CQCode"/>。
    /// </summary>
    public class Music : CQCode
    {
        public Music()
            : base("music")
        {
        }

        public Music(IDictionary<string, string> arguments)
            : base("music", arguments)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Music"/> 对象的 ID。
        /// </summary>
        public int Id
        {
            get => GetArgumentAsInt32("id");
            set => SetArgument("id", value);
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Music"/> 对象的提供商。
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value" /> 不是有效的 <see cref="MusicProvider"/> 值。</exception>
        public MusicProvider Provider
        {
            get => this["type"] switch
            {
                "qq" => MusicProvider.QQ,
                "163" => MusicProvider.Netease,
                _ => MusicProvider.None
            };

            set => this["type"] = value switch
            {
                MusicProvider.QQ => "qq",
                MusicProvider.Netease => "163",
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}