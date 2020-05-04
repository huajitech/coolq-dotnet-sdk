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
        /// <param name="type">要创建的 <see cref="CQCode"/> 对象的类型。</param>
        /// <param name="parameters">要创建的 <see cref="CQCode"/> 对象的参数。</param>
        /// <returns>一个 <see cref="CQCode"/> 类的新实例。</returns>
        /// <exception cref="ArgumentException"><paramref name="type"/> 为 <c>null</c>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        /// <exception cref="ArgumentNullException"><paramref name="parameters"/> 为 <c>null</c>。</exception>
        public static CQCode Create(string type, IDictionary<string, string> parameters)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(type));
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
                "at" when parameters["qq"] is "all" => new AtAll(parameters),
                "at" => new At(parameters),
                "rps" => new RockPaperScissors(parameters),
                "dice" => new Dice(parameters),
                "shake" => new Shake(parameters),
                "anonymous" => new Anonymous(parameters),
                "location" => new Location(parameters),
                "sign" => new ClockingIn(parameters),
                "music" when parameters["type"] is "custom" => new CustomMusic(parameters),
                "music" => new Music(parameters),
                "share" => new Share(parameters),
                "rich" => new RichText(parameters),
                "contact" => new ChatShare(parameters),
                _ => new CQCode(type, parameters)
            };
        }

        /// <summary>
        /// 以指定的类型创建一个 <see cref="CQCode"/> 类的新实例。
        /// </summary>
        /// <param name="type">要创建的 <see cref="CQCode"/> 对象的类型。</param>
        /// <returns>一个 <see cref="CQCode"/> 类的新实例。</returns>
        /// <exception cref="ArgumentException"><paramref name="type"/> 为 <c>null</c>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        public static CQCode Create(string type)
        {
            return Create(type, new Dictionary<string, string>());
        }
    }
}