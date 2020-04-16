using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 指示用作酷Q应用入口点的类。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class AppAttribute : Attribute
    {
        /// <summary>
        /// 以指定的 ID 初始化一个 <see cref="AppAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="id">ID。</param>
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