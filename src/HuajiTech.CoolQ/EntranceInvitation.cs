namespace HuajiTech.CoolQ
{
    internal class EntranceInvitation : QQ.IRequest
    {
        private readonly string _token;

        public EntranceInvitation(string token, string message)
        {
            _token = token;
            Message = message;
        }

        public string Message { get; }

        public void Accept() => Respond(Response.Accept);

        public void Reject() => Respond(Response.Reject);

        private void Respond(Response response) =>
            NativeMethods.EntranceRequest_Respond(
                Bot.Instance.AuthCode, _token, EntranceType.Passive, response, null).CheckError();
    }
}