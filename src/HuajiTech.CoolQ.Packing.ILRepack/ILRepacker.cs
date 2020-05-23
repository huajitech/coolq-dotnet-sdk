using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace HuajiTech.CoolQ.Packing
{
    public class ILRepacker : IPacker
    {
        internal const string ILRepackListResourceName = "ILRepack.List";

        static ILRepacker()
        {
            AppDomain.CurrentDomain.AssemblyResolve +=
                (sender, e) => Assembly.GetExecutingAssembly();

            AppDomain.CurrentDomain.ResourceResolve +=
                (sender, e) => Assembly.GetExecutingAssembly();
        }

        public IReadOnlyCollection<AssemblyName> GetPackedAssemblies()
        {
            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ILRepackListResourceName);
            var packedAssemblyNames = (string[])new BinaryFormatter().Deserialize(resourceStream);
            return packedAssemblyNames.Select(name => new AssemblyName(name)).ToList();
        }
    }
}