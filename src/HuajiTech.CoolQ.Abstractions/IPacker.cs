using System.Collections.Generic;
using System.Reflection;

namespace HuajiTech.CoolQ.Packing
{
    /// <summary>
    /// 定义打包器。
    /// </summary>
    public interface IPacker
    {
        /// <summary>
        /// 获取由打包器打包的程序集。
        /// </summary>
        IReadOnlyCollection<AssemblyName> GetPackedAssemblies();
    }
}