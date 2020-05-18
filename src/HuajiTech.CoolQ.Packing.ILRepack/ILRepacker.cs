using System;
using System.Collections.Generic;
using System.Reflection;

namespace HuajiTech.CoolQ.Packing
{
    public class ILRepacker : IPacker
    {
        static ILRepacker()
        {
            AppDomain.CurrentDomain.AssemblyResolve +=
                (sender, e) => Assembly.GetExecutingAssembly();

            AppDomain.CurrentDomain.ResourceResolve +=
                (sender, e) => Assembly.GetExecutingAssembly();
        }

        public IReadOnlyCollection<AssemblyName> GetPackedAssemblies()
        {
            throw new NotImplementedException();
        }
    }
}