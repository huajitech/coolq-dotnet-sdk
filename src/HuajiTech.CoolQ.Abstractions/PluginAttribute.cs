using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 指定插件类。
    /// 此类不能被继承。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class PluginAttribute : Attribute
    {
        /// <summary>
        /// 以指定的加载时机初始化一个 <see cref="PluginAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="loadTiming">加载时机。</param>
        public PluginAttribute(AppLifecycle loadTiming) => LoadTiming = loadTiming;

        /// <summary>
        /// 以 <see cref="AppLifecycle.Initializing"/> 初始化一个 <see cref="PluginAttribute"/> 的新实例。
        /// </summary>
        public PluginAttribute()
            : this(AppLifecycle.Initializing)
        {
        }

        /// <summary>
        /// 以指定的加载时机初始化一个 <see cref="PluginAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="loadTiming">加载时机的 <see cref="int"/> 值，如 <c>(int)LoadTiming.Enabled</c>。</param>
        public PluginAttribute(int loadTiming)
            : this((AppLifecycle)loadTiming)
        {
        }

        /// <summary>
        /// 以指定的加载时机初始化一个 <see cref="PluginAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="loadTiming">加载时机的 <see cref="string"/> 表示形式，如 <c>nameof(AppLifecycle.Enabled)</c>。</param>
        public PluginAttribute(string loadTiming)
            : this((AppLifecycle)Enum.Parse(typeof(AppLifecycle), loadTiming))
        {
        }

        /// <summary>
        /// 获取插件的加载时机。
        /// </summary>
        public AppLifecycle LoadTiming { get; }
    }
}