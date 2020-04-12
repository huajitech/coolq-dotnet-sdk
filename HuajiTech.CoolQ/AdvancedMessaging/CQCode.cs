using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示CQ码。
    /// </summary>
    public class CQCode : MessageElement, IEquatable<CQCode>
    {
        /// <summary>
        /// 以指定类型初始化一个 <see cref="CQCode"/> 类的新实例。
        /// </summary>
        /// <param name="type">类型。</param>
        public CQCode(string type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// 以指定的类型和参数初始化一个 <see cref="CQCode"/> 类的新实例。
        /// </summary>
        /// <param name="type">类型。</param>
        /// <param name="arguments">参数。</param>
        public CQCode(string type, IDictionary<string, string> arguments)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmptyOrWhiteSpace, nameof(type));
            }

            Type = type;
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        /// <summary>
        /// 初始化一个 <see cref="CQCode"/> 类的新实例。
        /// </summary>
        protected CQCode()
            : this(new Dictionary<string, string>())
        {
        }

        /// <summary>
        /// 以指定参数初始化一个 <see cref="CQCode"/> 类的新实例。
        /// </summary>
        /// <param name="arguments">参数。</param>
        protected CQCode(IDictionary<string, string> arguments)
        {
            Arguments = arguments;
        }

        /// <summary>
        /// 获取或设置指定键的参数。
        /// </summary>
        /// <param name="key">键。</param>
        public string this[string key]
        {
            get
            {
                if (!Arguments.ContainsKey(key))
                {
                    return null;
                }

                return Arguments[key];
            }

            set => Arguments[key] = value;
        }

        /// <summary>
        /// 获取当前 <see cref="CQCode"/> 对象的参数。
        /// </summary>
        public IDictionary<string, string> Arguments { get; }

        /// <summary>
        /// 获取当前 <see cref="CQCode"/> 对象的类型。
        /// </summary>
        public virtual string Type { get; }

        /// <summary>
        /// 对字符串进行转义。
        /// </summary>
        /// <param name="str">要转义的字符串。</param>
        /// <returns>转义后的字符串。</returns>
        public static string Escape(string str)
        {
            return PlainText.Escape(str).Replace(",", "&#44");
        }

        /// <summary>
        /// 对字符串进行反转义。
        /// </summary>
        /// <param name="str">要反转义的字符串。</param>
        /// <returns>反转义后的字符串。</returns>
        public static string Unescape(string str)
        {
            return PlainText.Unescape(str).Replace("&#44", ",");
        }

        public override string ToString()
        {
            return Arguments.Any() ?
                $"[CQ:{Type},{string.Join(",", Arguments.Select(para => $"{para.Key}={Escape(para.Value)}"))}]" :
                $"[CQ:{Type}]";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) || obj as CQCode == this;
        }

        public bool Equals(CQCode other)
        {
            return other == this;
        }

        protected bool GetArgumentAsBoolean(string key)
        {
            return this[key] == "true";
        }

        protected int GetArgumentAsInt32(string key)
        {
            if (int.TryParse(this[key], NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            {
                return value;
            }

            return default;
        }

        protected long GetArgumentAsInt64(string key)
        {
            if (long.TryParse(this[key], NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            {
                return value;
            }

            return default;
        }

        protected float GetArgumentAsSingle(string key)
        {
            if (float.TryParse(this[key], NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
            {
                return value;
            }

            return default;
        }

        protected Uri GetArgumentAsUri(string key)
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

        protected void SetArgument(string key, bool value)
        {
            this[key] = value ? "true" : "false";
        }

        protected void SetArgument(string key, int value)
        {
            this[key] = value.ToString(CultureInfo.InvariantCulture);
        }

        protected void SetArgument(string key, long value)
        {
            this[key] = value.ToString(CultureInfo.InvariantCulture);
        }

        protected void SetArgument(string key, float value)
        {
            this[key] = value.ToString(CultureInfo.InvariantCulture);
        }

        protected void SetArgument(string key, Uri value)
        {
            this[key] = value?.ToString();
        }

        public static bool operator !=(CQCode left, CQCode right)
        {
            return !(left == right);
        }

        public static bool operator ==(CQCode left, CQCode right)
        {
            return left?.ToString() == right?.ToString();
        }
    }
}