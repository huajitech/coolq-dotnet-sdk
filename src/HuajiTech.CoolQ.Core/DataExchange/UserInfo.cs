namespace HuajiTech.CoolQ.DataExchange
{
    internal class UserInfo
    {
        public static readonly UserInfo Empty = new UserInfo();

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public string? Nickname { get; set; }

        public long Number { get; set; }
    }
}