using HuajiTech.QQ;

namespace HuajiTech.CoolQ
{
    internal class Message : IMessage
    {
        public Message(long id)
        {
            Id = id;
        }

        public long Id { get; }

        public override bool Equals(object? obj) => Equals(obj as IMessage);

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => $"Message({Id})";

        public bool Equals(IMessage? other) =>
            base.Equals(other) || other?.Id == Id;

        public void Recall() =>
            NativeMethods.Message_Recall(Bot.Instance.AuthCode, Id).CheckError();
    }
}