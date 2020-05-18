namespace HuajiTech.CoolQ.DataExchange
{
    internal sealed class FriendInfoReader : Reader<FriendInfo>
    {
        public FriendInfoReader(string base64String)
            : base(base64String)
        {
        }

        public override FriendInfo Read() =>
            new FriendInfo
            {
                Number = ReadInt64(),
                Nickname = ReadString(),
                Alias = ReadString()
            };
    }
}