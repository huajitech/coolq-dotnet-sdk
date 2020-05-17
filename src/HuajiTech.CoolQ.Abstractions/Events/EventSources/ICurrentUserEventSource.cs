namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义当前用户事件。
    /// </summary>
    public interface ICurrentUserEventSource : IMessageEventSource, IFriendshipEventSource, INotifyEntranceInvited
    {
    }
}