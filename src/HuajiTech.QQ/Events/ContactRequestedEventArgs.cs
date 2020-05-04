using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="IContactEventSource.ContactRequested"/> 事件提供数据。
    /// </summary>
    public class ContactRequestedEventArgs : TimedEventArgs
    {
        public ContactRequestedEventArgs(
            DateTime time, IUser requester, IContactRequest request)
            : base(time)
        {
            Requester = requester;
            Request = request;
        }

        /// <summary>
        /// 获取请求用户。
        /// </summary>
        public IUser Requester { get; }

        /// <summary>
        /// 获取请求。
        /// </summary>
        public IContactRequest Request { get; }
    }
}