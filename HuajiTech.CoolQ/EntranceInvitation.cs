using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示入群邀请。
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
        /// 获取消息。
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 同意邀请。
        /// </summary>
        public void Accept()
        {
            Respond(RequestResponse.Accept);
        }

        /// <summary>
        /// 以异步操作同意邀请。
        /// </summary>
        public Task AcceptAsync()
        {
            return Task.Run(Accept);
        }

        /// <summary>
        /// 拒绝邀请。
        /// </summary>
        public void Reject()
        {
            Respond(RequestResponse.Reject);
        }

        /// <summary>
        /// 以异步操作拒绝邀请。
        /// </summary>
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