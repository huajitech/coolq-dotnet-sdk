using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 指示用作酷Q应用入口点的类。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class AppAttribute : Attribute
    {
        public AppAttribute(string id)
        {
            Id = id;
        }

        /// <summary>
        /// 获取 ID。
        /// </summary>
        public string Id { get; }
    }
}