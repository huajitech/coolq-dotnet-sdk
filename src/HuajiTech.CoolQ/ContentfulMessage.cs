using HuajiTech.QQ;
using System;

namespace HuajiTech.CoolQ
{
    internal class ContentfulMessage : Message, IContentfulMessage
    {
        public ContentfulMessage(long id, string content)
            : base(id)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public string Content { get; }

        public override string ToString() => Content;
    }
}