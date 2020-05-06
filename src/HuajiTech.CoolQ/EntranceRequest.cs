namespace HuajiTech.CoolQ
{
    internal class EntranceRequest : QQ.IEntranceRequest
    {
        private readonly string _token;

        public EntranceRequest(string token, string message)
        {
            _token = token;
            Message = message;
        }

        public string Message { get; }

        public void Accept() => Respond(Response.Accept, null);

        public void Reject(string reason) => Respond(Response.Reject, reason);

        public void Reject() => Reject(null);

        private void Respond(Response response, string rejectReason) =>
            NativeMethods.RespondEntranceRequest(
                Bot.Instance.AuthCode, _token, MemberEventType.Active, response, rejectReason).CheckError();
    }
}