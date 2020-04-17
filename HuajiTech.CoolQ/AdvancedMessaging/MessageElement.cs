namespace HuajiTech.CoolQ.AdvancedMessaging
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
            return new ComplexMessage(this).Add(element);
        }

        public static ComplexMessage operator +(MessageElement left, MessageElement right)
        {
            if (left is null)
            {
                return null;
            }

            if (right is null)
            {
                return new ComplexMessage(left);
            }

            return left.Add(right);
        }

        public static implicit operator MessageElement(string str)
        {
            return FromString(str);
        }
    }
}