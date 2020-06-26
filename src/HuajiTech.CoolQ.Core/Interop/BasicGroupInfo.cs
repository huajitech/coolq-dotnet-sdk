namespace HuajiTech.CoolQ.Interop
{
    internal class BasicGroupInfo
    {
        public static readonly BasicGroupInfo Empty = new BasicGroupInfo();

        public string? Name { get; set; }

        public long Number { get; set; }
    }
}