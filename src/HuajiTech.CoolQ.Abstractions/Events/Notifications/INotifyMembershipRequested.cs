using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义入群请求事件。
    /// </summary>
    public interface INotifyMembershipRequested
    {
        /// <summary>
        /// 在收到入群请求时引发。
        /// </summary>
        event EventHandler<MembershipRequestedEventArgs> MembershipRequested;
    }
}