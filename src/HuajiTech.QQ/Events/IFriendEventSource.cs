using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义好友事件。
    /// </summary>
    public interface IFriendEventSource
    {
        /// <summary>
        /// 在好友已添加时引发。
        /// </summary>
        event EventHandler<FriendAddedEventArgs> FriendAdded;

        /// <summary>
        /// 在收到好友请求时引发。
        /// </summary>
        event EventHandler<FriendshipRequestedEventArgs> FriendshipRequested;
    }
}