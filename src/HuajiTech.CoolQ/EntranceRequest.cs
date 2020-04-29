namespace HuajiTech.CoolQ
{
    internal class EntranceRequest : QQ.EntranceRequest
    {
        private readonly string _token;

        internal EntranceRequest(string token, string message)
        {
            _token = token;
            Message = message;
        }

        public override string Message { get; }

        public override void Accept() => Respond(Response.Accept, null);

        public override void Reject(string reason) => Respond(Response.Reject, reason);

        private void Respond(Response response, string rejectReason)
        {
            NativeMethods.RespondEntranceRequest(
                Bot.Instance.AuthCode, _token, EntranceEventType.Request, response, rejectReason).CheckError();
        }
    }
}