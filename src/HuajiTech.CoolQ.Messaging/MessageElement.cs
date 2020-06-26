using System;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示消息元素。
    /// 此类为抽象类。
    /// </summary>
    public abstract class MessageElement : ISendable, IEquatable<MessageElement?>
    {
        public static implicit operator MessageElement(string? str) => FromString(str);

        public static implicit operator ComplexMessage(MessageElement? element)
            => element?.ToComplexMessage() ?? new ComplexMessage();

        public static bool operator !=(MessageElement? left, MessageElement? right)
            => !(left == right);

        public static bool operator ==(MessageElement? left, MessageElement? right)
            => left?.Equals(right) ?? right is null;

        public static ComplexMessage operator +(MessageElement? left, MessageElement? right)
            => right is null ? left ?? new ComplexMessage() : left?.Add(right) ?? right;

        public static MessageElement FromString(string? str) => new PlainText(str);

        public ComplexMessage Add(MessageElement element) => ToComplexMessage().Add(element);

        public ComplexMessage ToComplexMessage() => new ComplexMessage(this);

        public abstract string ToSendableString();

        public override string ToString() => ToSendableString();

        public override int GetHashCode() => ToSendableString().GetHashCode();

        public override bool Equals(object? obj) => Equals(obj as MessageElement);

        public bool Equals(MessageElement? other)
            => base.Equals(other) || other?.ToSendableString() == ToSendableString();
    }
}