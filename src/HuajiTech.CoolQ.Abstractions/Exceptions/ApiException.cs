using System;
using System.Runtime.Serialization;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 在 API 调用发生错误时引发的异常。
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
            ErrorCode = errorValue;
        }

        public ApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// 获取当前 <see cref="ApiException"/> 实例的错误代码。
        /// 如果没有错误代码，则为 <see langword="null"/>。
        /// </summary>
        public int? ErrorCode { get; }
    }
}