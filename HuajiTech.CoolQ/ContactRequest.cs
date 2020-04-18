using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示联系人请求。
    /// 此类不能在外部被实例化。
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
        /// 获取当前 <see cref="ContactRequest"/> 对象的附加消息。
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 同意请求。
        /// </summary>
        /// <param name="alias">联系人的备注名。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Accept(string alias = null)
        {
            Respond(RequestResponse.Accept, alias);
        }

        /// <summary>
        /// 以异步操作同意请求。
        /// </summary>
        /// <param name="alias">联系人的备注名。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task AcceptAsync(string alias = null)
        {
            return Task.Run(() => Accept(alias));
        }

        /// <summary>
        /// 拒绝请求。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Reject()
        {
            Respond(RequestResponse.Reject, null);
        }

        /// <summary>
        /// 以异步操作拒绝请求。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
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