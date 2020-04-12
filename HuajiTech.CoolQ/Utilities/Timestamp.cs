using System;

namespace HuajiTech.CoolQ.Utilities
{
    internal static class Timestamp
    {
        public static readonly DateTime Base =
            TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local);

        public static DateTime ToDateTime(int timestamp)
        {
            return Base + new TimeSpan(timestamp * TimeSpan.TicksPerSecond);
        }
    }
}