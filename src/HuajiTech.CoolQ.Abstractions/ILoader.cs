using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ.Loaders
{
    /// <summary>
    /// 定义加载器。
    /// </summary>
    public interface ILoader
    {
        /// <summary>
        /// 获取指定类型的插件。
        /// </summary>
        /// <typeparam name="T">插件的类型。</typeparam>
        /// <returns>指定的插件类型的实例。</returns>
        T GetPlugin<T>()
            where T : notnull;

        /// <summary>
        /// 获取指定类型的插件。
        /// </summary>
        /// <param name="pluginType">插件的类型。</param>
        /// <returns>指定的插件类型的实例。</returns>
        object GetPlugin(Type pluginType);

        /// <summary>
        /// 获取在指定加载时机加载的插件。
        /// </summary>
        /// <param name="loadTiming">加载时机。</param>
        /// <returns>取在指定加载时机加载的插件集合。
        /// 如果没有在指定的加载时机加载的插件，则该方法返回空序列。</returns>
        IReadOnlyCollection<object> GetPlugins(AppLifecycle loadTiming);
    }
}