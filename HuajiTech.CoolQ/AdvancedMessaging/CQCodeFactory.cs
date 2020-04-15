using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.AdvancedMessaging
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
        /// <param name="parameters">参数。</param>
        /// <returns><see cref="CQCode"/> 类的新实例。</returns>
        public static CQCode Create(string type, IDictionary<string, string> parameters)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmpty, nameof(type));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return type switch
            {
                "face" => new Emoticon(parameters),
                "emoji" => new Emoji(parameters),
                "bface" => new CustomEmoticon(parameters),
                "sface" => new SmallEmoticon(parameters),
                "image" => new Image(parameters),
                "record" => new Record(parameters),
                "at" when parameters["qq"] != "all" => new At(parameters),
                "at" when parameters["qq"] == "all" => new AtAll(parameters),
                "rps" => new RockPaperScissors(parameters),
                "dice" => new Dice(parameters),
                "shake" => new Shake(parameters),
                "anonymous" => new Anonymous(parameters),
                "location" => new Location(parameters),
                "sign" => new ClockingIn(parameters),
                "music" when parameters["type"] != "custom" => new Music(parameters),
                "music" when parameters["type"] == "custom" => new CustomMusic(parameters),
                "share" => new Share(parameters),
                "rich" => new RichText(parameters),
                "contact" => new ChatShare(parameters),
                _ => new CQCode(type, parameters)
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