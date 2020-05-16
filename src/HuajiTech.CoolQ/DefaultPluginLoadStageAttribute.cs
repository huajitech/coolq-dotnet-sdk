using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 指定缺省插件加载阶段。
    /// 此类不能被继承。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class DefaultPluginLoadStageAttribute : Attribute
    {
        /// <summary>
        /// 以指定的加载阶段初始化一个 <see cref="DefaultPluginLoadStageAttribute"/> 类的新实例。
        /// </summary>
        /// <param name="loadStage">插件加载阶段，与 <see cref="AppLifecycle"/> 枚举对应。</param>
        public DefaultPluginLoadStageAttribute(int loadStage)
        {
            LoadStage = loadStage;
        }

        /// <summary>
        /// 获取当前 <see cref="DefaultPluginLoadStageAttribute"/> 对象的插件加载阶段。
        /// </summary>
        public int LoadStage { get; }
    }
}