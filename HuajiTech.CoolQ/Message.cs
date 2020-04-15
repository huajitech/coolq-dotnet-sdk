using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示消息。
    /// </summary>
    public class Message
    {
        internal Message(long id, string content)
        {
            Id = id;
            Content = content;
        }

        /// <summary>
        /// 获取内容。
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// 获取 ID。
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// 撤回。
        /// </summary>
        public void Recall()
        {
            NativeMethods.RecallMessage(Bot.AuthCode, Id).CheckError();
        }

        /// <summary>
        /// 以异步操作撤回。
        /// </summary>
        public Task RecallAsync()
        {
            return Task.Run(Recall);
        }

        public override string ToString()
        {
            return Content;
        }

        public static implicit operator string(Message message)
        {
            return message?.Content;
        }
    }
}