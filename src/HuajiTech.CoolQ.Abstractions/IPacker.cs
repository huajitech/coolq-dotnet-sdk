using System.Collections.Generic;
using System.Reflection;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义打包器。
    /// </summary>
    public interface IPacker
    {
        IReadOnlyCollection<AssemblyName> GetPackedAssemblies();
    }
}