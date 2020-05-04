using System;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义消息。
    /// </summary>
    public interface IMessage : IEquatable<IMessage>
    {
        /// <summary>
        /// 获取当前 <see cref="IMessage"/> 对象的标识符。
        /// </summary>
        long Id { get; }

        /// <summary>
        /// 获取当前 <see cref="IMessage"/> 对象的内容。
        /// </summary>
        string Content { get; }

        /// <summary>
        /// 撤回当前 <see cref="IMessage"/> 对象。
        /// </summary>
        void Recall();
    }
}