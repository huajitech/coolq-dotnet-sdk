using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 为 <see cref="INotifyEntranceRequested.EntranceRequested"/> 事件提供数据。
    /// </summary>
    public class EntranceRequestedEventArgs : TimedEventArgs
    {
        public EntranceRequestedEventArgs(
            DateTime time, IGroup source, IUser requester, IEntranceRequest request)
            : base(time)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Requester = requester ?? throw new ArgumentNullException(nameof(requester));
            Request = request ?? throw new ArgumentNullException(nameof(request));
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