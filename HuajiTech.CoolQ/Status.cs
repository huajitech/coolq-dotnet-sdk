using HuajiTech.CoolQ.DataExchange;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示状态。
    /// </summary>
    public class Status
    {
        /// <summary>
        /// 以指定的值，单位和颜色初始化一个 <see cref="Status"/> 类的新实例。
        /// </summary>
        /// <param name="value">值。</param>
        /// <param name="unit">单位。</param>
        /// <param name="color">颜色。</param>
        public Status(string value, string unit, StatusColor color)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new System.ArgumentException(Resources.FieldCannotBeEmpty, nameof(value));
            }

            if (string.IsNullOrEmpty(unit))
            {
                throw new System.ArgumentException(Resources.FieldCannotBeEmpty, nameof(unit));
            }

            Color = color;
            Unit = unit;
            Value = value;
        }

        /// <summary>
        /// 获取颜色。
        /// </summary>
        public StatusColor Color { get; }

        /// <summary>
        /// 获取单位。
        /// </summary>
        public string Unit { get; }

        /// <summary>
        /// 获取值。
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// 编码。
        /// </summary>
        /// <returns>编码后的字符串。</returns>
        public string Encode()
        {
            using var writer = new StatusWriter();
            writer.Write(this);
            return writer.GetBase64();
        }
    }
}