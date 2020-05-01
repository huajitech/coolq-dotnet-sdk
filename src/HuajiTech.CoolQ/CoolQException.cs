using HuajiTech.QQ;
using System;
using System.Runtime.Serialization;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 在酷Q返回了意料之外的值时引发的异常。
    /// </summary>
    [Serializable]
    public class CoolQException : ApiException
    {
        public CoolQException(string message)
            : base(message)
        {
        }

        public CoolQException(string message, int errorValue)
            : base(message, errorValue)
        {
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
    }
}