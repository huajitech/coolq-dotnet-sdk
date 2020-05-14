using System.Collections.Generic;
using System.ComponentModel;

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

        public Music(IDictionary<string, string> parameters)
            : base("music", parameters)
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
        /// 获取或设置当前 <see cref="Music"/> 对象的提供商。
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException"><paramref name="value" /> 不是有效的 <see cref="MusicPlatform"/> 值。</exception>
        public MusicPlatform Platform
        {
            get => this["type"] switch
            {
                "qq" => MusicPlatform.QQ,
                "163" => MusicPlatform.Netease,
                _ => MusicPlatform.None
            };

            set => this["type"] = value switch
            {
                MusicPlatform.QQ => "qq",
                MusicPlatform.Netease => "163",
                _ => throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(MusicPlatform))
            };
        }
    }
}