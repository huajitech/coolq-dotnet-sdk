using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示入群请求。
    /// 此类不能在外部被实例化。
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
        /// 获取当前 <see cref="EntranceRequest"/> 对象的附加消息。
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 同意请求。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Accept()
        {
            Respond(RequestResponse.Accept, null);
        }

        /// <summary>
        /// 以异步操作同意请求。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task AcceptAsync()
        {
            return Task.Run(Accept);
        }

        /// <summary>
        /// 拒绝请求。
        /// </summary>
        /// <param name="reason">理由。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Reject(string reason = null)
        {
            Respond(RequestResponse.Reject, reason);
        }

        /// <summary>
        /// 以异步操作拒绝请求。
        /// </summary>
        /// <param name="reason">理由。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
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