using System;
using System.Runtime.Serialization;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 在调用 API 发生错误时引发的异常。
    /// </summary>
    [Serializable]
    public class ApiException : Exception
    {
        public ApiException()
        {
        }

        public ApiException(string message)
            : base(message)
        {
        }

        public ApiException(string message, int errorValue)
            : this(message)
        {
            ErrorValue = errorValue;
        }

        public ApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public int? ErrorValue { get; }
    }
}