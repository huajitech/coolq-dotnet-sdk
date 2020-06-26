namespace HuajiTech.CoolQ.Interop
{
    internal class AnonymousMemberInfo
    {
        public static readonly AnonymousMemberInfo Empty = new AnonymousMemberInfo();

        public long Id { get; set; }

        public string? Name { get; set; }

        public byte[]? Token { get; set; }
    }
}