using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示CQ码。
    /// </summary>
    public class CQCode : MessageElement
    {
        /// <summary>
        /// 以指定的类型初始化一个 <see cref="CQCode"/> 类的新实例。
        /// </summary>
        /// <param name="type">CQ码的类型。</param>
        /// <exception cref="ArgumentException"><paramref name="type"/> 为 <see langword="null"/>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        public CQCode(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(type));
            }

            Type = type;
            Parameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// 以指定的类型和参数初始化一个 <see cref="CQCode"/> 类的新实例。
        /// </summary>
        /// <param name="type">CQ码的类型。</param>
        /// <param name="parameters">一个字典，包含 <see cref="CQCode"/> 的所有参数。</param>
        /// <exception cref="ArgumentException"><paramref name="type"/> 为 <see langword="null"/>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        /// <exception cref="ArgumentNullException"><paramref name="parameters"/> 为 <see langword="null"/>。</exception>
        public CQCode(string type, IDictionary<string, string> parameters)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(type));
            }

            Type = type;
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        /// <summary>
        /// 获取当前 <see cref="CQCode"/> 实例的参数。
        /// </summary>
        public IDictionary<string, string> Parameters { get; }

        /// <summary>
        /// 获取当前 <see cref="CQCode"/> 实例的类型。
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// 获取或设置指定键处的参数的值。
        /// </summary>
        /// <param name="key">要获取或设置的参数的键。</param>
        /// <value>
        /// 指定 <paramref name="key"/> 处的参数的值。
        /// 如果指定键不存在，则为 <see langword="null"/>。
        /// </value>
        public string? this[string key]
        {
            get
            {
                if (!Parameters.ContainsKey(key))
                {
                    return null;
                }

                return Parameters[key];
            }

            set
            {
                if (value is null)
                {
                    Parameters.Remove(key);
                    return;
                }

                Parameters[key] = value;
            }
        }

        /// <summary>
        /// 将指定的字符串转换为可以让酷Q按原义解释字符的语法。
        /// </summary>
        /// <param name="str">要转换的字符串。</param>
        /// <returns>指定字符串的已转换值。</returns>
        public static string Escape(string str) => PlainText.Escape(str).Replace(",", "&#44");

        /// <summary>
        /// 将字符串中的转义字符转换为具有特殊意义的酷Q字符。
        /// </summary>
        /// <param name="str">要转换的字符串。</param>
        /// <returns>指定字符串的已转换值。</returns>
        public static string Unescape(string str) => PlainText.Unescape(str).Replace("&#44", ",");

        /// <summary>
        /// 以指定的类型和参数创建一个 <see cref="CQCode"/> 类的新实例。
        /// </summary>
        /// <param name="type">要创建的 <see cref="CQCode"/> 实例的类型。</param>
        /// <param name="parameters">要创建的 <see cref="CQCode"/> 实例的参数。</param>
        /// <returns>一个 <see cref="CQCode"/> 类的新实例。</returns>
        /// <exception cref="ArgumentException"><paramref name="type"/> 为 <see langword="null"/>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        /// <exception cref="ArgumentNullException"><paramref name="parameters"/> 为 <see langword="null"/>。</exception>
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
                "at" when parameters["qq"] is "all" => new MentionAll(parameters),
                "at" => new Mention(parameters),
                "face" => new Emoticon(parameters),
                "emoji" => new Emoji(parameters),
                "bface" => new CustomEmoticon(parameters),
                "sface" => new SmallEmoticon(parameters),
                "image" => new Image(parameters),
                "record" => new Record(parameters),
                "music" when parameters["type"] is "custom" => new CustomMusic(parameters),
                "music" => new Music(parameters),
                "share" => new Share(parameters),
                "rich" => new RichText(parameters),
                "location" => new Location(parameters),
                "sign" => new SigningIn(parameters),
                "hb" => new RedEnvelope(parameters),
                "contact" => new ContactShare(parameters),
                "rps" => new RockPaperScissors(parameters),
                "dice" => new Dice(parameters),
                "shake" => new Shake(parameters),
                "anonymous" => new Anonymous(parameters),
                _ => new CQCode(type, parameters)
            };
        }

        /// <summary>
        /// 以指定的类型创建一个 <see cref="CQCode"/> 类的新实例。
        /// </summary>
        /// <param name="type">要创建的 <see cref="CQCode"/> 实例的类型。</param>
        /// <returns>一个 <see cref="CQCode"/> 类的新实例。</returns>
        /// <exception cref="ArgumentException"><paramref name="type"/> 为 <see langword="null"/>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        public static CQCode Create(string type) => Create(type, new Dictionary<string, string>());

        public sealed override string ToSendableString()
            => Parameters.Any() ?
                $"[CQ:{Type},{string.Join(",", Parameters.Select(param => $"{param.Key}={Escape(param.Value)}"))}]" :
                $"[CQ:{Type}]";

        public bool GetParameterAsBoolean(string key) => this[key] is "true";

        public int GetParameterAsInt32(string key)
        {
            if (int.TryParse(this[key], NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            {
                return value;
            }

            return default;
        }

        public long GetParameterAsInt64(string key)
        {
            if (long.TryParse(this[key], NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            {
                return value;
            }

            return default;
        }

        public float GetParameterAsSingle(string key)
        {
            if (float.TryParse(this[key], NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
            {
                return value;
            }

            return default;
        }

        public double GetParameterAsDouble(string key)
        {
            if (double.TryParse(this[key], NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
            {
                return value;
            }

            return default;
        }

        public Uri? GetParameterAsUri(string key)
        {
            try
            {
                return new Uri(this[key]);
            }
            catch (UriFormatException)
            {
                return default;
            }
        }

        public CQCode SetParameter(string key, bool value)
        {
            this[key] = value ? "true" : "false";
            return this;
        }

        public CQCode SetParameter(string key, int value)
        {
            this[key] = value.ToString(CultureInfo.InvariantCulture);
            return this;
        }

        public CQCode SetParameter(string key, long value)
        {
            this[key] = value.ToString(CultureInfo.InvariantCulture);
            return this;
        }

        public CQCode SetParameter(string key, float value)
        {
            this[key] = value.ToString(CultureInfo.InvariantCulture);
            return this;
        }

        public CQCode SetParameter(string key, double value)
        {
            this[key] = value.ToString(CultureInfo.InvariantCulture);
            return this;
        }

        public CQCode SetParameter(string key, Uri? value)
        {
            this[key] = value?.ToString();
            return this;
        }
    }
}