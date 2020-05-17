using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义入群邀请事件。
    /// </summary>
    public interface INotifyEntranceInvited
    {
        /// <summary>
        /// 在被邀请加群时引发。
        /// </summary>
        event EventHandler<EntranceInvitedEventArgs> EntranceInvited;
    }
}