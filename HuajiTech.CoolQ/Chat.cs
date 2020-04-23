using System;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示聊天。
    /// 此类为抽象类。
    /// </summary>
    public abstract class Chat : IEquatable<Chat>
    {
        protected Chat(long number)
        {
            Number = number;
        }

        /// <summary>
        /// 获取当前 <see cref="Chat"/> 对象的显示名称。
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// 获取当前 <see cref="Chat"/> 对象的号码。
        /// </summary>
        public long Number { get; }

        public static bool operator !=(Chat left, Chat right)
        {
            return !(left == right);
        }

        public static bool operator ==(Chat left, Chat right)
        {
            return left?.Equals(right) ?? right is null;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Chat);
        }

        public virtual bool Equals(Chat other)
        {
            return base.Equals(other) || other?.Number == Number;
        }

        public override int GetHashCode()
        {
            return (int)Number;
        }

        /// <summary>
        /// 向当前 <see cref="Chat"/> 对象发送消息。
        /// </summary>
        /// <param name="message">要发送的消息。</param>
        /// <returns>一个 <see cref="Message"/> 对象，表示已发送的消息。</returns>
        public abstract Message Send(string message);

        /// <summary>
        /// 以异步操作向当前 <see cref="Chat"/> 对象发送消息。
        /// </summary>
        /// <param name="message">要发送的消息。</param>
        /// <returns>一个 <see cref="Message"/> 对象，表示已发送的消息。</returns>
        public virtual Task<Message> SendAsync(string message)
        {
            return Task.Run(() => Send(message));
        }

        public override string ToString()
        {
            return $"{GetType().Name}({Number})";
        }
    }
}