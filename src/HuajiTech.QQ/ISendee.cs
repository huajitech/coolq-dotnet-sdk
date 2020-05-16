namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义可发送消息的对象。
    /// </summary>
    public interface ISendee
    {
        /// <summary>
        /// 向当前 <see cref="ISendee"/> 对象发送指定的字符串。
        /// </summary>
        /// <param name="message">要发送的字符串。</param>
        /// <returns>一个 <see cref="IContentfulMessage"/> 对象，表示已发送的消息。</returns>
        IContentfulMessage Send(string message);
    }
}