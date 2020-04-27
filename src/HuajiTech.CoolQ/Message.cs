using HuajiTech.QQ;
using System;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示消息。
    /// </summary>
    internal class Message : IMessage, IEquatable<Message>
    {
        /// <summary>
        /// 以指定的 ID 和内容初始化一个 <see cref="Message"/> 类的新实例。
        /// </summary>
        internal Message(long id, string content)
        {
            Id = id;
            Content = content;
        }

        /// <summary>
        /// 获取当前 <see cref="Message"/> 对象的内容。
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// 获取当前 <see cref="Message"/> 对象的ID。
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// 撤回当前 <see cref="Message"/> 对象。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Recall()
        {
            NativeMethods.RecallMessage(Bot.Instance.AuthCode, Id).CheckError();
        }

        /// <summary>
        /// 以异步操作撤回当前 <see cref="Message"/> 对象。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task RecallAsync()
        {
            return Task.Run(Recall);
        }

        public bool Equals(Message other)
        {
            return other?.Id == Id && other?.Content == Content;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) || Equals(obj as Message);
        }

        public override int GetHashCode()
        {
            return (int)Id ^ Content.GetHashCode();
        }

        public override string ToString()
        {
            return Content;
        }

        public static bool operator ==(Message left, Message right)
        {
            return left?.Equals(right) ?? right is null;
        }

        public static bool operator !=(Message left, Message right)
        {
            return !(left == right);
        }

        public static implicit operator string(Message message)
        {
            return message?.Content;
        }
    }
}