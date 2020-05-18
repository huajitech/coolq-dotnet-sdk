namespace HuajiTech.CoolQ.DataExchange
{
    internal class AnonymousMemberInfo
    {
        public static readonly AnonymousMemberInfo Empty = new AnonymousMemberInfo();

        public long Id { get; set; }

        public string? Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "StyleCop.CSharp.SpacingRules", "SA1011:Closing square brackets should be spaced correctly", Justification = "<¹ÒÆð>")]
        public byte[]? Token { get; set; }
    }
}