using System.Reflection;
using Autofac;
using HuajiTech.CoolQ.Loaders;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 提供默认的初始化实现。
    /// </summary>
    public static class DefaultInitializer
    {
        /// <summary>
        /// 初始化 <see cref="AutofacLoader"/>。
        /// </summary>
        public static void Init()
        {
            new AutofacLoader(new ContainerBuilder())
                .RegisterSdk()
                .RegisterPlugins(Assembly.GetExecutingAssembly())
                .Build()
                .Init();
        }
    }
}