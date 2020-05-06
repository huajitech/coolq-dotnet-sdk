using HuajiTech.QQ;

namespace HuajiTech.CoolQ.DataExchange
{
    internal class FileReader : Reader<File>
    {
        public FileReader(string base64String)
            : base(base64String)
        {
        }

        public override File Read() =>
            new File(
                id: ReadString(),
                name: ReadString(),
                length: ReadInt64(),
                busId: ReadInt64());
    }
}