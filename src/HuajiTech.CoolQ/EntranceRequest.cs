using HuajiTech.QQ;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示入群请求。
    /// 此类不能在外部被实例化。
    /// </summary>
    internal class EntranceRequest : IEntranceRequest
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
            Respond(Response.Accept, null);
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
        public void Reject(string reason)
        {
            Respond(Response.Reject, reason);
        }

        public void Reject()
        {
            Reject(null);
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

        public Task RejectAsync()
        {
            return RejectAsync(null);
        }

        private void Respond(Response response, string rejectReason)
        {
            NativeMethods.RespondEntranceRequest(
                Bot.Instance.AuthCode, _token, EntranceEventType.Request, response, rejectReason).CheckError();
        }
    }
}