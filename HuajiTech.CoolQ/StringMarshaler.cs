using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HuajiTech.CoolQ
{
    internal class StringMarshaler : ICustomMarshaler
    {
        private static readonly Encoding Encoding = Encoding.GetEncoding("GB18030");
        private static readonly StringMarshaler Instance = new StringMarshaler();

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Usage", "CA1801:检查未使用的参数", Justification = "<挂起>")]
        public static ICustomMarshaler GetInstance(string cookie)
        {
            return Instance;
        }

        public void CleanUpManagedData(object ManagedObj)
        {
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            Marshal.FreeHGlobal(pNativeData);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design", "CA1024:在适用处使用属性", Justification = "<挂起>")]
        public int GetNativeDataSize()
        {
            return -1;
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            if (ManagedObj is null)
            {
                return IntPtr.Zero;
            }

            if (ManagedObj is string str)
            {
                var bytes = Encoding.GetBytes(str);
                IntPtr pNativeData = Marshal.AllocHGlobal(bytes.Length + 1);

                Marshal.Copy(bytes, 0, pNativeData, bytes.Length);
                Marshal.WriteByte(pNativeData, bytes.Length, 0);

                return pNativeData;
            }

            throw new MarshalDirectiveException();
        }

        public unsafe object MarshalNativeToManaged(IntPtr pNativeData)
        {
            if (pNativeData == IntPtr.Zero)
            {
                return null;
            }

            var ptr = (sbyte*)pNativeData.ToPointer();
            var length = 0;

            while (*(ptr + length) != '\0')
            {
                length++;
            }

            return new string(ptr, 0, length, Encoding);
        }
    }
}