namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示消息元素。
    /// 此类为抽象类。
    /// </summary>
    public abstract class MessageElement
    {
        public static MessageElement FromString(string str)
        {
            return new PlainText(str);
        }

        public ComplexMessage Add(MessageElement element)
        {
            return ComplexMessage.FromMessageElement(this).Add(element);
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