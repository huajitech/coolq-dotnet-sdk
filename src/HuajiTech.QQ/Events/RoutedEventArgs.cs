using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 表示路由事件的数据，并为所有酷Q事件数据提供基类。
    /// </summary>
    public class RoutedEventArgs : EventArgs
    {
        /// <summary>
        /// 获取或设置一个值，指示事件是否已处理完毕。
        /// </summary>
        public bool Handled { get; set; }
    }
}