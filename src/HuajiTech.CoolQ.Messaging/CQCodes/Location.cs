using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示位置的 <see cref="CQCode"/>。
    /// </summary>
    public class Location : CQCode
    {
        public Location()
            : base("location")
        {
        }

        public Location(IDictionary<string, string> parameters)
            : base("location", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Location"/> 实例的地址。
        /// </summary>
        public string? Address
        {
            get => this["content"];
            set => this["content"] = value;
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Location"/> 实例的纬度。
        /// </summary>
        public double Latitude
        {
            get => GetParameterAsDouble("lat");
            set => SetParameter("lat", value);
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Location"/> 实例的经度。
        /// </summary>
        public double Longitude
        {
            get => GetParameterAsDouble("lon");
            set => SetParameter("lon", value);
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Location"/> 实例的名称。
        /// </summary>
        public string? Name
        {
            get => this["title"];
            set => this["title"] = value;
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Location"/> 实例的缩放等级。
        /// </summary>
        public int Scale
        {
            get => GetParameterAsInt32("zoom");
            set => SetParameter("zoom", value);
        }
    }
}