using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义联系人事件。
    /// </summary>
    public interface IContactEventSource
    {
        /// <summary>
        /// 在联系人已添加时引发。
        /// </summary>
        event EventHandler<ContactAddedEventArgs> ContactAdded;

        /// <summary>
        /// 在收到联系人请求时引发。
        /// </summary>
        event EventHandler<ContactRequestedEventArgs> ContactRequested;
    }
}