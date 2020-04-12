using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示联系人请求。
    /// </summary>
    public class ContactRequest
    {
        private readonly string _token;

        internal ContactRequest(string token, string message)
        {
            _token = token;
            Message = message;
        }

        /// <summary>
        /// 获取消息。
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 同意请求。
        /// </summary>
        /// <param name="alias">联系人的备注名。</param>
        public void Accept(string alias = null)
        {
            Respond(RequestResponse.Accept, alias);
        }

        /// <summary>
        /// 以异步操作同意请求。
        /// </summary>
        /// <param name="alias">联系人的备注名。</param>
        public Task AcceptAsync(string alias = null)
        {
            return Task.Run(() => Accept(alias));
        }

        /// <summary>
        /// 拒绝请求。
        /// </summary>
        public void Reject()
        {
            Respond(RequestResponse.Reject, null);
        }

        /// <summary>
        /// 以异步操作拒绝请求。
        /// </summary>
        public Task RejectAsync()
        {
            return Task.Run(Reject);
        }

        private void Respond(RequestResponse response, string alias)
        {
            NativeMethods.RespondContactRequest(
                Bot.AuthCode, _token, response, alias).CheckError();
        }
    }
}