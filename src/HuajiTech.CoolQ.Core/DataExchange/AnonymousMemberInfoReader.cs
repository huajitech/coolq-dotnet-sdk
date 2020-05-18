namespace HuajiTech.CoolQ.DataExchange
{
    internal sealed class AnonymousMemberInfoReader : Reader<AnonymousMemberInfo>
    {
        public AnonymousMemberInfoReader(string base64String)
            : base(base64String)
        {
        }

        public override AnonymousMemberInfo Read() =>
            new AnonymousMemberInfo
            {
                Id = ReadInt64(),
                Name = ReadString(),
                Token = ReadBytes()
            };
    }
}