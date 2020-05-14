using HuajiTech.QQ;

namespace HuajiTech.CoolQ
{
    internal class Message : IMessage
    {
        public Message(long id, string content)
        {
            Id = id;
            Content = content;
        }

        public long Id { get; }

        public string Content { get; }

        public override bool Equals(object? obj) => Equals(obj as IMessage);

        public override int GetHashCode() => Id.GetHashCode() ^ Content.GetHashCode();

        public override string ToString() => Content;

        public bool Equals(IMessage? other) =>
            base.Equals(other) || (other?.Id == Id && other.Content == Content);

        public void Recall() =>
            NativeMethods.Message_Recall(Bot.Instance.AuthCode, Id).CheckError();
    }
}