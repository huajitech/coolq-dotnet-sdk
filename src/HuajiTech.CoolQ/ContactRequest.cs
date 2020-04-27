using HuajiTech.QQ;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示联系人请求。
    /// 此类不能在外部被实例化。
    /// </summary>
    internal class ContactRequest : IContactRequest
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
        /// <param name="remark">联系人的备注名。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Accept(string remark)
        {
            Respond(Response.Accept, remark);
        }

        public void Accept()
        {
            Accept(null);
        }

        /// <summary>
        /// 以异步操作同意请求。
        /// </summary>
        /// <param name="remark">联系人的备注名。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task AcceptAsync(string remark)
        {
            return Task.Run(() => Accept(remark));
        }

        public Task AcceptAsync()
        {
            return AcceptAsync(null);
        }

        /// <summary>
        /// 拒绝请求。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Reject()
        {
            Respond(Response.Reject, null);
        }

        /// <summary>
        /// 以异步操作拒绝请求。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task RejectAsync()
        {
            return Task.Run(Reject);
        }

        private void Respond(Response response, string alias)
        {
            NativeMethods.RespondContactRequest(
                Bot.Instance.AuthCode, _token, response, alias).CheckError();
        }
    }
}