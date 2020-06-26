using System;
using System.Runtime.Serialization;

namespace HuajiTech.CoolQ
{
    [Serializable]
    public class PluginLoadException : Exception
    {
        public PluginLoadException()
        {
        }

        public PluginLoadException(string message)
            : base(message)
        {
        }

        public PluginLoadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected PluginLoadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}