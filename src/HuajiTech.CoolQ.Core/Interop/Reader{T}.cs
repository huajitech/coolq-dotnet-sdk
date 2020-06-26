using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using HuajiTech.CoolQ.Utilities;

namespace HuajiTech.CoolQ.Interop
{
    internal abstract class Reader<T> : IDisposable
    {
        protected static readonly Encoding Encoding = Encoding.GetEncoding("GB18030");

        protected Reader(string base64String)
        {
            BinaryReader = new BinaryReader(
                new MemoryStream(Convert.FromBase64String(base64String)));
        }

        protected BinaryReader BinaryReader { get; }

        protected static short FromBigEndian(short bigEndian) => IPAddress.NetworkToHostOrder(bigEndian);

        protected static int FromBigEndian(int bigEndian) => IPAddress.NetworkToHostOrder(bigEndian);

        protected static long FromBigEndian(long bigEndian) => IPAddress.NetworkToHostOrder(bigEndian);

        public void Dispose() => BinaryReader.Dispose();

        public abstract T Read();

        public IEnumerable<T> ReadAll()
        {
            var length = ReadInt32();

            for (var i = 0; i < length; i++)
            {
                _ = ReadInt16();
                yield return Read();
            }
        }

        protected byte[] ReadBytes()
        {
            var length = ReadInt16();
            return BinaryReader.ReadBytes(length);
        }

        protected short ReadInt16() => FromBigEndian(BinaryReader.ReadInt16());

        protected int ReadInt32() => FromBigEndian(BinaryReader.ReadInt32());

        protected bool ReadBoolean() => ReadInt32() > 0;

        protected DateTime ReadDateTime() => Timestamp.ToDateTime(ReadInt32());

        protected long ReadInt64() => FromBigEndian(BinaryReader.ReadInt64());

        protected string? ReadString()
        {
            var bytes = ReadBytes();

            if (bytes.Length is 0)
            {
                return null;
            }

            return Encoding.GetString(bytes);
        }
    }
}