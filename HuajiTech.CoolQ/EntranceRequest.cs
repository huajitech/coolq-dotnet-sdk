using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示入群请求。
    /// </summary>
    public class EntranceRequest
    {
        private readonly string _token;

        internal EntranceRequest(string token, string message)
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
        public void Accept()
        {
            Respond(RequestResponse.Accept, null);
        }

        /// <summary>
        /// 以异步操作同意请求。
        /// </summary>
        public Task AcceptAsync()
        {
            return Task.Run(Accept);
        }

        /// <summary>
        /// 拒绝请求。
        /// </summary>
        /// <param name="reason">理由。</param>
        public void Reject(string reason = null)
        {
            Respond(RequestResponse.Reject, reason);
        }

        /// <summary>
        /// 以异步操作拒绝请求。
        /// </summary>
        /// <param name="reason">理由。</param>
        public Task RejectAsync(string reason = null)
        {
            return Task.Run(() => Reject(reason));
        }

        private void Respond(RequestResponse response, string rejectReason)
        {
            NativeMethods.RespondEntranceRequest(
                Bot.AuthCode, _token, EntranceType.Request, response, rejectReason).CheckError();
        }
    }
}