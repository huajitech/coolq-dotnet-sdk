using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="IEntranceRequestEventSource.EntranceRequested"/> 事件提供数据。
    /// </summary>
    public class EntranceRequestedEventArgs : TimedEventArgs
    {
        public EntranceRequestedEventArgs(
            DateTime time, IGroup source, IUser requester, IEntranceRequest request)
            : base(time)
        {
            Source = source;
            Requester = requester;
            Request = request;
        }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public IGroup Source { get; }

        /// <summary>
        /// 获取请求用户。
        /// </summary>
        public IUser Requester { get; }

        /// <summary>
        /// 获取请求。
        /// </summary>
        public IEntranceRequest Request { get; }
    }
}