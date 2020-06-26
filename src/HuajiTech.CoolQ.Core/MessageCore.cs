using System;

namespace HuajiTech.CoolQ
{
    internal class MessageCore : Message
    {
        private readonly string? _content;

        public MessageCore(int id, string? content = null)
        {
            Id = id;
            _content = content;
        }

        public override int Id { get; }

        public override string Content => _content ?? throw new NotSupportedException();

        public override void Recall()
            => NativeMethods.Message_Recall(Bot.Instance.AuthCode, Id).CheckError();
    }
}