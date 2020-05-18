namespace HuajiTech.CoolQ.DataExchange
{
    internal class FriendInfo
    {
        public static readonly FriendInfo Empty = new FriendInfo();

        public string? Alias { get; set; }

        public string? Nickname { get; set; }

        public long Number { get; set; }
    }
}