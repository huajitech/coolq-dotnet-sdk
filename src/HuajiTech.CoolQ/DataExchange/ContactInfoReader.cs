namespace HuajiTech.CoolQ.DataExchange
{
    internal sealed class ContactInfoReader : Reader<ContactInfo>
    {
        public ContactInfoReader(string base64String)
            : base(base64String)
        {
        }

        public override ContactInfo Read() =>
            new ContactInfo
            {
                Number = ReadInt64(),
                Nickname = ReadString(),
                Alias = ReadString()
            };
    }
}