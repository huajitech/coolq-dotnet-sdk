using System;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示聊天。
    /// 此类为抽象类。
    /// </summary>
    public abstract class Chat : ISendable, IEquatable<Chat>
    {
        /// <summary>
        /// 以指定的号码初始化一个 <see cref="Chat"/> 类的新实例。
        /// </summary>
        /// <param name="number">号码。</param>
        protected Chat(long number) => Number = number;

        /// <summary>
        /// 获取当前 <see cref="Chat"/> 对象的显示名称。
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// 获取当前 <see cref="Chat"/> 对象的号码。
        /// </summary>
        public long Number { get; }

        public static bool operator !=(Chat left, Chat right) => !(left == right);

        public static bool operator ==(Chat left, Chat right) => left?.Equals(right) ?? right is null;

        public override bool Equals(object obj) => Equals(obj as Chat);

        public virtual bool Equals(Chat other) => base.Equals(other) || other?.Number == Number;

        public override int GetHashCode() => (int)Number;

        public abstract Message Send(string message);

        public virtual Task<Message> SendAsync(string message) => Task.Run(() => Send(message));

        public override string ToString() => $"{GetType().Name}({Number})";
    }
}