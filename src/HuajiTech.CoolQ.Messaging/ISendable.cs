namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 定义用于发送的方法。
    /// </summary>
    public interface ISendable
    {
        /// <summary>
        /// 获取当前 <see cref="ISendable"/> 实例的可发送字符串表示形式。
        /// </summary>
        string ToSendableString();
    }
}