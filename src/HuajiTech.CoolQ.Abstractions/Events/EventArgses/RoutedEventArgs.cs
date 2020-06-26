using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 表示路由事件的数据，并为所有事件数据提供基类。
    /// </summary>
    public class RoutedEventArgs : EventArgs
    {
        /// <summary>
        /// 获取或设置一个值，指示事件是否已处理完毕。
        /// </summary>
        public virtual bool Handled { get; set; }
    }
}