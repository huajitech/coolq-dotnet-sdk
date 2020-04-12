using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 为 <see cref="Group.EntranceRequested"/> 事件提供数据。
    /// </summary>
    public class EntranceRequestedEventArgs : RoutedEventArgs
    {
        public EntranceRequestedEventArgs(
            DateTime time, Group source, User requester, EntranceRequest request)
        {
            Time = time;
            Source = source;
            Requester = requester;
            Request = request;
        }

        /// <summary>
        /// 获取时间。
        /// </summary>
        public DateTime Time { get; }

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