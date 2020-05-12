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
        /// 获取或设置当前 <see cref="Location"/> 对象的地址。
        /// </summary>
        public string? Address
        {
            get => this["content"];
            set => this["content"] = value;
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Location"/> 对象的纬度。
        /// </summary>
        public float Latitude
        {
            get => GetParameterAsSingle("lat");
            set => SetParameter("lat", value);
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Location"/> 对象的经度。
        /// </summary>
        public float Longitude
        {
            get => GetParameterAsSingle("lon");
            set => SetParameter("lon", value);
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Location"/> 对象的名称。
        /// </summary>
        public string? Name
        {
            get => this["title"];
            set => this["title"] = value;
        }
    }
}