using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 为 <see cref="CurrentUser.ContactRequested"/> 事件提供数据。
    /// </summary>
    public class ContactRequestedEventArgs : RoutedEventArgs
    {
        public ContactRequestedEventArgs(
            DateTime time, User requester, ContactRequest request)
        {
            Time = time;
            Requester = requester;
            Request = request;
        }

        /// <summary>
        /// 获取时间。
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// 获取请求用户。
        /// </summary>
        public User Requester { get; }

        /// <summary>
        /// 获取请求。
        /// </summary>
        public ContactRequest Request { get; }
    }
}