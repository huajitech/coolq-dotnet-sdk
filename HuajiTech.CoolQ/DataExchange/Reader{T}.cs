using HuajiTech.CoolQ.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace HuajiTech.CoolQ.DataExchange
{
    internal abstract class Reader<T> : IDisposable
    {
        protected static readonly Encoding Encoding = Encoding.GetEncoding("GB18030");

        protected Reader(string base64String)
        {
            if (base64String is null)
            {
                throw new CoolQException(Resources.NullReturnValue);
            }

            BinaryReader = new BinaryReader(
                new MemoryStream(Convert.FromBase64String(base64String)));
        }

        protected BinaryReader BinaryReader { get; }

        public void Dispose()
        {
            BinaryReader.Dispose();
        }

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

        protected static short FromBigEndian(short bigEndian)
        {
            return IPAddress.NetworkToHostOrder(bigEndian);
        }

        protected static int FromBigEndian(int bigEndian)
        {
            return IPAddress.NetworkToHostOrder(bigEndian);
        }

        protected static long FromBigEndian(long bigEndian)
        {
            return IPAddress.NetworkToHostOrder(bigEndian);
        }

        protected byte[] ReadBytes()
        {
            var length = ReadInt16();
            return BinaryReader.ReadBytes(length);
        }

        protected short ReadInt16()
        {
            return FromBigEndian(BinaryReader.ReadInt16());
        }

        protected int ReadInt32()
        {
            return FromBigEndian(BinaryReader.ReadInt32());
        }

        protected bool ReadBoolean()
        {
            return ReadInt32() > 0;
        }

        protected DateTime ReadDateTime()
        {
            return Timestamp.ToDateTime(ReadInt32());
        }

        protected long ReadInt64()
        {
            return FromBigEndian(BinaryReader.ReadInt64());
        }

        protected string ReadString()
        {
            var bytes = ReadBytes();
            if (bytes.Length == 0)
            {
                return null;
            }

            return Encoding.GetString(bytes);
        }
    }
}