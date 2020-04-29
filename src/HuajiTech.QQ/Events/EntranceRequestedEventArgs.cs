using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="IEntranceRequestEventSource.EntranceRequested"/> 事件提供数据。
    /// </summary>
    public class EntranceRequestedEventArgs : TimedEventArgs
    {
        public EntranceRequestedEventArgs(
            DateTime time, Group source, User requester, EntranceRequest request)
            : base(time)
        {
            Source = source;
            Requester = requester;
            Request = request;
        }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public Group Source { get; }

        /// <summary>
        /// 获取请求用户。
        /// </summary>
        public User Requester { get; }

        /// <summary>
        /// 获取请求。
        /// </summary>
        public EntranceRequest Request { get; }
    }
}