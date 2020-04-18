using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示入群邀请。
    /// 此类不能在外部被实例化。
    /// </summary>
    public class EntranceInvitation
    {
        private readonly string _token;

        internal EntranceInvitation(string token, string message)
        {
            _token = token;
            Message = message;
        }

        /// <summary>
        /// 获取当前 <see cref="EntranceInvitation"/> 对象的附加消息。
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 同意邀请。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Accept()
        {
            Respond(RequestResponse.Accept);
        }

        /// <summary>
        /// 以异步操作同意邀请。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task AcceptAsync()
        {
            return Task.Run(Accept);
        }

        /// <summary>
        /// 拒绝邀请。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Reject()
        {
            Respond(RequestResponse.Reject);
        }

        /// <summary>
        /// 以异步操作拒绝邀请。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task RejectAsync()
        {
            return Task.Run(Reject);
        }

        private void Respond(RequestResponse response)
        {
            NativeMethods.RespondEntranceRequest(
                Bot.AuthCode, _token, EntranceType.Invite, response, null).CheckError();
        }
    }
}