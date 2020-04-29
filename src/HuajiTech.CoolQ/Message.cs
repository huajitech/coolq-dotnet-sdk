namespace HuajiTech.CoolQ
{
    internal class Message : QQ.Message
    {
        public Message(long id, string content)
            : base(content)
        {
            Id = id;
        }

        public long Id { get; }

        public override bool Equals(QQ.Message other) =>
            base.Equals(other) && other is Message message && message.Id == Id;

        public override void Recall()
        {
            NativeMethods.RecallMessage(Bot.Instance.AuthCode, Id).CheckError();
        }
    }
}