using System;

namespace HuajiTech.CoolQ
{
    internal class Message : IMessage
    {
        private readonly string? _content;

        public Message(long id, string? content = null)
        {
            Id = id;
            _content = content;
        }

        public long Id { get; }

        public string Content => _content ?? throw new NotSupportedException();

        public override string ToString() => _content ?? $"{GetType().Name}({Id})";

        public override bool Equals(object? obj) => Equals(obj as IMessage);

        public bool Equals(IMessage? other) => other?.Id == Id;

        public override int GetHashCode() => Id.GetHashCode();

        public void Recall() =>
            NativeMethods.Message_Recall(Bot.Instance.AuthCode, Id).CheckError();
    }
}