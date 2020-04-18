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
            get => GetParameterAsInt32("id");
            set => SetParameter("id", value);
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Music"/> 对象的音乐类型。
        /// </summary>
        public MusicType MusicType
        {
            get => this["type"] switch
            {
                "qq" => MusicType.QQ,
                "163" => MusicType.Netease,
                _ => MusicType.None
            };

            set => this["type"] = value switch
            {
                MusicType.QQ => "qq",
                MusicType.Netease => "163",
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}