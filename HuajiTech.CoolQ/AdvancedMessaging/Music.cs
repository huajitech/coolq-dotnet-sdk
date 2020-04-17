using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示音乐。
    /// </summary>
    public class Music : CQCode
    {
        public Music()
        {
        }

        public Music(IDictionary<string, string> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// 获取或设置 ID。
        /// </summary>
        public int Id
        {
            get => GetParameterAsInt32("id");
            set => SetParameter("id", value);
        }

        /// <summary>
        /// 获取或设置音乐类型。
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

        public override string Type => "music";
    }
}