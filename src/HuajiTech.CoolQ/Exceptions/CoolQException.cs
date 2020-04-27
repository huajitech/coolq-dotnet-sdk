using System;
using System.Runtime.Serialization;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 在酷Q返回了意料之外的值时引发的异常。
    /// </summary>
    [Serializable]
    public class CoolQException : Exception
    {
        public CoolQException(string message)
            : base(message)
        {
        }

        public CoolQException(string message, int returnValue)
            : base(message)
        {
            ReturnValue = returnValue;
        }

        public CoolQException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public CoolQException()
        {
        }

        protected CoolQException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public int? ReturnValue { get; }
    }
}