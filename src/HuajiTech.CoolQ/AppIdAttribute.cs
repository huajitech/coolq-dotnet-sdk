using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 指示当前程序集的 AppID。
    /// 此类不能被继承。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class AppIdAttribute : Attribute
    {
        /// <summary>
        /// 以指定的 ID 初始化一个 <see cref="AppIdAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="id">应用的 AppID。</param>
        public AppIdAttribute(string id)
        {
            Id = id;
        }

        /// <summary>
        /// 获取当前 <see cref="AppIdAttribute"/> 对象的 ID。
        /// </summary>
        public string Id { get; }
    }
}