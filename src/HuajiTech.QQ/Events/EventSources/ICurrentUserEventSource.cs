namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义当前用户事件。
    /// </summary>
    public interface ICurrentUserEventSource : IMessageEventSource, IFriendshipEventSource, INotifyEntranceInvited
    {
    }
}