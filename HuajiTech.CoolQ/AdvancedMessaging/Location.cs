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

        public Location(IDictionary<string, string> parameters)
            : base(parameters)
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
            get => GetParameterAsSingle("lat");
            set => SetParameter("lat", value);
        }

        /// <summary>
        /// 获取或设置经度。
        /// </summary>
        public float Longitude
        {
            get => GetParameterAsSingle("lon");
            set => SetParameter("lon", value);
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