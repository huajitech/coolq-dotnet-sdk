namespace HuajiTech.CoolQ
{
    internal class MembershipRequest : IMembershipRequest
    {
        private readonly string _token;

        public MembershipRequest(string token, string message)
        {
            _token = token;
            Message = message;
        }

        public string Message { get; }

        public void Accept() => Respond(Response.Accept, null);

        public void Reject(string? reason) => Respond(Response.Reject, reason);

        public void Reject() => Reject(null);

        private void Respond(Response response, string? rejectReason) =>
            NativeMethods.EntranceRequest_Respond(
                Bot.Instance.AuthCode, _token, Entrance.Active, response, rejectReason).CheckError();
    }
}