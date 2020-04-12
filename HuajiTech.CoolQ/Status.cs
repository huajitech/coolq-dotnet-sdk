using HuajiTech.CoolQ.DataExchange;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示状态。
    /// </summary>
    public class Status
    {
        /// <summary>
        /// 获取或设置颜色。
        /// </summary>
        public StatusColor Color { get; set; }

        /// <summary>
        /// 获取或设置单位。
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 获取或设置值。
        /// </summary>
        public string Value { get; set; }

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