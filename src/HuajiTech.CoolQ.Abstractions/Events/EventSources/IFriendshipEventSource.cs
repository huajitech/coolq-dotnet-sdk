namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义好友事件。
    /// </summary>
    public interface IFriendshipEventSource : INotifyFriendAdded, INotifyFriendshipRequested
    {
    }
}