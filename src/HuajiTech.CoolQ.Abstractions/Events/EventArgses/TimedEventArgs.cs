using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 为可以提供引发时间的事件数据提供基类。
    /// </summary>
    public class TimedEventArgs : RoutedEventArgs
    {
        public TimedEventArgs(DateTime time)
        {
            Time = time;
        }

        /// <summary>
        /// 获取事件发生的时间。
        /// </summary>
        public DateTime Time { get; }
    }
}