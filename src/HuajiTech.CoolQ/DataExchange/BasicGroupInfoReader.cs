namespace HuajiTech.CoolQ.DataExchange
{
    internal sealed class BasicGroupInfoReader : Reader<BasicGroupInfo>
    {
        public BasicGroupInfoReader(string base64String)
            : base(base64String)
        {
        }

        public override BasicGroupInfo Read() =>
            new BasicGroupInfo
            {
                Number = ReadInt64(),
                Name = ReadString()
            };
    }
}