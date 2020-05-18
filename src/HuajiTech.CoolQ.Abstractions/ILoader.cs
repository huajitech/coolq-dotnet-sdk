using System;
using System.Collections.Generic;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义加载器。
    /// </summary>
    public interface ILoader
    {
        TPlugin GetPlugin<TPlugin>()
          where TPlugin : notnull, IPlugin;

        IPlugin GetPlugin(Type pluginType);

        ICollection<IPlugin> GetPlugins(AppLifecycle loadStage);

        ICollection<IPlugin> GetPlugins();
    }
}