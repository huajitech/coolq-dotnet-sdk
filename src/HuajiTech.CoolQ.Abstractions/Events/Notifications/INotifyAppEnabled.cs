using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义应用启用事件。
    /// </summary>
    public interface INotifyAppEnabled
    {
        /// <summary>
        /// 在应用被启用时引发。
        /// </summary>
        event EventHandler AppEnabled;
    }
}