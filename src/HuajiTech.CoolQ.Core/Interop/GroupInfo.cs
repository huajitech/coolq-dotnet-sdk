namespace HuajiTech.CoolQ.Interop
{
    internal class GroupInfo
    {
        public static readonly GroupInfo Empty = new GroupInfo();

        public int MemberCapacity { get; set; }

        public int MemberCount { get; set; }

        public string? Name { get; set; }

        public long Number { get; set; }
    }
}