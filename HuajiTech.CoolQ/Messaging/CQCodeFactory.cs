using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 提供用于创建 <see cref="CQCode"/> 对象的方法的静态类。
    /// </summary>
    public static class CQCodeFactory
    {
        /// <summary>
        /// 以指定的类型和参数创建一个 <see cref="CQCode"/> 类的新实例。
        /// </summary>
        /// <param name="type">类型。</param>
        /// <param name="arguments">参数。</param>
        /// <returns><see cref="CQCode"/> 类的新实例。</returns>
        public static CQCode Create(string type, IDictionary<string, string> arguments)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmpty, nameof(type));
            }

            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return type switch
            {
                "face" => new Emoticon(arguments),
                "emoji" => new Emoji(arguments),
                "bface" => new CustomEmoticon(arguments),
                "sface" => new SmallEmoticon(arguments),
                "image" => new Image(arguments),
                "record" => new Record(arguments),
                "at" when arguments["qq"] != "all" => new At(arguments),
                "at" when arguments["qq"] == "all" => new AtAll(arguments),
                "rps" => new RockPaperScissors(arguments),
                "dice" => new Dice(arguments),
                "shake" => new Shake(arguments),
                "anonymous" => new Anonymous(arguments),
                "location" => new Location(arguments),
                "sign" => new ClockingIn(arguments),
                "music" when arguments["type"] != "custom" => new Music(arguments),
                "music" when arguments["type"] == "custom" => new CustomMusic(arguments),
                "share" => new Share(arguments),
                "rich" => new RichText(arguments),
                "contact" => new ChatShare(arguments),
                _ => new CQCode(type, arguments)
            };
        }

        /// <summary>
        /// 以指定类型创建一个 <see cref="CQCode"/> 类的新实例。
        /// </summary>
        /// <param name="type">类型。</param>
        /// <returns><see cref="CQCode"/> 类的新实例。</returns>
        public static CQCode Create(string type)
        {
            return Create(type, new Dictionary<string, string>());
        }
    }
}