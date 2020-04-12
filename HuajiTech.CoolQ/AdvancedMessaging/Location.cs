using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示地点。
    /// </summary>
    public class Location : CQCode
    {
        public Location()
        {
        }

        public Location(IDictionary<string, string> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// 获取或设置地址。
        /// </summary>
        public string Address
        {
            get => this["content"];
            set => this["content"] = value;
        }

        /// <summary>
        /// 获取或设置纬度。
        /// </summary>
        public float Latitude
        {
            get => GetArgumentAsSingle("lat");
            set => SetArgument("lat", value);
        }

        /// <summary>
        /// 获取或设置经度。
        /// </summary>
        public float Longitude
        {
            get => GetArgumentAsSingle("lon");
            set => SetArgument("lon", value);
        }

        /// <summary>
        /// 获取或设置名称。
        /// </summary>
        public string Name
        {
            get => this["title"];
            set => this["title"] = value;
        }

        public override string Type => "location";
    }
}