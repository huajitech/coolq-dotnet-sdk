using System;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示消息。
    /// 此类为抽象类。
    /// </summary>
    public abstract class Message : IEquatable<Message>
    {
        /// <summary>
        /// 以指定的标识符和内容初始化一个 <see cref="Message"/> 类的新实例。
        /// </summary>
        /// <param name="content">内容。</param>
        protected Message(string content) => Content = content ?? throw new ArgumentNullException(nameof(content));

        /// <summary>
        /// 获取当前 <see cref="Message"/> 对象的内容。
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// 撤回当前 <see cref="Message"/> 对象。
        /// </summary>
        public abstract void Recall();

        /// <summary>
        /// 以异步操作撤回当前 <see cref="Message"/> 对象。
        /// </summary>
        public virtual Task RecallAsync() => Task.Run(Recall);

        public virtual bool Equals(Message other) => base.Equals(other) || other?.Content == Content;

        public override bool Equals(object obj) => Equals(obj as Message);

        public override int GetHashCode() => Content.GetHashCode();

        public override string ToString() => Content;

        public static bool operator ==(Message left, Message right) => left?.Equals(right) ?? right is null;

        public static bool operator !=(Message left, Message right) => !(left == right);

        public static implicit operator string(Message message) => message?.Content;
    }
}