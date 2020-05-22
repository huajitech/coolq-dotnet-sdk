using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义消息。
    /// </summary>
    public interface IMessage : IEquatable<IMessage?>
    {
        /// <summary>
        /// 获取当前 <see cref="IMessage"/> 实例的 ID。
        /// </summary>
        long Id { get; }

        /// <summary>
        /// 获取当前 <see cref="IMessage"/> 实例的内容。
        /// </summary>
        string Content { get; }

        /// <summary>
        /// 撤回当前 <see cref="IMessage"/> 实例。
        /// </summary>
        void Recall();
    }
}