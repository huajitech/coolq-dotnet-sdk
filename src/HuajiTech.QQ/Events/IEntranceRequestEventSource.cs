using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义入群请求事件。
    /// </summary>
    public interface IEntranceRequestEventSource
    {
        /// <summary>
        /// 在收到入群请求时引发。
        /// </summary>
        event EventHandler<EntranceRequestedEventArgs> EntranceRequested;
    }
}