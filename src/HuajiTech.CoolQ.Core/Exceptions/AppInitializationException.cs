using System;
using System.Runtime.Serialization;

namespace HuajiTech.CoolQ
{
    [Serializable]
    public class AppInitializationException : Exception
    {
        public AppInitializationException()
        {
        }

        public AppInitializationException(string message)
            : base(message)
        {
        }

        public AppInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AppInitializationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}