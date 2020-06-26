using System;
using System.IO;
using System.Net;
using System.Text;

namespace HuajiTech.CoolQ.Interop
{
    internal abstract class Writer<T> : IDisposable
    {
        protected static readonly Encoding Encoding = Encoding.GetEncoding("GB18030");

        protected Writer()
        {
            MemoryStream = new MemoryStream();
            BinaryWriter = new BinaryWriter(MemoryStream);
        }

        protected BinaryWriter BinaryWriter { get; }

        protected MemoryStream MemoryStream { get; }

        protected static short ToBigEndian(short littleEndian) => IPAddress.HostToNetworkOrder(littleEndian);

        protected static int ToBigEndian(int littleEndian) => IPAddress.HostToNetworkOrder(littleEndian);

        protected static long ToBigEndian(long littleEndian) => IPAddress.HostToNetworkOrder(littleEndian);

        public void Dispose()
        {
            MemoryStream.Dispose();
            BinaryWriter.Dispose();
        }

        public string GetBase64() => Convert.ToBase64String(MemoryStream.ToArray());

        public abstract void Write(T value);

        protected void Write(byte[] bytes)
        {
            var length = (short)bytes.Length;
            Write(length);
            BinaryWriter.Write(bytes);
        }

        protected void Write(string str) => Write(Encoding.GetBytes(str));

        protected void Write(short value) => BinaryWriter.Write(ToBigEndian(value));

        protected void Write(int value) => BinaryWriter.Write(ToBigEndian(value));
    }
}