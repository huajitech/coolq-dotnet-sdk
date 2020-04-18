using System;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示消息元素。
    /// 此类为抽象类。
    /// </summary>
    public abstract class MessageElement : IEquatable<MessageElement>
    {
        public static MessageElement FromString(string str)
        {
            return new PlainText(str);
        }

        public ComplexMessage Add(MessageElement element)
        {
            return ComplexMessage.FromMessageElement(this).Add(element);
        }

        public abstract override string ToString();

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MessageElement);
        }

        public bool Equals(MessageElement other)
        {
            return base.Equals(other) || other?.ToString() == ToString();
        }

        public static bool operator !=(MessageElement left, MessageElement right)
        {
            return !(left == right);
        }

        public static bool operator ==(MessageElement left, MessageElement right)
        {
            return left?.Equals(right) ?? right is null;
        }

        public static ComplexMessage operator +(MessageElement left, MessageElement right)
        {
            return left?.Add(right);
        }

        public static implicit operator MessageElement(string str)
        {
            return FromString(str);
        }
    }
}