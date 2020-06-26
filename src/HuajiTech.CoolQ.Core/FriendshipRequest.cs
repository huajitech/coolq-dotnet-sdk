namespace HuajiTech.CoolQ
{
    internal class FriendshipRequest : IFriendshipRequest
    {
        private readonly string _token;

        public FriendshipRequest(string token, string message)
        {
            _token = token;
            Message = message;
        }

        public string Message { get; }

        public void Accept(string? alias) => Respond(Response.Accept, alias);

        public void Accept() => Accept(null);

        public void Reject() => Respond(Response.Reject, null);

        private void Respond(Response response, string? alias)
            => NativeMethods.FriendshipRequest_Respond(
                  Bot.Instance.AuthCode, _token, response, alias).CheckError();
    }
}