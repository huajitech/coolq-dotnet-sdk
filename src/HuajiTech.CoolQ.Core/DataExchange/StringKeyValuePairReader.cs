using System.Collections.Generic;

namespace HuajiTech.CoolQ.DataExchange
{
    internal class StringKeyValuePairReader : Reader<KeyValuePair<string, string>>
    {
        public StringKeyValuePairReader(string base64String)
            : base(base64String)
        {
        }

        public override KeyValuePair<string, string> Read() =>
            new KeyValuePair<string, string>(ReadString()!, ReadString()!);
    }
}