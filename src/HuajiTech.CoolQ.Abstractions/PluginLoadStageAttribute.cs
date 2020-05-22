using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 指定插件加载阶段。
    /// 此类不能被继承。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class PluginLoadStageAttribute : Attribute
    {
        public PluginLoadStageAttribute(int loadStage)
        {
            LoadStage = loadStage;
        }

        public int LoadStage { get; }
    }
}