using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="IContactEventSource.ContactAdded"/> 事件提供数据。
    /// </summary>
    public class ContactAddedEventArgs : TimedEventArgs
    {
        public ContactAddedEventArgs(DateTime time, IContact contactAdded)
            : base(time)
        {
            ContactAdded = contactAdded;
        }

        /// <summary>
        /// 获取添加的联系人。
        /// </summary>
        public IContact ContactAdded { get; }
    }
}