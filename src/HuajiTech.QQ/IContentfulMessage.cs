using System;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义消息。
    /// </summary>
    public interface IContentfulMessage : IMessage
    {
        /// <summary>
        /// 获取当前 <see cref="IContentfulMessage"/> 对象的内容。
        /// </summary>
        string Content { get; }
    }
}