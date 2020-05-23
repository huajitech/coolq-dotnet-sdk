using System;
using HuajiTech.CoolQ.DataExchange;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示状态。
    /// </summary>
    public class Status : IEquatable<Status?>
    {
        /// <summary>
        /// 以指定的值，单位和颜色初始化一个 <see cref="Status"/> 类的新实例。
        /// </summary>
        /// <param name="value">状态的值。</param>
        /// <param name="unit">状态的单位。</param>
        /// <param name="color">状态的颜色。</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> 为 <c>null</c>。</exception>
        /// <exception cref="ArgumentNullException"><paramref name="unit"/> 为 <c>null</c>。</exception>
        public Status(string value, string unit, StatusColor color)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            Color = color;
        }

        /// <summary>
        /// 获取当前 <see cref="Status"/> 实例的颜色。
        /// </summary>
        public StatusColor Color { get; }

        /// <summary>
        /// 获取当前 <see cref="Status"/> 实例的单位。
        /// </summary>
        public string Unit { get; }

        /// <summary>
        /// 获取当前 <see cref="Status"/> 实例的值。
        /// </summary>
        public string Value { get; }

        public static bool operator ==(Status left, Status right) => left?.Equals(right) ?? right is null;

        public static bool operator !=(Status left, Status right) => !(left == right);

        /// <summary>
        /// 将当前 <see cref="Status"/> 实例的值编码为可被酷Q使用的 Base64 字符串。
        /// </summary>
        /// <returns>编码后的 Base64 字符串。</returns>
        public string Encode()
        {
            using var writer = new StatusWriter();
            writer.Write(this);
            return writer.GetBase64();
        }

        public bool Equals(Status? other) => other?.Value == Value && other?.Unit == Unit && other?.Color == Color;

        public override bool Equals(object? obj) => base.Equals(obj) || Equals(obj as Status);

        public override int GetHashCode() => (int)Color ^ Value.GetHashCode() ^ Unit.GetHashCode();
    }
}