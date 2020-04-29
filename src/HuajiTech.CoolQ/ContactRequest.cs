namespace HuajiTech.CoolQ
{
    internal class ContactRequest : QQ.ContactRequest
    {
        private readonly string _token;

        internal ContactRequest(string token, string message)
        {
            _token = token;
            Message = message;
        }

        public override string Message { get; }

        public override void Accept(string alias) => Respond(Response.Accept, alias);

        public override void Reject() => Respond(Response.Reject, null);

        private void Respond(Response response, string alias)
        {
            NativeMethods.RespondContactRequest(
                Bot.Instance.AuthCode, _token, response, alias).CheckError();
        }
    }
}