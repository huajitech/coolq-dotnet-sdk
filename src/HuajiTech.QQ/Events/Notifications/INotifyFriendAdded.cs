using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义好友已添加事件。
    /// </summary>
    public interface INotifyFriendAdded
    {
        /// <summary>
        /// 在好友已添加时引发。
        /// </summary>
        event EventHandler<FriendAddedEventArgs> FriendAdded;
    }
}