using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义好友正在添加事件。
    /// </summary>
    public interface INotifyFriendAdding
    {
        /// <summary>
        /// 在收到好友请求时引发。
        /// </summary>
        event EventHandler<FriendAddingEventArgs> FriendAdding;
    }
}