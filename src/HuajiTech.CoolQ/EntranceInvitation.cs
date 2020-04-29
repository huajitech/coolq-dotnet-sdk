namespace HuajiTech.CoolQ
{
    internal class EntranceInvitation : QQ.Request
    {
        private readonly string _token;

        internal EntranceInvitation(string token, string message)
        {
            _token = token;
            Message = message;
        }

        public override string Message { get; }

        public override void Accept() => Respond(Response.Accept);

        public override void Reject() => Respond(Response.Reject);

        private void Respond(Response response)
        {
            NativeMethods.RespondEntranceRequest(
                Bot.Instance.AuthCode, _token, EntranceEventType.Invite, response, null).CheckError();
        }
    }
}