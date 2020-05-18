using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 指定插件加载阶段。
    /// 此类不能被继承。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class PluginLoadStageAttribute : Attribute
    {
        public PluginLoadStageAttribute(Type type, int loadStage)
        {
            if (!typeof(IPlugin).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(CoreResources.TypeIsNotPlugin);
            }

            Type = type;
            LoadStage = loadStage;
        }

        public Type Type { get; }

        public int LoadStage { get; }
    }
}