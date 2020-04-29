using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义可发送消息的对象。
    /// </summary>
    public interface ISendable
    {
        /// <summary>
        /// 向当前 <see cref="ISendable"/> 对象发送指定的字符串。
        /// </summary>
        /// <param name="message">要发送的字符串。</param>
        /// <returns>一个 <see cref="Message"/> 对象，表示已发送的消息。</returns>
        Message Send(string message);

        /// <summary>
        /// 以异步操作向当前 <see cref="ISendable"/> 对象发送指定的字符串。
        /// </summary>
        /// <param name="message">要发送的字符串。</param>
        /// <returns>一个 <see cref="Message"/> 对象，表示已发送的消息。</returns>
        Task<Message> SendAsync(string message);
    }
}