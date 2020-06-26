using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示消息。
    /// 此类为抽象类。
    /// </summary>
    public abstract class Message : IEquatable<Message?>
    {
        /// <summary>
        /// 在派生类中重写时，获取当前 <see cref="Message"/> 实例的 ID。
        /// </summary>
        public abstract int Id { get; }

        /// <summary>
        /// 在派生类中重写时，获取当前 <see cref="Message"/> 实例的内容。
        /// </summary>
        public abstract string Content { get; }

        public static implicit operator string?(Message? message) => message?.ToString();

        public sealed override string ToString() => Content ?? string.Empty;

        public override bool Equals(object? obj) => Equals(obj as Message);

        public bool Equals(Message? other) => other?.Id == Id;

        public override int GetHashCode() => Id.GetHashCode();

        /// <summary>
        /// 在派生类中重写时，撤回当前 <see cref="Message"/> 实例。
        /// </summary>
        public abstract void Recall();
    }
}