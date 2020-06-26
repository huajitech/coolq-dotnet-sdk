namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义消息的接收者。
    /// </summary>
    public interface ISendee
    {
        /// <summary>
        /// 向当前 <see cref="ISendee"/> 实例发送指定的字符串。
        /// </summary>
        /// <param name="message">要发送的字符串。</param>
        /// <returns>一个 <see cref="Message"/> 实例，表示已发送的消息。</returns>
        Message Send(string message);
    }
}