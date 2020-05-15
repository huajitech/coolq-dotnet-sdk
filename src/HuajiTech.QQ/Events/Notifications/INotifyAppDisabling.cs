using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义应用禁用事件。
    /// </summary>
    public interface INotifyAppDisabling
    {
        /// <summary>
        /// 在应用被禁用时引发。
        /// </summary>
        event EventHandler AppDisabling;
    }
}